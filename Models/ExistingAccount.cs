using helpmeinvest.Enums;

namespace helpmeinvest.Models
{
    public class ExistingAccount
    {
        public string Id { get; set; }

        public int AccountId { get; set; }

        public AccountType AccountType { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public int Balance { get; set; }

        public int UnrealizedGainLoss { get; set; }
    }
}
