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
    public class AnualBalanceRepository : IAnualBalanceRepository
    {
        private readonly AccountantContext _context;
        private readonly IBalanceRepository _balanceRepo;

        public AnualBalanceRepository(AccountantContext context, IBalanceRepository balanceRepo)
        {
            _context = context;
            _balanceRepo = balanceRepo;
        }

        public async Task<AnualBalance> Add(AnualBalance entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Add(entity);
                    await _context.SaveChangesAsync();

                    return entity;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AnualBalanceExists(int id)
        {
            return _context.AnualBalances.Any(e => e.Id == id);
        }

        public async Task<AnualBalance> CalculateAndSave(AnualBalance anualBalance)
        {
            try
            {
                anualBalance.AnualBalanceResult = 0;

                List<Balance> balances = await _balanceRepo.GetBalancesFromYear(anualBalance.Id);

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

        public async Task<AnualBalance> Delete(int id)
        {
            try
            {
                AnualBalance anualBalanaceToRemove = await Get(id);

                if (anualBalanaceToRemove.Balances.Count > 0)
                    return null;

                _context.AnualBalances.Remove(anualBalanaceToRemove);
                await _context.SaveChangesAsync();

                return anualBalanaceToRemove;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AnualBalance> Get(int id)
        {
            try
            {
                return await _context.AnualBalances
                    .Include(b => b.Balances)
                    .Where(a => a.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<AnualBalance>> GetAll()
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

        public async Task<List<Balance>> GetBalancesFromYear(int anualBalanceId)
        {
            try
            {
                return await _balanceRepo.GetBalancesFromYear(anualBalanceId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AnualBalance> Update(AnualBalance entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
