using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Accountant.MVC.Context;
using Accountant.MVC.Models;
using Accountant.MVC.Interfaces;

namespace Accountant.MVC.Controllers
{
    public class AnualBalanceController : Controller
    {
        private readonly IAnualBalanceRepository _repository;

        public AnualBalanceController(IAnualBalanceRepository repository)
        {
            _repository = repository;
        }

        // GET: AnualBalance
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: AnualBalance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anualBalance = await _repository.Get((int)id);
            
            if (anualBalance == null)
            {
                return NotFound();
            }

            anualBalance.Balances = await _repository.GetBalancesFromYear(anualBalance.Id);

            return View(anualBalance);
        }

        // GET: AnualBalance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnualBalance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,AnualBalanceResult")] AnualBalance anualBalance)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(anualBalance);
                return RedirectToAction(nameof(Index));
            }
            return View(anualBalance);
        }

        // GET: AnualBalance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anualBalance = await _repository.Get((int)id);

            if (anualBalance == null)
            {
                return NotFound();
            }
            return View(anualBalance);
        }

        // POST: AnualBalance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,AnualBalanceResult")] AnualBalance anualBalance)
        {
            if (id != anualBalance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(anualBalance);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.AnualBalanceExists(anualBalance.Id))
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
            return View(anualBalance);
        }

        // GET: AnualBalance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anualBalance = await _repository.Get((int)id);

            if (anualBalance == null)
            {
                return NotFound();
            }

            return View(anualBalance);
        }

        // POST: AnualBalance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (await _repository.Delete(id) != null)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
           
                
        }

    }
}
