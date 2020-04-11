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
    public class SpendingController : Controller
    {
        private readonly AccountantContext _context;

        public SpendingController(AccountantContext context)
        {
            _context = context;
        }

        // GET: Spending
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spendings.ToListAsync());
        }

        // GET: Spending/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spending = await _context.Spendings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spending == null)
            {
                return NotFound();
            }

            return View(spending);
        }

        // GET: Spending/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spending/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BalanceId,Category,Description,Amount,Date")] Spending spending)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spending);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var spending = await _context.Spendings.FindAsync(id);
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
                    _context.Update(spending);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpendingExists(spending.Id))
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

            var spending = await _context.Spendings
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var spending = await _context.Spendings.FindAsync(id);
            _context.Spendings.Remove(spending);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpendingExists(int id)
        {
            return _context.Spendings.Any(e => e.Id == id);
        }
    }
}
