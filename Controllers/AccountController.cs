using helpmeinvest.Models;
using helpmeinvest.Models.Response;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace helpmeinvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService AccountService;
        public AccountController(AccountService accountService)
        {
            AccountService = accountService;
        }

        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return AccountService.GetAccounts();
        }

        [HttpPost]
        public Account CreateAccount(Account account)
        {
            return AccountService.CreateAccount(account);
        }

        [HttpGet("new")]
        public NewAccountTypesResponse GetNewAccountTypes(string referenceId)
        {
            return AccountService.GetNewAccountTypes(referenceId);
        }

        [HttpGet("existing")]
        public IEnumerable<ExistingAccount> GetExistingAccounts(int customerId)
        {
            return AccountService.GetExistingAccounts(customerId);
        }
    }
}
