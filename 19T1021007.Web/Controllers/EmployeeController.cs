using _19T1021007.BusinessLayers;
using _19T1021007.DomainModels;
using _19T1021007.Web.Codes;
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
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase uploadPhoto)
        {

            if (string.IsNullOrWhiteSpace(birthday))
            {
                ModelState.AddModelError("BirthDate", "Ngày  sinh không được để trống");
            }
            else
            {
                DateTime? d = SelectListComerce.DMYStringToDateTime(birthday);
                if (d == null)
                    ModelState.AddModelError("BirthDate", $"ngày {birthday} sinh không hợp lệ");
                else
                    data.BirthDate = d.Value;
            }
            // Kiểm soát đầu vào có hợp lệ hay không
            if (string.IsNullOrWhiteSpace(data.LastName))
                ModelState.AddModelError("LastName", " Họ không được để trống");
            if (string.IsNullOrWhiteSpace(data.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");
            /* if (string.IsNullOrWhiteSpace(string.Format("{0:dd/MM/yyyy}", Convert.ToString(data.BirthDate))))
                 ModelState.AddModelError("BirthDate", "ngày sinh không được để trống");*/
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            if (string.IsNullOrWhiteSpace(data.Photo))
                /* ModelState.AddModelError("Photo", "Ảnh không được để trống");*/
                data.Photo = "";
            if (string.IsNullOrWhiteSpace(data.Notes))
                ModelState.AddModelError("Notes", "ghi chú không được để trống");
            if (uploadPhoto != null)
            {
                // lưu file vào đường dẫn vật lý, đường dẫn ảo
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);// phép cộng chuỗi
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
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