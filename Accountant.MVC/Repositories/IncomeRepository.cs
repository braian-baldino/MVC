using Accountant.MVC.Context;
using Accountant.MVC.Interfaces;
using Accountant.MVC.Models;
using Accountant.MVC.Models.DropDowns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AccountantContext _context;
        private readonly IBalanceRepository _balanceRepo;

        public IncomeRepository(AccountantContext context,IBalanceRepository balanceRepo)
        {
            _context = context;
            _balanceRepo = balanceRepo;
        }

        public async Task<Income> Add(Income income)
        {
            try
            {
                if (income != null)
                {
                    _context.Add(income);
                    await _context.SaveChangesAsync();

                    if (await _balanceRepo.CalculateAndSave(await GetParentBalance(income.Id)) != null)
                        return income;

                    return null;
                }

                return null;
            }
            catch (Exception)
            {
                throw null;
            }
        }

        public async Task<Income> Delete(int id)
        {
            try
            {
                Income incomeToRemove = await Get(id);
                 _context.Incomes.Remove(incomeToRemove);
                await _context.SaveChangesAsync();

                if (await _balanceRepo.CalculateAndSave(await GetParentBalance(incomeToRemove.Id)) != null)
                    return incomeToRemove;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Income> Get(int id)
        {
            try
            {
                return await _context.Incomes.Where(i => i.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Income>> GetAll()
        {
            try
            {
                return await _context.Incomes.ToListAsync();
            }
            catch (Exception)
            {
                throw null;
            }
        }

        public async Task<List<AnualBalance>> GetAnualBalances()
        {
            try
            {
                return await _context.AnualBalances.Include(b => b.Balances).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> GetBalance(int id)
        {
            try
            {
                return await _balanceRepo.Get(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Balance>> GetBalances()
        {
            try
            {
                return await _balanceRepo.GetAll();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Income>> GetIncomesFromBalance(int balanceId)
        {
            try
            {
                if(_balanceRepo.BalanceExists(balanceId))
                {
                    return await _context.Incomes.Where(i => i.BalanceId == balanceId).ToListAsync();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<EIncomeType>> GetIncomeTypes()
        {
            try
            {
                return await _context.IncomeTypes.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> GetParentBalance(int incomeId)
        {
            try
            {
                Income income = await Get(incomeId);

                if (income == null)
                    return null;

                Balance balance = await _balanceRepo.Get(income.BalanceId);

                if (balance == null)
                    return null;

                return balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.Id == id);
        }

        public async Task<Income> Update(Income entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                if (await _balanceRepo.CalculateAndSave(await GetParentBalance(entity.Id)) != null)
                    return entity;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
