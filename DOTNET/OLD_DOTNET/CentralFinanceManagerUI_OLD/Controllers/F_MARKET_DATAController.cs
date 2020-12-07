using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentralFinanceManagerUI.Data;
using CentralFinanceManagerUI.Models;

namespace CentralFinanceManagerUI.Controllers
{
    public class F_MARKET_DATAController : Controller
    {
        private readonly SqlDbContext _context;

        public F_MARKET_DATAController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: F_MARKET_DATA
        public async Task<IActionResult> Index()
        {
            return View(await _context.F_MARKET_DATA.ToListAsync());
        }

        // GET: F_MARKET_DATA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var f_MARKET_DATA = await _context.F_MARKET_DATA
                .FirstOrDefaultAsync(m => m.ColumnID == id);
            if (f_MARKET_DATA == null)
            {
                return NotFound();
            }

            return View(f_MARKET_DATA);
        }

        // GET: F_MARKET_DATA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: F_MARKET_DATA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColumnID,Symbol,Open,High,Low,Close,Name,AGG_Volume,Market_Timestamp")] F_MARKET_DATA f_MARKET_DATA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(f_MARKET_DATA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(f_MARKET_DATA);
        }

        // GET: F_MARKET_DATA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var f_MARKET_DATA = await _context.F_MARKET_DATA.FindAsync(id);
            if (f_MARKET_DATA == null)
            {
                return NotFound();
            }
            return View(f_MARKET_DATA);
        }

        // POST: F_MARKET_DATA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColumnID,Symbol,Open,High,Low,Close,Name,AGG_Volume,Market_Timestamp")] F_MARKET_DATA f_MARKET_DATA)
        {
            if (id != f_MARKET_DATA.ColumnID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(f_MARKET_DATA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!F_MARKET_DATAExists(f_MARKET_DATA.ColumnID))
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
            return View(f_MARKET_DATA);
        }

        // GET: F_MARKET_DATA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var f_MARKET_DATA = await _context.F_MARKET_DATA
                .FirstOrDefaultAsync(m => m.ColumnID == id);
            if (f_MARKET_DATA == null)
            {
                return NotFound();
            }

            return View(f_MARKET_DATA);
        }

        // POST: F_MARKET_DATA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var f_MARKET_DATA = await _context.F_MARKET_DATA.FindAsync(id);
            _context.F_MARKET_DATA.Remove(f_MARKET_DATA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool F_MARKET_DATAExists(int id)
        {
            return _context.F_MARKET_DATA.Any(e => e.ColumnID == id);
        }
    }
}
