using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PaymentHistoryController : Controller
    {
        private readonly ILogger<PaymentHistoryController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentHistoryController(ILogger<PaymentHistoryController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

          
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<OrderHeader>  OrderList = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == userId).ToList();
           
            List<OrderVM>  History=new List<OrderVM>();
            foreach (var order in OrderList)
            {
                var orderid = order.Id;
                OrderVM orderVM = new OrderVM();
                orderVM.OrderHeader = order;
                List<OrderDetail> OrderDetailsList = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderid,includeProperties:"Product").ToList();

                orderVM.OrderDetail= OrderDetailsList;

                History.Add(orderVM);
            }



            return View(History);
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