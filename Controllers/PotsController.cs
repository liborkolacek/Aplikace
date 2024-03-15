using Aplikace.Databaze;
using Aplikace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Aplikace.Controllers
{
    public class PotsController : Controller
    {
        private readonly PropojeniDB _context;

        public PotsController(PropojeniDB context)
        {
            _context = context;
        }

        // 
        public async Task<IActionResult> Index()
        {
            var pots = await _context.Pot.OrderBy(p => p.Size).ToListAsync();
            return View(pots);
        }
        // získaní detailu hrnce
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pot = await _context.Pot
                .FirstOrDefaultAsync(m => m.PotId == id);
            if (pot == null)
            {
                return NotFound();
            }

            return View(pot);
        }

        // Vytvoření hrnce
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hrnce/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazev,Velikost")] Pot pot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pot);
        }

        // editace hrnců
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pot = await _context.Pot.FindAsync(id);
            if (pot == null)
            {
                return NotFound();
            }
            return View(pot);
        }

        // POST: Hrnce/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazev,Velikost")] Pot pot)
        {
            if (id != pot.PotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PotExists(pot.PotId))
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
            return View(pot);
        }

        // Odstranění hrnce
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hrnce = await _context.Pot
                .FirstOrDefaultAsync(m => m.PotId == id);
            if (hrnce == null)
            {
                return NotFound();
            }

            return View(hrnce);
        }

        // Odstranění Hrnců
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pot = await _context.Pot.FindAsync(id);
            _context.Pot.Remove(pot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PotExists(int id)
        {
            return _context.Pot.Any(e => e.PotId == id);
        }
    }
}
