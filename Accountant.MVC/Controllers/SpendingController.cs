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
    public class SpendingController : Controller
    {
        private readonly ISpendingRepository _repository;

        public SpendingController(ISpendingRepository repository)
        {
            _repository = repository;
        }

        // GET: Spending
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: Spending/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spending = await _repository.Get((int)id);

            if (spending == null)
            {
                return NotFound();
            }

            return View(spending);
        }

        // GET: Spending/Create
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                if(id == null)
                {
                    ViewBag.AnualBalances = await _repository.GetAnualBalances();
                    ViewBag.Categories = await _repository.GetSpendingTypes();
                    return View("Create");
                }
                else
                {
                    ViewBag.Balance = await _repository.GetBalance((int)id);
                    ViewBag.Categories = await _repository.GetSpendingTypes();
                    return View("CreateFromBalance");
                }
                
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // POST: Spending/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BalanceId","Category","Description","Amount","Date")]Spending spending)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(await _repository.Add(spending) != null)
                        return RedirectToAction(nameof(Details), "Balance", new { id = spending.BalanceId }, null);
                }
                catch (Exception)
                {
                    throw new Exception("No se pudo cargar el gasto.");
                }               
            }
            return View(spending);
        }

        // GET: Spending/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spending = await _repository.Get((int)id);

            if (spending == null)
            {
                return NotFound();
            }
            return View(spending);
        }

        // POST: Spending/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BalanceId,Category,Description,Amount,Date")] Spending spending)
        {
            if (id != spending.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await _repository.Update(spending) != null)
                        return RedirectToAction(nameof(Details), "Balance", new { id = spending.BalanceId }, null);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.SpendingExists(spending.Id))
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
            return View(spending);
        }

        // GET: Spending/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Spending spending = await _repository.Get((int)id);

            if (spending == null)
            {
                return NotFound();
            }

            return View(spending);
        }

        // POST: Spending/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Spending spending = await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
