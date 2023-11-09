using helpmeinvest.Models;
using helpmeinvest.Repositories;
using System.Collections.Generic;

namespace helpmeinvest.Services
{
    public class CustomerService
    {
        private readonly CustomerRepo CustomerRepo;

        public CustomerService(CustomerRepo customerRepo)
        {
            CustomerRepo = customerRepo;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return CustomerRepo.Get();
        }

        public Customer CreateCustomer(Customer customer)
        {
            return CustomerRepo.Create(customer);
        }
    }
}
