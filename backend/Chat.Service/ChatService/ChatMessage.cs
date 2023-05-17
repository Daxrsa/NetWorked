using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ChatService
{
    public class ChatMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string User { get; set; }
        public string Room { get; set; }
        public string Message { get; set; }
    }
}
