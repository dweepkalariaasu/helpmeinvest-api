using helpmeinvest.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace helpmeinvest.Models
{
    public class ChannelIntegration
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int? customerId { get; set; }
        public AccountType AccountType { get; set; }
        public bool additionalAccountTypesAllowed { get; set; }
    }
}
