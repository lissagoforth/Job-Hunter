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
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            JobListViewModel model = new JobListViewModel();
            model.Jobs = await (from j in _context.Job
                          join c in _context.Company
                          on j.CompanyId equals c.Id
                          select new JobViewModel
                          {
                              Id = j.Id,
                              Company = c.Name,
                              JobTitle = j.JobTitle
                          }).ToListAsync();
            return View(model);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var j = new JobViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include("JobNotes")
                .Include("Company")
                .SingleOrDefaultAsync(m => m.Id == id);

            j.Id = job.Id;
            j.Company = job.Company.Name;
            j.JobTitle = job.JobTitle;
            j.JobNotes = job.JobNotes;
             
            if (job == null)
            {
                return NotFound();
            }


            return View(j);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( JobCreateViewModel job)
        {
            //create new instance of company

            var c = new Company();
            if (_context.Company.Any(com => com.Name == job.Company))
            {
                c = _context.Company.SingleOrDefault(com => com.Name == job.Company);
            }

            else
            {
                c.Name = job.Company;
                _context.Company.Add(c);
                _context.SaveChanges();
            }
            
            //create new instance of job
            var j = new Job()
            {
                CompanyId = c.Id,
                JobTitle = job.JobTitle
            };

            _context.Job.Add(j);
            _context.SaveChanges();

            //create new instance of jobnote
            var note = new JobNotes()
            {
                User = await _userManager.GetUserAsync(User),
                JobId = j.Id,
                Notes = job.Note,
            };

            _context.JobNotes.Add(note);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Jobs/Add Note/5
        public async Task<IActionResult> AddNote(int jobId)
        {
            var note = new JobNotes();

            var job = await _context.Job
                .Include("JobNotes")
                .SingleOrDefaultAsync(m => m.Id == jobId);

            note.User = await _userManager.GetUserAsync(User);
            note.JobId = jobId;
            note.Notes = "this is a hard-coded test note";

            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(int jobId, JobNotes jobNote)
        {
            var note = new JobNotes()
            {
                User =  await _userManager.GetUserAsync(User),
                JobId = jobId,
                Notes = jobNote.Notes,
            };

            if (jobId != jobNote.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(jobNote.Id))
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
            return View(note);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var j = new JobViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include("JobNotes")
                .Include("Company")
                .SingleOrDefaultAsync(m => m.Id == id);

            j.Id = job.Id;
            j.Company = job.Company.Name;
            j.JobTitle = job.JobTitle;
            j.JobNotes = job.JobNotes;

            if (job == null)
            {
                return NotFound();
            }


            return View(j);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobViewModel job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var j = new JobViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include("JobNotes")
                .Include("Company")
                .SingleOrDefaultAsync(m => m.Id == id);

            j.Id = job.Id;
            j.Company = job.Company.Name;
            j.JobTitle = job.JobTitle;
            j.JobNotes = job.JobNotes;

            if (job == null)
            {
                return NotFound();
            }


            return View(j);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job
                .Include("JobNotes")
                .SingleOrDefaultAsync(m => m.Id == id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}
