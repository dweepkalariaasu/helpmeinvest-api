using helpmeinvest.Enums;
using helpmeinvest.Models;
using helpmeinvest.Models.Response;
using helpmeinvest.Repositories;
using System.Collections.Generic;

namespace helpmeinvest.Services
{
    public class AccountService
    {
        private readonly AccountRepo AccountRepo;
        private readonly ChannelIntegrationRepo CiRepo;

        public AccountService(AccountRepo accountRepo, ChannelIntegrationRepo ciRepo)
        {
            AccountRepo = accountRepo;
            CiRepo = ciRepo;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return AccountRepo.Get();
        }

        public Account CreateAccount(Account account)
        {
            return AccountRepo.Create(account);
        }

        public NewAccountTypesResponse GetNewAccountTypes(string referenceId)
        {
            
            var ciObj = CiRepo.Get(referenceId);
            var accountType = (ciObj != null) ? ciObj.AccountType : AccountType.Brokerage;



        }
    }
}
