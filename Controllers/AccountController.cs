using helpmeinvest.Models;
using helpmeinvest.Models.Response;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace helpmeinvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService service)
        {
            _accountService = service;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Account> GetAccounts()
        {
            return _accountService.GetAccounts();
        }

        [HttpPost]
        public Account CreateAccount(Account account)
        {
            return _accountService.CreateAccount(account);
        }

        [HttpGet("new")]
        public NewAccountTypesResponse GetNewAccountTypes(string referenceId)
        {
            return _accountService.GetNewAccountTypes(referenceId);
        }

        [HttpGet("existing")]
        public IEnumerable<ExistingAccount> GetExistingAccounts(int customerId)
        {
            return _accountService.GetExistingAccounts(customerId);
        }
    }
}
