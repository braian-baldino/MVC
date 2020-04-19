using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Accountant.MVC.Context;
using Accountant.MVC.Models;
using Accountant.MVC.Dtos;
using Accountant.MVC.Interfaces;

namespace Accountant.MVC.Controllers
{
    public class BalanceController : Controller
    {
        private readonly IBalanceRepository _repository;

        public BalanceController(IBalanceRepository repository)
        {
            _repository = repository;
        }

        // GET: Balance
        public async Task<IActionResult> Index(int anualBalanceId)
        {
            return View(await _repository.GetBalancesFromYear(anualBalanceId));
        }

        // GET: Balance/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var balance = await _repository.Get((int)id);

            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // GET: Balance/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                BalanceDto balanceDto = new BalanceDto();

                balanceDto.Years = await _repository.GetAnualBalances();
                balanceDto.MonthList = await _repository.GetMonthList();

                return View(balanceDto);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // POST: Balance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BalanceDto balanceDto)
        {
            if (ModelState.IsValid)
            {                         
                try
                {
                    if(!await _repository.UniqueMonthValidation(balanceDto.Balance))
                    {
                        return BadRequest();
                    }

                    Balance newBalance = await _repository.Add(balanceDto.Balance);

                    if (newBalance == null)
                        throw new Exception();

                    return RedirectToAction(nameof(Details), "Balance", new { id = newBalance.Id }, null);
                }
                catch (Exception)
                {
                    throw new Exception("Couldnt create Balance");
                }
                
            }
            return View(balanceDto);
        }

        // GET: Balance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _repository.Get((int)id);

            if (balance == null)
            {
                return NotFound();
            }
            return View(balance);
        }

        // POST: Balance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnualBalanceId,Month,BalanceResult")] Balance balance)
        {
            if (id != balance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _repository.Update(balance);

                    if (response == null)
                        throw new Exception("Couldn't update balance");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.BalanceExists(balance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(balance);
        }

        // GET: Balance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _repository.Get((int)id);

            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // POST: Balance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _repository.Delete(id) != null)
                return RedirectToAction(nameof(Index));
            else
                return BadRequest();
        }

    }
}
