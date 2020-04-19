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
    public class SpendingRepository : ISpendingRepository
    {
        private readonly AccountantContext _context;
        private readonly IBalanceRepository _balanceRepo;

        public SpendingRepository(AccountantContext context,IBalanceRepository balanceRepo)
        {
            _context = context;
            _balanceRepo = balanceRepo;
        }

        public async Task<Spending> Add(Spending entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Add(entity);
                    await _context.SaveChangesAsync();

                    if (await _balanceRepo.CalculateAndSave(await GetParentBalance(entity.Id)) != null)
                        return entity;

                    return null;
                }

                return null;
            }
            catch (Exception)
            {
                throw null;
            }
        }

        public async Task<Spending> Delete(int id)
        {
            try
            {
                Spending spendingToRemove = await Get(id);
                _context.Spendings.Remove(spendingToRemove);
                await _context.SaveChangesAsync();

                if (await _balanceRepo.CalculateAndSave(await GetParentBalance(spendingToRemove.Id)) != null)
                    return spendingToRemove;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Spending> Get(int id)
        {
            try
            {
                return await _context.Spendings.Where(i => i.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Spending>> GetAll()
        {
            try
            {
                return await _context.Spendings
                    .OrderBy(o => o.Date)
                    .ToListAsync();
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

        public async Task<Balance> GetParentBalance(int spendingId)
        {
            try
            {
                Spending spending = await Get(spendingId);

                if (spending == null)
                    return null;

                Balance balance = await _balanceRepo.Get(spending.BalanceId);

                if (balance == null)
                    return null;

                return balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Spending>> GetSpendingsFromBalance(int balanceId)
        {
            try
            {
                if (_balanceRepo.BalanceExists(balanceId))
                {
                    return await _context.Spendings
                        .OrderBy(o => o.Date)
                        .Where(i => i.BalanceId == balanceId).ToListAsync();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ESpendingType>> GetSpendingTypes()
        {
            try
            {
                return await _context.SpendingTypes
                    .OrderBy(o => o.CategoryName)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SpendingExists(int id)
        {
            return _context.Spendings.Any(e => e.Id == id);
        }

        public async Task<Spending> Update(Spending entity)
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
