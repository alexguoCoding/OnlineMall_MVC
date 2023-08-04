using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.DbInitializer {
    public class DbInitializer : IDbInitializer {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db) {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }


        public void Initialize() {


            //migrations if they are not applied
            try {
                if (_db.Database.GetPendingMigrations().Count() > 0) {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex) { }



            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult()) {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();


                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser {
                    UserName = "admin@dotnetmastery.com",
                    Email = "admin@dotnetmastery.com",
                    Name = "admin",
                    PhoneNumber = "1112223333",
                    StreetAddress = "test 123 Ave",
                    State = "IL",
                    PostalCode = "23422",
                    City = "Chicago"
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dotnetmastery.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();


                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "comsumer1@gmail.com",
                    Email = "comsumer1@gmail.com",
                    Name = "comsumer1",
                    PhoneNumber = "1111111111",
                    StreetAddress = "test 123 Ave",
                    State = "AL",
                    PostalCode = "T6H 5J1",
                    City = "Edmonton"
                }, "Consumer123*").GetAwaiter().GetResult();


                 user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "comsumer1@gmail.com");

                _userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "comsumer2@gmail.com",
                    Email = "comsumer2@gmail.com",
                    Name = "comsumer2",
                    PhoneNumber = "222222",
                    StreetAddress = "test 123 Ave",
                    State = "AL",
                    PostalCode = "T6H 5J1",
                    City = "Edmonton"
                }, "Consumer123*").GetAwaiter().GetResult();


                user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "comsumer2@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();


            }

            return;
        }
    }
}
