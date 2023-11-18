using helpmeinvest.Models;
using MongoDB.Driver;
using System.Linq;

namespace helpmeinvest.Repositories
{
    public class CredentialRepo
    {
        private readonly IMongoCollection<Credential> _credentials;

        public CredentialRepo(IHelpMeInvestDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _credentials = database.GetCollection<Credential>(settings.CredentialsCollectionName);
        }

        public Credential Get(string custId) =>
            _credentials.Find<Credential>(credential => credential.CustomerId == custId).FirstOrDefault();

        public Credential IsValidCredential(Credential cred)
        {
            var dbCred = _credentials.Find<Credential>(credential => credential.UserName.Equals(cred.UserName, System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (dbCred != null && cred.Password.Equals(dbCred.Password))
            {
                dbCred.Password = null;
                return dbCred;
            }
            return null;
        }

        public Credential Create(Credential cred)
        {
            var dbCred = _credentials.Find<Credential>(credential => credential.UserName.Equals(cred.UserName, System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (dbCred == null)
            {
                _credentials.InsertOne(cred);
                return cred;
            }
            return null;
        }

        public void Update(string custId, Credential credentialIn) =>
            _credentials.ReplaceOne(credential => credential.CustomerId == custId, credentialIn);

        public void Remove(Credential credentialIn) =>
            _credentials.DeleteOne(credential => credential.CustomerId == credentialIn.CustomerId);

        public void Remove(string custId) =>
            _credentials.DeleteOne(credential => credential.CustomerId == custId);
    }
}
