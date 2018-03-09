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
    public class JobNotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobNotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobNotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobNotes.ToListAsync());
        }

        // GET: JobNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNotes = await _context.JobNotes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobNotes == null)
            {
                return NotFound();
            }

            return View(jobNotes);
        }

        // GET: JobNotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Notes")] JobNotes jobNotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobNotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobNotes);
        }

        // GET: JobNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNotes = await _context.JobNotes.SingleOrDefaultAsync(m => m.Id == id);
            if (jobNotes == null)
            {
                return NotFound();
            }
            return View(jobNotes);
        }

        // POST: JobNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Notes")] JobNotes jobNotes)
        {
            if (id != jobNotes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobNotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobNotesExists(jobNotes.Id))
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
            return View(jobNotes);
        }

        // GET: JobNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNotes = await _context.JobNotes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobNotes == null)
            {
                return NotFound();
            }

            return View(jobNotes);
        }

        // POST: JobNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobNotes = await _context.JobNotes.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobNotes.Remove(jobNotes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobNotesExists(int id)
        {
            return _context.JobNotes.Any(e => e.Id == id);
        }
    }
}
