using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Accountant.MVC.Context;
using Accountant.MVC.Models;

namespace Accountant.MVC.Controllers
{
    public class AnualBalanceController : Controller
    {
        private readonly AccountantContext _context;

        public AnualBalanceController(AccountantContext context)
        {
            _context = context;
        }

        // GET: AnualBalance
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnualBalances.ToListAsync());
        }

        // GET: AnualBalance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anualBalance = await _context.AnualBalances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anualBalance == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Create([Bind("Id,AnualBalanceResult")] AnualBalance anualBalance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anualBalance);
                await _context.SaveChangesAsync();
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

            var anualBalance = await _context.AnualBalances.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnualBalanceResult")] AnualBalance anualBalance)
        {
            if (id != anualBalance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anualBalance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnualBalanceExists(anualBalance.Id))
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

            var anualBalance = await _context.AnualBalances
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var anualBalance = await _context.AnualBalances.FindAsync(id);
            _context.AnualBalances.Remove(anualBalance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnualBalanceExists(int id)
        {
            return _context.AnualBalances.Any(e => e.Id == id);
        }
    }
}
