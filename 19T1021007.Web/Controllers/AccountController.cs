using _19T1021007.BusinessLayers;
using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _19T1021007.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// giao diện đăng nhập vào hệ thống
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName = "", string password = "")
        {
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)) 
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View();
            }

            var userAccount = UserAccountService.Authorize(AccountTypes.Employee,userName, password);
            if(userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }
            string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(string userName = "", string oldPassword = "", string newPassword = "", string confirmPassword = "")
        {

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View("Index");
            }

            if(newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Vui lòng nhập mật khẩu mới trùng với mật khẩu xác nhận");
                return View("Index");
            }


            if (!UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword))
            {
                ModelState.AddModelError("", "Đổi mật khẩu không thành công. Vui lòng kiểm tra lại");
                return View("Index");
            }
            ModelState.AddModelError("", "Đổi mật khẩu thành công");
            return RedirectToAction("Login");
        }
    }
}