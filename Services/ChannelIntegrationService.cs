using helpmeinvest.Models;
using helpmeinvest.Repositories;
using System.Collections.Generic;

namespace helpmeinvest.Services
{
    public class ChannelIntegrationService
    {
        private readonly ChannelIntegrationRepo ciRepo;

        public ChannelIntegrationService(ChannelIntegrationRepo repo)
        {
            ciRepo = repo;
        }

        public IEnumerable<ChannelIntegration> GetChannelIntegrations()
        {
            return ciRepo.Get();
        }

        public string CreateChannelIntegration(ChannelIntegration ci)
        {
            var response = ciRepo.Create(ci);
            if (response != null)
            {
                return response.Id;
            }
            return string.Empty;
        }
    }
}
