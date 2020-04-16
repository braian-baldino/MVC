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
    public class ESpendingTypeController : Controller
    {
        private readonly AccountantContext _context;

        public ESpendingTypeController(AccountantContext context)
        {
            _context = context;
        }

        // GET: ESpendingType
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpendingTypes.ToListAsync());
        }

        // GET: ESpendingType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSpendingType = await _context.SpendingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eSpendingType == null)
            {
                return NotFound();
            }

            return View(eSpendingType);
        }

        // GET: ESpendingType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ESpendingType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName")] ESpendingType eSpendingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eSpendingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eSpendingType);
        }

        // GET: ESpendingType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSpendingType = await _context.SpendingTypes.FindAsync(id);
            if (eSpendingType == null)
            {
                return NotFound();
            }
            return View(eSpendingType);
        }

        // POST: ESpendingType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName")] ESpendingType eSpendingType)
        {
            if (id != eSpendingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eSpendingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ESpendingTypeExists(eSpendingType.Id))
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
            return View(eSpendingType);
        }

        // GET: ESpendingType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSpendingType = await _context.SpendingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eSpendingType == null)
            {
                return NotFound();
            }

            return View(eSpendingType);
        }

        // POST: ESpendingType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eSpendingType = await _context.SpendingTypes.FindAsync(id);
            _context.SpendingTypes.Remove(eSpendingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ESpendingTypeExists(int id)
        {
            return _context.SpendingTypes.Any(e => e.Id == id);
        }
    }
}
