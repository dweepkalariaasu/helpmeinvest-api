using helpmeinvest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helpmeinvest.Models.Request
{
    public class SaveApplication
    {
        public RegistrationType? SelectedRegistrationType { get; set; }
        public int? SelectedAccountId { get; set; }

        public DateTime DateOfBirth { get; set; }
        public TaxFilingStatus TaxFilingStatus { get; set; }
        public int AdjustedGrossIncome { get; set; }
        public CustomerType CustomerType { get; set; }
    }
}
