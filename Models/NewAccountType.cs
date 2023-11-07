using helpmeinvest.Enums;

namespace helpmeinvest.Models
{
    public class NewAccountType
    {
        public AccountType AccountType { get; set; }
        public RegistrationType RegistrationType { get; set; }
        public bool IsCommon { get; set; }
        public int Rank { get; set; }
    }
}
