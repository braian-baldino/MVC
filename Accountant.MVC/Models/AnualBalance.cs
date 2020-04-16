using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Models
{
    public class AnualBalance
    {
        public int Id { get; set; }
        public List<Balance> Balances { get; set; } = new List<Balance>();
        public int Year { get; set; }
        public double? AnualBalanceResult {get; set;}
        public bool Positive { get; set; }
    }
}
