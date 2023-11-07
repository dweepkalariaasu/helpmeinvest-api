using helpmeinvest.Enums;
using System;
using System.Collections.Generic;

namespace helpmeinvest.Models
{
    public class Account
    {
        public string Id { get; set; }

        public int AccountId { get; set; }

        public AccountType AccountType { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public int Balance { get; set; }

        public int UnrealizedGainLoss { get; set; }

        public int primaryAccountHolder { get; set; }

        public List<int> accountHolders { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
