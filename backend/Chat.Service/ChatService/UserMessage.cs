using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatService
{
    public class UserMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string User { get; set; }
    }
}
