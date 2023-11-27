using helpmeinvest.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace helpmeinvest.Models
{
    public class ChannelIntegration
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int? CustomerId { get; set; }

        public ChannelParams? ChannelParams { get; set; }

        public UserSelection? UserSelection { get; set; }
    }

    public class ChannelParams
    {
        public AccountType AccountType { get; set; }

        public AccountType? AdditionalAccountType { get; set; }
    }

    public class UserSelection {
        public RegistrationType? RegistrationType { get; set; }

        public  int? AccountId { get; set; }
    }
}
