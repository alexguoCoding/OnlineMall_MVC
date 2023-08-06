using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
        public IActionResult Index()
        {
         

            return View();
        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            List<ApplicationUser> ApplicationUserList = _unitOfWork.ApplicationUser.GetAll().ToList();
            var userroles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in ApplicationUserList) {
                var userId = userroles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role= roles.FirstOrDefault(u => u.Id == userId).Name;

            }


            return Json(new { data = ApplicationUserList });
        }
        #endregion



    }



}
