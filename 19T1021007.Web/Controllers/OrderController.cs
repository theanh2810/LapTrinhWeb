using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021007.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
       /// <summary>
       /// Xử lý đơn hàng
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Lập đơn hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
    }
}