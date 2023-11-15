using helpmeinvest.Enums;
using helpmeinvest.Models;
using helpmeinvest.Repositories;
using System.Collections.Generic;

namespace helpmeinvest.Services
{
    public class AccountService
    {
        private readonly AccountRepo AccountRepo;
        private readonly ChannelIntegrationRepo CiRepo;
        private readonly INewAccountTypes NewAccountTypes;
        private readonly AccountEligibilityService EligibilityService;

        public AccountService(AccountRepo accountRepo,
            ChannelIntegrationRepo ciRepo,
            INewAccountTypes settings,
            AccountEligibilityService eligibilityService)
        {
            AccountRepo = accountRepo;
            CiRepo = ciRepo;
            NewAccountTypes = settings;
            EligibilityService = eligibilityService;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return AccountRepo.Get();
        }

        public Account CreateAccount(Account account)
        {
            return AccountRepo.Create(account);
        }

        public Dictionary<AccountType, IEnumerable<NewAccountType>> GetNewAccountTypes(string referenceId)
        {
            var ciObj = CiRepo.Get(referenceId);
            var accountType = (ciObj != null) ? ciObj.AccountType : AccountType.Brokerage;
            var response = new Dictionary<AccountType, IEnumerable<NewAccountType>>();

            response.Add(accountType, NewAccountTypes.NewAccountTypeList.FindAll(a => a.AccountType == accountType));

            if (ciObj.AdditionalAccountType != null)
            {
                response.Add((AccountType)(ciObj.AdditionalAccountType), 
                    NewAccountTypes.NewAccountTypeList.FindAll(a => a.AccountType == ciObj.AdditionalAccountType));
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
                AccountId = "..." + a.AccountId.ToString().Substring(a.AccountId.ToString().Length - 3),
                AccountType = a.AccountType,
                Balance = a.Balance,
                RegistrationType = a.RegistrationType,
                UnrealizedGainLoss = a.UnrealizedGainLoss,
                inEligibleReason = EligibilityService.GetAccountEligibility(a),
                IsEligible = (EligibilityService.GetAccountEligibility(a) == string.Empty)
            }));

            return existingAccounts;
        }
    }
}
