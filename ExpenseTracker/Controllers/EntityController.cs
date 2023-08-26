using ExpenseTracker.DB;
using ExpenseTracker.DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    public class EntityController : Controller
    {
        private readonly ExpenseContext _context;

        public EntityController(ExpenseContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return RedirectToAction("FilterByDateRange", new { startDate, endDate });
            }

            var entries = _context.Entrys.Include(e => e.Category).ToList();
            return View(entries);
        }



        // GET: ExpenseEntry/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatID, Amount, EntryDate, Notes")] ExpenseEntry entry)
        {
            
                if (entry.EntryDate > DateTime.Now)
                {
                    ModelState.AddModelError("EntryDate", "Expenditure date cannot be a future date.");
                    ViewBag.Categories = _context.Categories.ToList();
                    return View(entry);
                }

                entry.EntryDate = DateTime.Now;
                _context.Entrys.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

            ViewBag.Categories = _context.Categories.ToList();
            return View(entry);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entrys.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, CatID, Amount, EntryDate, Notes")] ExpenseEntry entry)
        {
            if (id != entry.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseEntryExists(entry.ID))
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

            ViewBag.Categories = _context.Categories.ToList();
            return View(entry);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entrys
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _context.Entrys.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entrys.Remove(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseEntryExists(int id)
        {
            return _context.Entrys.Any(e => e.ID == id);
        }
        public async Task<IActionResult> FilterByDateRange(DateTime startDate, DateTime endDate)
        {
            var entries = await _context.Entrys
                .Include(e => e.Category)
                .Where(e => e.EntryDate >= startDate && e.EntryDate <= endDate)
                .ToListAsync();

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View("Index", entries);
        }



    }
}
