﻿using Accountant.MVC.Models;
using Accountant.MVC.Models.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Interfaces
{
    public interface IIncomeRepository : IBasicRepository<Income>
    {
        //IBasicRepository<T>
        //public Task<List<T>> GetAll();
        //public Task<T> Add(T entity);
        //public Task<T> Update(T entity);
        //public Task<T> Delete(int id);
        //public Task<T> Get(int id);

        public Task<List<Income>> GetIncomesFromBalance(int balanceId);
        public Task<Balance> GetParentBalance(int incomeId);
        public Task<List<Balance>> GetBalances();
        public Task<Balance> GetBalance(int id);
        public Task<List<AnualBalance>> GetAnualBalances();
        public Task<List<EIncomeType>> GetIncomeTypes();

        public bool IncomeExists(int id);
    }
}
