using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebAppDataProvider;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        #region [ Fields ]
        private readonly ILogger<HomeController> _logger;
        private readonly IMemberDataProvider _memberDataProvider;
        private readonly AdminAccount _adminAccount;
        #endregion

        #region [ CTor ]
        public HomeController(  ILogger<HomeController> logger,
                                IMemberDataProvider memberDataProvider,
                                AdminAccount adminAccount) {
            this._logger = logger;
            this._memberDataProvider = memberDataProvider;
            this._adminAccount = adminAccount;
        }
        #endregion
        public IActionResult Index() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password) {
            var tempMember = _memberDataProvider.Login(email, password);
            var adminLogin = (_adminAccount.Email == email && _adminAccount.Password == password);
            
            try {
                if (ModelState.IsValid) {
                    if (adminLogin) {
                        HttpContext.Session.SetString("UserType", "admin");
                        return RedirectToAction("Index","Product");
                    } else if (tempMember != null) {
                        HttpContext.Session.SetString("UserType", tempMember.MemberId.ToString());
                        HttpContext.Session.SetInt32("UserId", tempMember.MemberId);
                        return RedirectToAction("OrderByMember", "Order");
                    } else {
                        ViewBag.error = "Invalid Email Address or Password.";
                        return RedirectToAction("Index");
                    }
                } return View();
            } catch (Exception ex) {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout() {
            HttpContext.Session.Remove("UserType");
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index");
        }
    }
}
