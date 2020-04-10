using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Models
{
    public class Income : CommonType
    {
        public int Id { get; set; }
        [ForeignKey("Balance")]
        public int BalanceId { get; set; }
    }
}
