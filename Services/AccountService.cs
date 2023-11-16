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

        public NewAccountTypesResponse GetNewAccountTypes(string referenceId)
        {
            var ciObj = CiRepo.Get(referenceId);
            var response = new NewAccountTypesResponse();
            response.SelectedAccountType = ciObj?.AccountType ?? AccountType.Brokerage;
            response.AdditionalAccountType = ciObj?.AdditionalAccountType;
            response.AccountTypes = new List<NewAccountType>();
            response.AccountTypes.AddRange(NewAccountTypes.NewAccountTypeList.FindAll(a => a.AccountType == response.SelectedAccountType));

            if (ciObj.AdditionalAccountType != null)
            {
                response.AccountTypes.AddRange(NewAccountTypes.NewAccountTypeList.FindAll(a => a.AccountType == ciObj.AdditionalAccountType));
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
                InEligibleReason = EligibilityService.GetAccountEligibility(a),
                IsEligible = (EligibilityService.GetAccountEligibility(a) == string.Empty)
            }));

            return existingAccounts;
        }
    }
}
