using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Models
{
    public class Balance
    {
        public int Id { get; set; }
        [ForeignKey("AnualBalance")]
        public int AnualBalanceId { get; set; }
        public List<Income> Incomes { get; set; } = new List<Income>();
        public List<Spending> Spendings { get; set; } = new List<Spending>();
        public string Month { get; set; }
        public double? TotalIncomes { get; set; }
        public double? TotalSpendings { get; set; }
        public double? BalanceResult { get; set; }
        public bool Positive { get; set; }
    }
}
