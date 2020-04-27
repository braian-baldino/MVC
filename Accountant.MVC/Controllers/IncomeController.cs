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
    public class IncomeController : Controller
    {
        private readonly IIncomeRepository _repository;

        public IncomeController(IIncomeRepository repository)
        {
            _repository = repository;
        }

        // GET: Income
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: Income/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _repository.Get((int)id);

            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // GET: Income/Create
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.AnualBalances = await _repository.GetAnualBalances();
                    ViewBag.Categories = await _repository.GetIncomeTypes();
                    return View("Create");
                }
                else
                {
                    ViewBag.Balance = await _repository.GetBalance((int)id);
                    ViewBag.Categories = await _repository.GetIncomeTypes();
                    return View("CreateFromBalance");
                }

                throw new Exception();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // POST: Income/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BalanceId","Description","Amount","Date","Category")]Income income)
        {
            if (ModelState.IsValid)
            {
                if (await _repository.Add(income) != null)
                    return RedirectToAction(nameof(Details), "Balance", new { id = income.BalanceId }, null);
            }

            return View(income);
        }

        // GET: Income/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _repository.Get((int)id);

            if (income == null)
            {
                return NotFound();
            }
            return View(income);
        }

        // POST: Income/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BalanceId,Category,Description,Amount,Date")] Income income)
        {
            if (id != income.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await _repository.Update(income) != null)
                        return RedirectToAction(nameof(Details), "Balance", new { id = income.BalanceId }, null);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.IncomeExists(income.Id))
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
            return View(income);
        }

        // GET: Income/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _repository.Get((int)id);

            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // POST: Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
