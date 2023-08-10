using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PersonalInformationController : Controller
    {
        private readonly ILogger<PersonalInformationController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PersonalInformationController(ILogger<PersonalInformationController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string CategoryName)
        {

          
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser Info = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            PurchasePoint check = _unitOfWork.PurchasePoint.Get(u => u.ApplicationUserId == userId);
            if(check == null) {
                check = new PurchasePoint();
                check.ApplicationUserId = userId;
                check.Point =0;
                _unitOfWork.PurchasePoint.Add(check);
            }
            _unitOfWork.Save();
            ComsumerInfoVM Vm= new ComsumerInfoVM();
            Vm.ApplicationUser = Info;
            Vm.PurchasePoint = check;


            return View(Vm);
        }




     
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}