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
    public class ContactNotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactNotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactNotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactNotes.ToListAsync());
        }

        // GET: ContactNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactNotes = await _context.ContactNotes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contactNotes == null)
            {
                return NotFound();
            }

            return View(contactNotes);
        }

        // GET: ContactNotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Notes")] ContactNotes contactNotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactNotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactNotes);
        }

        // GET: ContactNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactNotes = await _context.ContactNotes.SingleOrDefaultAsync(m => m.Id == id);
            if (contactNotes == null)
            {
                return NotFound();
            }
            return View(contactNotes);
        }

        // POST: ContactNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Notes")] ContactNotes contactNotes)
        {
            if (id != contactNotes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactNotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactNotesExists(contactNotes.Id))
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
            return View(contactNotes);
        }

        // GET: ContactNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactNotes = await _context.ContactNotes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contactNotes == null)
            {
                return NotFound();
            }

            return View(contactNotes);
        }

        // POST: ContactNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactNotes = await _context.ContactNotes.SingleOrDefaultAsync(m => m.Id == id);
            _context.ContactNotes.Remove(contactNotes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactNotesExists(int id)
        {
            return _context.ContactNotes.Any(e => e.Id == id);
        }
    }
}
