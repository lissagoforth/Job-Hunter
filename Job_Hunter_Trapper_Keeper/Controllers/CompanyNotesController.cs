using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Hunter_Trapper_Keeper.Data;
using Job_Hunter_Trapper_Keeper.Models;

namespace Job_Hunter_Trapper_Keeper.Controllers
{
    public class CompanyNotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyNotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompanyNotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompanyNotes.ToListAsync());
        }

        // GET: CompanyNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyNotes = await _context.CompanyNotes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (companyNotes == null)
            {
                return NotFound();
            }

            return View(companyNotes);
        }

        // GET: CompanyNotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Notes")] CompanyNotes companyNotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyNotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyNotes);
        }

        // GET: CompanyNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyNotes = await _context.CompanyNotes.SingleOrDefaultAsync(m => m.Id == id);
            if (companyNotes == null)
            {
                return NotFound();
            }
            return View(companyNotes);
        }

        // POST: CompanyNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Notes")] CompanyNotes companyNotes)
        {
            if (id != companyNotes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyNotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyNotesExists(companyNotes.Id))
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
            return View(companyNotes);
        }

        // GET: CompanyNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyNotes = await _context.CompanyNotes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (companyNotes == null)
            {
                return NotFound();
            }

            return View(companyNotes);
        }

        // POST: CompanyNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyNotes = await _context.CompanyNotes.SingleOrDefaultAsync(m => m.Id == id);
            _context.CompanyNotes.Remove(companyNotes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyNotesExists(int id)
        {
            return _context.CompanyNotes.Any(e => e.Id == id);
        }
    }
}
