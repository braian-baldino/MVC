using Accountant.MVC.Models;
using Accountant.MVC.Models.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Dtos
{
    public class SpendingDto
    {
        public List<AnualBalance> AnualBalances { get; set; } = new List<AnualBalance>();
        public Balance Balance { get; set; }
        public List<ESpendingType> Categories { get; set; } = new List<ESpendingType>();
        public Spending Spending { get; set; }
    }
}
