using Accountant.MVC.Models;
using Accountant.MVC.Models.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Interfaces
{
    public interface IBalanceRepository : IBasicRepository<Balance>
    {
        //IBasicRepository<T>
        //public Task<List<T>> GetAll();
        //public Task<T> Add(T entity);
        //public Task<T> Update(T entity);
        //public Task<T> Delete(int id);
        //public Task<T> Get(int id);

        public Task<List<Balance>> GetBalancesFromYear(int anualBalanceId);
        public Task<AnualBalance> GetParentAnualBalance(int balanceId);
        public Task<List<AnualBalance>> GetAnualBalances();
        public Task<List<EMonth>> GetMonthList();

        public Task<Balance> CalculateAndSave(Balance balance);
        public Task<AnualBalance> CalculateAndSaveYear(AnualBalance anualBalance);
        public bool BalanceExists(int id);
        
    }
}
