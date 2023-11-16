using helpmeinvest.Enums;
using System.Collections.Generic;

namespace helpmeinvest.Models.Response
{
    public class NewAccountTypesResponse
    {
        public AccountType SelectedAccountType { get; set; }

        public AccountType? AdditionalAccountType { get; set; }

        public List<NewAccountType> AccountTypes { get; set; }
    }
}
