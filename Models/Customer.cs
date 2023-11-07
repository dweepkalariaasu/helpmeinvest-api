using helpmeinvest.Enums;
using System;

namespace helpmeinvest.Models
{
    public class Customer
    {
        public int id { get; set; }

        public int CustomerId { get; set; }

        public CustomerType CustomerType { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SSN { get; set; }

        public TaxFilingStatus TaxFilingStatus { get; set; }

        public int AdjustedGrossIncome { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
