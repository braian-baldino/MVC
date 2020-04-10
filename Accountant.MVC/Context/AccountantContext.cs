using Accountant.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Context
{
    public class AccountantContext : DbContext
    {

        public AccountantContext(DbContextOptions<AccountantContext> options) : base(options) { }

        public DbSet<AnualBalance> AnualBalances { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<Income> Incomes { get; set; }
    }
}
