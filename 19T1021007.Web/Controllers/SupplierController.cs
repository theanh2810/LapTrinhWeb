using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021007.DomainModels;
using _19T1021007.BusinessLayers;

namespace _19T1021007.Web.Controllers
{
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 10;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue="")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers( page, PAGE_SIZE, searchValue,out rowCount);
            int pageCount = rowCount / PAGE_SIZE;
            if(rowCount%PAGE_SIZE > 0)
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
        /// thêm nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp";

            Supplier data = new Supplier()
            {
                SupplierID = 0
            };

            return View("Edit", data);
        }
        /// <summary>
        /// sửa nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Cập nhập nhà cung cấp";

            int SupplierID = Convert.ToInt32(id);

            var data = CommonDataService.GetSupplier(SupplierID);
            return View(data);
        }
        /// <summary>
        /// xóa nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id) 
        {
            int SupplierID = Convert.ToInt32(id);
            
            if(Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(SupplierID);
                return RedirectToAction("Index");
            }
            else
            {
                var data = CommonDataService.GetSupplier(SupplierID);
                return View(data);
            }
            
        }
        [HttpPost]
        public ActionResult Save(Supplier data)
        {
            if(data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index");
        }
    }
}