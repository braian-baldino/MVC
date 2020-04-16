using Accountant.MVC.Models;
using Accountant.MVC.Models.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Interfaces
{
    public interface ISpendingRepository : IBasicRepository<Spending>
    {
        //IBasicRepository<T>
        //public Task<List<T>> GetAll();
        //public Task<T> Add(T entity);
        //public Task<T> Update(T entity);
        //public Task<T> Delete(int id);
        //public Task<T> Get(int id);

        public Task<List<Spending>> GetSpendingsFromBalance(int balanceId);
        public Task<Balance> GetParentBalance(int spendingId);
        public Task<List<Balance>> GetBalances();
        public Task<Balance> GetBalance(int id);
        public Task<List<AnualBalance>> GetAnualBalances();
        public Task<List<ESpendingType>> GetSpendingTypes();

        public bool SpendingExists(int id);
    }
}
