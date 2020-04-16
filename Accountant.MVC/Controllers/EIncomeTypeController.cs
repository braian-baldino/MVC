using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Accountant.MVC.Context;
using Accountant.MVC.Models.DropDowns;

namespace Accountant.MVC.Controllers
{
    public class EIncomeTypeController : Controller
    {
        private readonly AccountantContext _context;

        public EIncomeTypeController(AccountantContext context)
        {
            _context = context;
        }

        // GET: EIncomeType
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncomeTypes.ToListAsync());
        }

        // GET: EIncomeType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eIncomeType = await _context.IncomeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eIncomeType == null)
            {
                return NotFound();
            }

            return View(eIncomeType);
        }

        // GET: EIncomeType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EIncomeType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName")] EIncomeType eIncomeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eIncomeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eIncomeType);
        }

        // GET: EIncomeType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eIncomeType = await _context.IncomeTypes.FindAsync(id);
            if (eIncomeType == null)
            {
                return NotFound();
            }
            return View(eIncomeType);
        }

        // POST: EIncomeType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName")] EIncomeType eIncomeType)
        {
            if (id != eIncomeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eIncomeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EIncomeTypeExists(eIncomeType.Id))
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
            return View(eIncomeType);
        }

        // GET: EIncomeType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eIncomeType = await _context.IncomeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eIncomeType == null)
            {
                return NotFound();
            }

            return View(eIncomeType);
        }

        // POST: EIncomeType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eIncomeType = await _context.IncomeTypes.FindAsync(id);
            _context.IncomeTypes.Remove(eIncomeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EIncomeTypeExists(int id)
        {
            return _context.IncomeTypes.Any(e => e.Id == id);
        }
    }
}
