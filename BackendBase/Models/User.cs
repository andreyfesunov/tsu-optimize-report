using MongoDB.Bson.Serialization.Attributes;

namespace BackendBase.Models
{
    public class User : Base
    {
        [BsonElement("nickname")]
        public string Nickname { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("password")]
        public string Password { get; set; } = null!;
        
    }
}
