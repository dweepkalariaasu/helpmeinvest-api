using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helpmeinvest.Models
{
    public class NewAccountTypes: INewAccountTypes
    {
        public List<NewAccountType> NewAccountTypeList { get; set; }
    }

    public interface INewAccountTypes
    {
        public List<NewAccountType> NewAccountTypeList { get; set; }
    }
}
