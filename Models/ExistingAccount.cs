using helpmeinvest.Enums;

namespace helpmeinvest.Models
{
    public class ExistingAccount
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public AccountType AccountType { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public decimal Balance { get; set; }

        public decimal UnrealizedGainLoss { get; set; }

        public bool IsEligible { get; set; }

        public string? inEligibleReason { get; set; }

    }
}
