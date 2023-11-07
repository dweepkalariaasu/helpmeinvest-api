using helpmeinvest.Enums;

namespace helpmeinvest.Models
{
    public class ChannelIntegration
    {
        public int Id { get; set; }
        public AccountType AccountType { get; set; }
        public bool additionalAccountTypesAllowed { get; set; }
    }
}
