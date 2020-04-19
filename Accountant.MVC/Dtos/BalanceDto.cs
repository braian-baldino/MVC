using Accountant.MVC.Models;
using Accountant.MVC.Models.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Dtos
{
    public class BalanceDto
    {
        public List<AnualBalance> Years { get; set; } = new List<AnualBalance>();
        public List<EMonth> MonthList { get; set; } = new List<EMonth>();
        public Balance Balance { get; set; }
    }
}
