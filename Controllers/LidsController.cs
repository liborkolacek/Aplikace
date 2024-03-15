using Aplikace.Databaze;
using Aplikace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aplikace.Controllers
{
    public class LidsController : Controller
    {
        private readonly PropojeniDB _context;

        public LidsController(PropojeniDB context)
        {
            _context = context;
        }

        // 
        public async Task<IActionResult> Index()
        {
            var lids = await _context.Lid.OrderBy(l => l.Size).ToListAsync();
            return View(lids);
        }

        // získaní detailu pokliček
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lid = await _context.Lid
                .FirstOrDefaultAsync(m => m.LidId == id);
            if (lid == null)
            {
                return NotFound();
            }

            return View(lid);
        }

        // Vytvoření hrnce
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hrnce/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazev,Velikost")] Lid lid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lid);
        }

        // editace pokliček
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lid = await _context.Lid.FindAsync(id);
            if (lid == null)
            {
                return NotFound();
            }
            return View(lid);
        }

        // Editace pokliček
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazev,Velikost")] Lid lid)
        {
            if (id != lid.LidId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LidExists(lid.LidId))
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
            return View(lid);
        }

        // Odstranění hrnce
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lid = await _context.Lid
                .FirstOrDefaultAsync(m => m.LidId == id);
            if (lid == null)
            {
                return NotFound();
            }

            return View(lid);
        }

        // Odstranění Hrnců
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lid = await _context.Lid.FindAsync(id);
            _context.Lid.Remove(lid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LidExists(int id)
        {
            return _context.Lid.Any(e => e.LidId == id);
        }
    }
}
