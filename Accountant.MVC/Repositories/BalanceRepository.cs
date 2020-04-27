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
    public class BalanceRepository: IBalanceRepository
    {
        private readonly AccountantContext _context;

        public BalanceRepository(AccountantContext context)
        {
            _context = context;
        }

        public async Task<List<Balance>> GetAll()
        {
            try
            {
                return await _context.Balances
               .Include(i => i.Incomes)
               .Include(s => s.Spendings)
               .Include(m => m.Month)
               .OrderBy(o => o.Month.Id)
               .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Balance>> GetBalancesFromYear(int anualBalanceId)
        {
            try
            {
                return await _context.Balances
                .Include(i => i.Incomes)
                .Include(s => s.Spendings)
                .Include(m => m.Month)
                .OrderBy(o => o.Month.Id)
                .Where(a => a.AnualBalanceId == anualBalanceId)
                .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AnualBalance> GetParentAnualBalance(int balanceId)
        {
            try
            {
                List<AnualBalance> anualBalances = await GetAnualBalances();

                foreach (AnualBalance anualBalance in anualBalances)
                {
                    foreach (Balance balance in anualBalance.Balances)
                    {
                        if (balance.Id == balanceId)
                            return anualBalance;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<AnualBalance>> GetAnualBalances()
        {
            try
            {
                return await _context.AnualBalances
                .Include(b => b.Balances)
                .ThenInclude(m => m.Month)
                .OrderBy(o => o.Year)
                .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AnualBalance> GetAnualBalance(int anualBalanceId)
        {
            try
            {
                return await _context.AnualBalances.Where(a => a.Id == anualBalanceId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<EMonth>> GetMonthList()
        {
            try
            {
                return await _context.Months
                    .OrderBy(o => o.Id)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> Get(int balanceId)
        {
            try
            {
                return await _context.Balances
               .Include(i => i.Incomes)
               .Include(s => s.Spendings)
               .Include(m => m.Month)
               .Where(b => b.Id == balanceId)
               .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EMonth> GetMonth(int monthId)
        {
            try
            {
                return await _context.Months.Where(m => m.Id == monthId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UniqueMonthValidation(Balance newBalance)
        {
            try
            {
                AnualBalance anualBalance = await _context.AnualBalances
                    .Include(b => b.Balances)
                    .ThenInclude(m => m.Month)
                    .Where(a => a.Id == newBalance.AnualBalanceId)
                    .FirstOrDefaultAsync();

                if(anualBalance != null)
                {
                    foreach (Balance _bal in anualBalance.Balances)
                    {
                        if (_bal.Month.Id == newBalance.MonthId)
                            return false;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw new Exception("Internal Error UniqueMonthValidation");
            }
        }

        public async Task<Balance> Add(Balance newBalance)
        {
            try
            {
                if(newBalance != null)
                {
                    newBalance.Month = await GetMonth(newBalance.MonthId);                 
                    newBalance.TotalIncomes = 0;
                    newBalance.TotalSpendings = 0;
                    newBalance.BalanceResult = 0;

                    _context.Add(newBalance);
                    await _context.SaveChangesAsync();

                    return newBalance;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> Update(Balance balance)
        {
            try
            {
                _context.Update(balance);
                await _context.SaveChangesAsync();
                return balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> Delete(int balanceId)
        {
            try
            {
                Balance balanceToRemove = await Get(balanceId);
                _context.Balances.Remove(balanceToRemove);
                await _context.SaveChangesAsync();

                return balanceToRemove;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> CalculateAndSave(Balance balance)
        {
            try
            {
                if(balance != null)
                {
                    balance.TotalSpendings = 0;
                    balance.TotalIncomes = 0;

                    foreach (Income income in balance.Incomes)
                    {
                        balance.TotalIncomes += income.Amount;
                    }

                    foreach (Spending spending in balance.Spendings)
                    {
                        balance.TotalSpendings += spending.Amount;
                    }

                    balance.BalanceResult = balance.TotalIncomes - balance.TotalSpendings;

                    balance.Positive = (balance.BalanceResult >= 0) ? true : false;

                    if (await Update(balance) != null && await CalculateAndSaveYear( await GetParentAnualBalance(balance.Id)) != null)
                        return balance;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
                     
        }

        public async Task<AnualBalance> CalculateAndSaveYear(AnualBalance anualBalance)
        {
            try
            {
                anualBalance.AnualBalanceResult = 0;

                List<Balance> balances = await GetBalancesFromYear(anualBalance.Id);

                foreach (Balance balance in balances)
                {
                    anualBalance.AnualBalanceResult += balance.BalanceResult;
                }

                anualBalance.Positive = (anualBalance.AnualBalanceResult >= 0) ? true : false;

                _context.Update(anualBalance);
                await _context.SaveChangesAsync();

                return anualBalance;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool BalanceExists(int id)
        {
            return _context.Balances.Any(e => e.Id == id);
        }
    }
}
