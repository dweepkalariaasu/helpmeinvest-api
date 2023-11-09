using helpmeinvest.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace helpmeinvest.Models
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public int AccountId { get; set; }

        public AccountType AccountType { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public decimal Balance { get; set; }

        public decimal UnrealizedGainLoss { get; set; }

        public int PrimaryAccountHolder { get; set; }

        public List<int> AccountHolders { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
