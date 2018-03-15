using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Hunter_Trapper_Keeper.Data;
using Job_Hunter_Trapper_Keeper.Models;
using Microsoft.AspNetCore.Identity;
using Job_Hunter_Trapper_Keeper.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Job_Hunter_Trapper_Keeper.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contact.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var c = new ContactNotesViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include("ContactNotes")
                .SingleOrDefaultAsync(m => m.Id == id);

            var fullname = (contact.FirstName + " " + contact.LastName);

            c.Id = contact.Id;
            c.ContactName = fullname;
            c.Email = contact.Email;
            c.Phone = contact.Phone;
            c.ContactNotes = contact.ContactNotes;

            if (contact == null)
            {
                return NotFound();
            }


            return View(c);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ContactCreateViewModel contact)
        {
            //create new instance of contact
            var c = new Contact()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone
            };

            _context.Contact.Add(c);
            _context.SaveChanges();

            //create new instance of jobnote
            var note = new ContactNotes()
            {
                User = await _userManager.GetUserAsync(User),
                ContactId = c.Id,
                Notes = contact.Note
            };

            _context.ContactNotes.Add(note);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var c = new ContactNotesViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include("ContactNotes")
                .SingleOrDefaultAsync(m => m.Id == id);

            var fullname = (contact.FirstName + " " + contact.LastName);

            c.Id = contact.Id;
            c.ContactName = fullname;
            c.Email = contact.Email;
            c.Phone = contact.Phone;
            c.ContactNotes = contact.ContactNotes;

            if (contact == null)
            {
                return NotFound();
            }


            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        // GET: Jobs/Add Note/5
        public async Task<IActionResult> AddNote(int id)
        {
            var c = new ContactAddNoteViewModel();

            var contact = await _context.Contact
                .Include("ContactNotes")
                .SingleOrDefaultAsync(m => m.Id == id);

            var fullname = (contact.FirstName + " " + contact.LastName);
            c.User = await _userManager.GetUserAsync(User);
            c.Id = contact.Id;
            c.ContactName = fullname;

            if (contact == null)
            {
                return NotFound();
            }
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(int id, ContactAddNoteViewModel contactNote)
        {
            var note = new ContactAddNoteViewModel()
            {
                User = await _userManager.GetUserAsync(User),
                Id = id,
                ContactName = contactNote.ContactName
            };

            var cn = new ContactNotes()
            {
                User = note.User,
                ContactId = id,
                Notes = note.Note
            };

            if (id != contactNote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ContactNotes.Add(cn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contactNote.Id))
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
            return View(contactNote);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var c = new ContactNotesViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include("ContactNotes")
                .SingleOrDefaultAsync(m => m.Id == id);

            var fullname = (contact.FirstName + " " + contact.LastName);

            c.Id = contact.Id;
            c.ContactName = fullname;
            c.Email = contact.Email;
            c.Phone = contact.Phone;
            c.ContactNotes = contact.ContactNotes;

            if (contact == null)
            {
                return NotFound();
            }


            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.Id == id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}
