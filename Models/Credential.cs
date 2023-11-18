using helpmeinvest.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace helpmeinvest.Models
{
    public class Credential
    {
        [BsonId]
        public string CustomerId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }

        public Role? Role { get; set; }
    }
}
