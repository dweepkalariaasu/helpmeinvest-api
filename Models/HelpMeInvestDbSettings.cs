namespace helpmeinvest.Models
{
    public class HelpMeInvestDbSettings: IHelpMeInvestDbSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string AccountsCollectionName { get; set; }

        public string CustomersCollectionName { get; set; }

        public string ChannelIntegrationCollectionName { get; set; }
    }

    public interface IHelpMeInvestDbSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string AccountsCollectionName { get; set; }

        public string CustomersCollectionName { get; set; }

        public string ChannelIntegrationCollectionName { get; set; }
    }
}
