using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatService
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public async Task SaveAsync()
        {
            // Your code to save the user to the database goes here
            // This is just a placeholder method
            await Task.Delay(100);
        }
    }

}
