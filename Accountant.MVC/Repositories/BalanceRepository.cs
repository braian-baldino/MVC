using Accountant.MVC.Context;
using Accountant.MVC.Interfaces;
using Accountant.MVC.Models;
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
               .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<List<Balance>> GetBalancesFromYear(int anualBalanceId)
        {
            try
            {
                return await _context.Balances
                .Include(i => i.Incomes)
                .Include(s => s.Spendings)
                .Where(a => a.AnualBalanceId == anualBalanceId)
                .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception();
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
               .Where(b => b.Id == balanceId)
               .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> Add(Balance balance)
        {
            try
            {
                if(balance != null)
                {
                    balance.TotalIncomes = 0;
                    balance.TotalSpendings = 0;
                    balance.BalanceResult = 0;

                    _context.Add(balance);
                    await _context.SaveChangesAsync();

                    return balance;
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
