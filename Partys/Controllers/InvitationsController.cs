using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Partys.Services;
using Partys.Data;
using Partys.Models;

namespace Partys.Controllers
{
    [Authorize] // Restrict access to logged-in users unless overridden
    public class InvitationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public InvitationsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Show the form to create an invitation for a given party
        public IActionResult Create(int partyId)
        {
            var party = _context.Parties.Find(partyId);

            if (party == null || party.IsDeleted)
                return NotFound();

            ViewBag.PartyId = partyId;
            ViewBag.PartyDescription = party.Description;

            return View();
        }

        // Handle form submission for creating an invitation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                invitation.Status = "Pending";
                invitation.IsSent = false;

                _context.Invitations.Add(invitation);
                _context.SaveChanges();

                return RedirectToAction("Details", "Parties", new { id = invitation.PartyId });
            }

            var party = _context.Parties.Find(invitation.PartyId);
            ViewBag.PartyId = invitation.PartyId;
            ViewBag.PartyDescription = party?.Description;

            return View(invitation);
        }

        // Sends an email for a specific invitation
        public async Task<IActionResult> Send(int id)
        {
            var invitation = _context.Invitations
                .Include(i => i.Party)
                .FirstOrDefault(i => i.Id == id);

            if (invitation == null)
                return NotFound();

            if (invitation.IsSent)
            {
                TempData["Message"] = "This invitation has already been sent.";
                return RedirectToAction("Details", "Parties", new { id = invitation.PartyId });
            }

            try
            {
                await _emailService.SendInvitationEmail(
                    invitation.GuestEmail!,
                    invitation.GuestName!,
                    invitation.Party!.Description!,
                    invitation.Party.EventDate,
                    invitation.Id
                );

                invitation.IsSent = true;
                invitation.SentDate = DateTime.Now;

                _context.SaveChanges();

                TempData["Message"] = $"Invitation sent to {invitation.GuestEmail}!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Email failed: {ex.Message}";
            }

            return RedirectToAction("Details", "Parties", new { id = invitation.PartyId });
        }
        
        public IActionResult Respond(int id, string response)
        {
            var invitation = _context.Invitations
                .Include(i => i.Party)
                .FirstOrDefault(i => i.Id == id);

            if (invitation == null)
                return NotFound();

            if (response == "accept")
                invitation.Status = "Accepted";
            else if (response == "decline")
                invitation.Status = "Declined";

            _context.SaveChanges();

            if (User.Identity.IsAuthenticated)
            {
                TempData["Message"] = $"You have {invitation.Status} the invitation to {invitation.Party.Description}";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Response = invitation.Status;
            ViewBag.PartyDescription = invitation.Party.Description;
            return View();
        }
    }
}