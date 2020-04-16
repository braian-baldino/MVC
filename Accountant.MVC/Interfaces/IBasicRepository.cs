using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Interfaces
{
    public interface IBasicRepository<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T> Get(int id);
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(int id);
    }
}
