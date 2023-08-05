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
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<ApplicationUser> ApplicationUserList = _unitOfWork.ApplicationUser.GetAll().ToList();

            return View(ApplicationUserList);
        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            List<ApplicationUser> ApplicationUserList = _unitOfWork.ApplicationUser.GetAll().ToList();
            return Json(new { data = ApplicationUserList });
        }
        #endregion



    }



}
