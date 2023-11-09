using helpmeinvest.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace helpmeinvest.Repositories
{
    public class ChannelIntegrationRepo
    {
        private readonly IMongoCollection<ChannelIntegration> _channelIntegrations;

        public ChannelIntegrationRepo(IHelpMeInvestDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _channelIntegrations = database.GetCollection<ChannelIntegration>(settings.ChannelIntegrationCollectionName);
        }

        public List<ChannelIntegration> Get() =>
            _channelIntegrations.Find(channelIntegration => true).ToList();

        public ChannelIntegration Get(string id) =>
            _channelIntegrations.Find<ChannelIntegration>(channelIntegration => channelIntegration.Id == id).FirstOrDefault();

        public ChannelIntegration Create(ChannelIntegration channelIntegration)
        {
            _channelIntegrations.InsertOne(channelIntegration);
            return channelIntegration;
        }

        public void Update(string id, ChannelIntegration channelIntegrationIn) =>
            _channelIntegrations.ReplaceOne(channelIntegration => channelIntegration.Id == id, channelIntegrationIn);

        public void Remove(ChannelIntegration channelIntegrationIn) =>
            _channelIntegrations.DeleteOne(channelIntegration => channelIntegration.Id == channelIntegrationIn.Id);

        public void Remove(string id) =>
            _channelIntegrations.DeleteOne(channelIntegration => channelIntegration.Id == id);
    }
}
