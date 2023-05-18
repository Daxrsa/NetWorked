using Microsoft.AspNetCore.SignalR;
using System.Linq;
using MongoDB.Driver;

namespace ChatService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ChatMessage> _messagesCollection;



        public ChatHub(IDictionary<string, UserConnection> connections, IMongoDatabase mongoDatabase)
        {
            _botUser = "NetWorked";
            _connections = connections;
            _mongoDatabase = mongoDatabase;
            _messagesCollection = _mongoDatabase.GetCollection<ChatMessage>("chatMessages");
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has left");
                SendConnectedUsers(userConnection.Room);

            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", userConnection.User, message);

                // Store the message in the MongoDB collection
                var chatMessage = new ChatMessage
                {
                    User = userConnection.User,
                    Room = userConnection.Room,
                    Message = message
                };
                await _messagesCollection.InsertOneAsync(chatMessage);
            }
        }



        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
            _connections[Context.ConnectionId] = userConnection;
            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has joined {userConnection.Room}");
            await SendConnectedUsers(userConnection.Room);
        }

        public Task SendConnectedUsers(string room)
        {
            var users = _connections.Values.Where(c => c.Room == room).Select(c => c.User);
            return Clients.Group(room).SendAsync("UsersInRoom", users);
        }
        public async Task DeleteMessage(string messageId)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                // Check if the user has permission to delete the message (e.g., the message was sent by the same user)
                var filter = Builders<ChatMessage>.Filter.Eq("Id", messageId) & Builders<ChatMessage>.Filter.Eq("User", userConnection.User);
                var deleteResult = await _messagesCollection.DeleteOneAsync(filter);

                if (deleteResult.DeletedCount > 0)
                {
                    // Inform the clients in the room that a message was deleted
                    await Clients.Group(userConnection.Room).SendAsync("MessageDeleted", messageId);
                }
            }
        }


    }
}