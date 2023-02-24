using _19T1021007.BusinessLayers;
using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021007.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string EMPLOYEE_SEARCH = "EmployeeSearchCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as Models.PaginationSearchInput;

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
        /// thêm nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }
        /// <summary>
        /// sửa nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhập nhân viên";
            return View(data);
        }
        /// <summary>
        /// xóa nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int EmployeeID = Convert.ToInt32(id);

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(EmployeeID);
                return RedirectToAction("Index");
            }
            else
            {
                var data = CommonDataService.GetEmployee(EmployeeID);
                return View(data);
            }
        }
        /// <summary>
        /// lưu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employee data)
        {
            if (string.IsNullOrWhiteSpace(data.FirstName))
            {
                ModelState.AddModelError(nameof(data.FirstName), "Họ không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.LastName))
            {
                ModelState.AddModelError(nameof(data.LastName), "Tên không được để trống");
            }
            
            
            data.Notes = data.Notes ?? "";
            data.Photo = "Images\\" + data.Photo ?? "http://dummyimage.com/140x180.png/cc0000/ffffff&text=EmployeePhoto";
            data.Email = data.Email ?? "";

            if (!ModelState.IsValid)
            {
                if (data.EmployeeID == 0)
                {
                    ViewBag.Title = "Bổ sung nhân viên";
                }
                else
                {
                    ViewBag.Title = "Cập nhập nhân viên";
                }
                return View("Edit", data);
            }

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
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
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            
            var result = new Models.EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["EmployeeSearchCondition"] = condition;
            return View(result);
        }
    }
}