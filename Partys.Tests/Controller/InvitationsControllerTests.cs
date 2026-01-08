using Xunit;
using Microsoft.EntityFrameworkCore;
using Partys.Controllers;
using Partys.Data;
using Partys.Models;

namespace Partys.Tests.Controllers
{
    public class InvitationsControllerTests
    {
        private ApplicationDbContext GetDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InvitationsTestDb")
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Respond_Accept_Changes_Status_To_Accepted()
        {
            // Arrange
            var db = GetDb();

            var party = new Party
            {
                Description = "Test Party",
                EventDate = DateTime.Now
            };
            db.Parties.Add(party);
            db.SaveChanges();

        }
    }
}
