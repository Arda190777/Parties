using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Partys.Data;
using Partys.Models;

namespace Partys.Controllers
{

    public class PartiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Parties
        [AllowAnonymous]
        public IActionResult Index()
        {
            var parties = _context.Parties
                .Where(p => !p.IsDeleted)
                .Include(p => p.Invitations)
                .OrderBy(p => p.EventDate)
                .ToList();

            return View(parties);
        }

        // GET: /Parties/Details
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var party = _context.Parties
                .Include(p => p.Invitations)
                .FirstOrDefault(p => p.Id == id && !p.IsDeleted);

            if (party == null)
                return NotFound();

            return View(party);
        }

        // GET: /Parties/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Parties/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] //Prevents unauthorized form submissions
        public IActionResult Create(Party party)
        {
            if (ModelState.IsValid)
            {
                _context.Parties.Add(party);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(party);
        }

        // GET: /Parties/Edit
        [Authorize]
        public IActionResult Edit(int id)
        {
            var party = _context.Parties.Find(id);

            if (party == null || party.IsDeleted)
                return NotFound();

            return View(party);
        }

        // POST: /Parties/Edit
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken] // Prevents unauthorized form submissions
        public IActionResult Edit(int id, Party party)
        {
            if (id != party.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(party);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(party);
        }

        // GET: /Parties/Delete
        // Admin only
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var party = _context.Parties.FirstOrDefault(p => p.Id == id);

            if (party == null)
                return NotFound();

            return View(party);
        }

        // POST: /Parties/Delete
        // Soft delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken] // Prevents unauthorized form submissions
        public IActionResult DeleteConfirmed(int id)
        {
            var party = _context.Parties.Find(id);


            

            if (party != null)
            {
                party.IsDeleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: /Parties/Restore
        // Admin-only
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            var party = _context.Parties.FirstOrDefault(p => p.Id == id);

            if (party != null && party.IsDeleted)
            {
                party.IsDeleted = false;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}