using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helpmeinvest.Models.Response
{
    public class NewAccountTypesResponse
    {
        public IEnumerable<NewAccountType> AccountTypes { get; set; }
        public IEnumerable<NewAccountType> AdditionalAccountTypes { get; set; }
    }
}
