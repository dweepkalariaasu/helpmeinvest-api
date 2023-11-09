using helpmeinvest.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace helpmeinvest.Repositories
{
    public class AccountRepo
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountRepo(IHelpMeInvestDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _accounts = database.GetCollection<Account>(settings.AccountsCollectionName);
        }

        public List<Account> Get() =>
            _accounts.Find(account => true).ToList();

        public Account Get(string id) =>
            _accounts.Find<Account>(account => account.Id == id).FirstOrDefault();

        public List<Account> GetByCustomerId(int customerId) =>
            _accounts.Find(account => account.PrimaryAccountHolder == customerId).ToList<Account>();

        public Account Create(Account account)
        {
            _accounts.InsertOne(account);
            return account;
        }

        public void Update(string id, Account accountIn) =>
            _accounts.ReplaceOne(account => account.Id == id, accountIn);

        public void Remove(Account accountIn) =>
            _accounts.DeleteOne(account => account.Id == accountIn.Id);

        public void Remove(string id) =>
            _accounts.DeleteOne(account => account.Id == id);
    }
}
