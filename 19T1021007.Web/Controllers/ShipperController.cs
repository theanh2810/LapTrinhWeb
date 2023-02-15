using _19T1021007.BusinessLayers;
using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021007.Web.Controllers
{
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 10;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(page, PAGE_SIZE, searchValue, out rowCount);
            int pageCount = rowCount / PAGE_SIZE;
            if (rowCount % PAGE_SIZE > 0)
            {
                pageCount++;
            }
            ViewBag.Page = page;
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;
            return View(data);
        }
        /// <summary>
        /// thêm người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung người giao hàng";

            Shipper data = new Shipper()
            {
                ShipperID = 0
            };

            return View("Edit", data);
        }
        /// <summary>
        /// sửa người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Cập nhập người giao hàng";

            int ShipperID = Convert.ToInt32(id);

            var data = CommonDataService.GetShipper(ShipperID);
            return View(data);
        }
        /// <summary>
        /// xóa người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int ShipperID = Convert.ToInt32(id);

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(ShipperID);
                return RedirectToAction("Index");
            }
            else
            {
                var data = CommonDataService.GetShipper(ShipperID);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Save(Shipper data)
        {
            if (data.ShipperID == 0)
            {
                CommonDataService.AddShipper(data);
            }
            else
            {
                CommonDataService.UpdateShipper(data);
            }
            return RedirectToAction("Index");
        }
    }
}