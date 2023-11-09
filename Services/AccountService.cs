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
        private readonly INewAccountTypes NewAccountTypes;

        public AccountService(AccountRepo accountRepo,
            ChannelIntegrationRepo ciRepo,
            INewAccountTypes settings)
        {
            AccountRepo = accountRepo;
            CiRepo = ciRepo;
            NewAccountTypes = settings;
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
            var response = new NewAccountTypesResponse();

            response.AccountTypes = NewAccountTypes.NewAccountTypeList.FindAll(a => a.AccountType == accountType);

            if (ciObj.AdditionalAccountType != null)
            {
                response.AdditionalAccountTypes = NewAccountTypes.NewAccountTypeList.FindAll(a => a.AccountType == ciObj.AdditionalAccountType);
            }
            return response;
        }

        public List<ExistingAccount> GetExistingAccounts(int customerId)
        {
            var accounts = AccountRepo.GetByCustomerId(customerId);
            var existingAccounts = new List<ExistingAccount>();
            accounts.ForEach(a => existingAccounts.Add(new ExistingAccount
            {
                Id = a.Id,
                AccountId = a.AccountId,
                AccountType = a.AccountType,
                Balance = a.Balance,
                RegistrationType = a.RegistrationType,
                UnrealizedGainLoss = a.UnrealizedGainLoss
            }));

            return existingAccounts;
        }
    }
}
