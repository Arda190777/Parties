using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Partys.Data;
using Partys.Models;

namespace Partys.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _partydb;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _partydb = context;
        }

        public IActionResult Index()
        {
            // default: no invitations
            var pendingInvitations = new List<Invitation>();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;

                pendingInvitations = _partydb.Invitations
                    .Include(i => i.Party)
                    .Where(i =>
                        i.GuestEmail == userEmail &&
                        i.IsSent &&
                        i.Status == "Pending")
                    .ToList();
            }

            // send the list of Invitation to the view
            return View(pendingInvitations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}