using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Partys.Controllers;
using Partys.Data;
using Partys.Models;
using Xunit;

namespace Partys.Tests
{
    public class PartiesControllerTests
    {
        private ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        //In this test we are checking soft delete
        
        [Fact]
        public void Index_ReturnsView_WithNonDeletedParties()
        {
            
            var context = GetContext();
            context.Parties.Add(new Party
            {
                Description = "Visible Party",
                EventDate = DateTime.Now.AddDays(1),
                Location = "Test",
                IsDeleted = false
            });
            context.Parties.Add(new Party
            {
                Description = "Deleted Party",
                EventDate = DateTime.Now.AddDays(2),
                Location = "Test",
                IsDeleted = true
            });
            context.SaveChanges();

            var controller = new PartiesController(context);

            
            var result = controller.Index();

            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Party>>(viewResult.Model);

            Assert.Single(model); 
            Assert.Equal("Visible Party", model.First().Description);
        }
    }
}
