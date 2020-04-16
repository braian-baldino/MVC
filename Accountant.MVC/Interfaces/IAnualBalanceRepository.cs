using Accountant.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Interfaces
{
    public interface IAnualBalanceRepository : IBasicRepository<AnualBalance>
    {
        //IBasicRepository<T>
        //public Task<List<T>> GetAll();
        //public Task<T> Add(T entity);
        //public Task<T> Update(T entity);
        //public Task<T> Delete(int id);
        //public Task<T> Get(int id);

        public Task<List<Balance>> GetBalancesFromYear(int anualBalanceId);
        public Task<AnualBalance> CalculateAndSave(AnualBalance anualBalance);
        public bool AnualBalanceExists(int id);
    }
}
