using _19T1021007.BusinessLayers;
using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace _19T1021007.Web.Controllers
{
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string CUSTOMER_SEARCH = "CustomerSearchCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as Models.PaginationSearchInput;

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
        /// thêm khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            Customer data = new Customer()
            {
                CustomerID = 0
            };

            return View("Edit", data);
        }
        /// <summary>
        /// sửa khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCustomer(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhập khách hàng";
            return View(data);
        }
        /// <summary>
        /// xóa khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int CustomerID = Convert.ToInt32(id);

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(CustomerID);
                return RedirectToAction("Index");
            }
            else
            {
                var data = CommonDataService.GetCustomer(CustomerID);
                return View(data);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer data)
        {
            if (string.IsNullOrWhiteSpace(data.CustomerName))
            {
                ModelState.AddModelError(nameof(data.CustomerName), "Tên khách hàng không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.ContactName))
            {
                ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Country))
            {
                ModelState.AddModelError(nameof(data.Country), "Quốc gia không được để trống");
            }
            data.Address = data.Address ?? "";
            data.City = data.City ?? "";
            data.PostalCode = data.PostalCode ?? "";
            data.Email = data.Email ?? "";

            if (!ModelState.IsValid)
            {
                if (data.CustomerID == 0)
                {
                    ViewBag.Title = "Bổ sung khách hàng";
                }
                else
                {
                    ViewBag.Title = "Cập nhập khách hàng";
                }
                return View("Edit", data);
            }

            if (data.CustomerID == 0)
            {
                CommonDataService.AddCustomer(data);
            }
            else
            {
                CommonDataService.UpdateCustomer(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new Models.CustomerSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["CustomerSearchCondition"] = condition;
            return View(result);
        }
    }
}