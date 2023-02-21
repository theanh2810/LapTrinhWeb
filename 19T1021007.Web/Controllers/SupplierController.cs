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
        private const string SUPPLIER_SEARCH = "SupplierSearchCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as Models.PaginationSearchInput;

            if (condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                };
            };

            return View(condition);
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
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new Models.SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["SupplierSearchCondition"] = condition;
            return View(result);
        }
    }
}