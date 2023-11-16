using helpmeinvest.Enums;
using helpmeinvest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helpmeinvest.Services
{
    public class AccountEligibilityService
    {
        private RegistrationType[] IneligibleRegTypes = {
            RegistrationType.Custodial,
            RegistrationType.LivingTrust,
            RegistrationType.Joint
        };

        private const string ineligibleRegistrationType= "Ineligible account type";

        private const string transferInProgress = "Ineligible account type";

        public string GetAccountEligibility(Account account)
        {
            if (account.AccountId == 12345678)
            {
                return transferInProgress;
            }
            else if (IneligibleRegTypes.Contains(account.RegistrationType))
            {
                return ineligibleRegistrationType;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
