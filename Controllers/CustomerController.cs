using helpmeinvest.Models;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace helpmeinvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService CustomerService;
        public CustomerController(CustomerService service)
        {
            CustomerService = service;
        }

        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return CustomerService.GetCustomers();
        }

        [HttpPost]
        public Customer CreateCustomer(Customer account)
        {
            return CustomerService.CreateCustomer(account);
        }
    }
}
