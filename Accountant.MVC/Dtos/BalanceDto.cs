using Accountant.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Dtos
{
    public class BalanceDto
    {
        public List<AnualBalance> Years { get; set; } = new List<AnualBalance>();
        public Balance Month { get; set; }
    }
}
