using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Job_Hunter_Trapper_Keeper.Data;
using Job_Hunter_Trapper_Keeper.Models;
using Job_Hunter_Trapper_Keeper.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Job_Hunter_Trapper_Keeper.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompaniesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Company.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var c = new CompanyNotesViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include("CompanyNotes")
                .SingleOrDefaultAsync(m => m.Id == id);

            c.Id = company.Id;
            c.CompanyName = company.Name;
            c.Address = company.Address;
            c.City = company.City;
            c.State = company.State;
            c.Zip = company.Zip;
            c.Phone = company.Phone;
            c.CompanyNotes = company.CompanyNotes;

            if (company == null)
            {
                return NotFound();
            }


            return View(c);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateViewModel company)
        {
            //create new instance of company
            var c = new Company()
            {
                Name = company.Name,
                Address = company.Address,
                City = company.City,
                State = company.State,
                Zip = company.Zip,
                Phone = company.Phone
            };

            _context.Company.Add(c);
            _context.SaveChanges();

            //create new instance of CompanyNote
            var note = new CompanyNotes()
            {
                User = await _userManager.GetUserAsync(User),
                CompanyId = c.Id,
                Notes = company.Note
            };

            _context.CompanyNotes.Add(note);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Jobs/Add Note/5
        public async Task<IActionResult> AddNote(int companyId)
        {

            var company = await _context.Company
                .Include("CompanyNotes")
                .SingleOrDefaultAsync(m => m.Id == companyId);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(int companyId, CompanyNotes companyNote)
        {
            var note = new CompanyNotes()
            {
                User = await _userManager.GetUserAsync(User),
                CompanyId = companyId,
                Notes = companyNote.Notes,
            };

            if (companyId != companyNote.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(companyNote.Id))
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
            return View(companyNote);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var c = new CompanyNotesViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include("CompanyNotes")
                .SingleOrDefaultAsync(m => m.Id == id);

            c.Id = company.Id;
            c.CompanyName = company.Name;
            c.Address = company.Address;
            c.City = company.City;
            c.State = company.State;
            c.Zip = company.Zip;
            c.Phone = company.Phone;
            c.CompanyNotes = company.CompanyNotes;

            if (company == null)
            {
                return NotFound();
            }


            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyNotesViewModel company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var c = new CompanyNotesViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include("CompanyNotes")
                .SingleOrDefaultAsync(m => m.Id == id);

            c.Id = company.Id;
            c.CompanyName = company.Name;
            c.Address = company.Address;
            c.City = company.City;
            c.State = company.State;
            c.Zip = company.Zip;
            c.Phone = company.Phone;
            c.CompanyNotes = company.CompanyNotes;

            if (company == null)
            {
                return NotFound();
            }


            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Company.SingleOrDefaultAsync(m => m.Id == id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.Id == id);
        }
    }
}
