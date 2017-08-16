using EasySleepApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasySleepApi.Initializer
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {

            /* Offer off = new Offer()
            {
                IsActive = true,
                Description = "Une super description",
                MaxPeople = 7,
                Owner = new ApplicationUser()
                {
                    Id = null,
                    UserName = "test@test.com",
                    LastName = "Mergalet",
                    FirstName = "Adrien",
                    Password = "Testtest",
                    ConfirmPassword = "Testtest",
                    Email = "test@test.com",
                    BornDate = "1917-01-01T12:00:00.0914156",
                    PhoneNumber = "0478987456",
                    IsBanned = false,
                    ProfilePicture = null,
                    HasRegistered = false,
                    LoginProvider = null
                },
                Location = new Location()
                {
                    Street = "Rue des cerisiers",
                    HouseNum = 43,
                    PostalCode = 5000,
                    City = "Namur"
                }
            };

            context.Offers.Add(off);
            context.SaveChanges(); */
        }

    }
}