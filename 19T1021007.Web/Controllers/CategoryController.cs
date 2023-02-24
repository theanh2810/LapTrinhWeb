using _19T1021007.BusinessLayers;
using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021007.Web.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string CATEGORY_SEARCH = "CategorySearchCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[CATEGORY_SEARCH] as Models.PaginationSearchInput;

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
        /// thêm loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            Category data = new Category()
            {
                CategoryID = 0
            };

            return View("Edit", data);
        }
        /// <summary>
        /// sửa loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhập loại hàng";
            return View(data);
        }
        /// <summary>
        /// xóa loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int CategoryID = Convert.ToInt32(id);

            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(CategoryID);
                return RedirectToAction("Index");
            }
            else
            {
                var data = CommonDataService.GetCategory(CategoryID);
                return View(data);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category data)
        {
            if (string.IsNullOrWhiteSpace(data.CategoryName))
            {
                ModelState.AddModelError(nameof(data.CategoryName), "Tên loại hàng không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Description))
            {
                ModelState.AddModelError(nameof(data.Description), "Chi tiết loại hàng không được để trống");
            }

            if (!ModelState.IsValid)
            {
                if (data.CategoryID == 0)
                {
                    ViewBag.Title = "Bổ sung loại hàng";
                }
                else
                {
                    ViewBag.Title = "Cập nhập loại hàng";
                }
                return View("Edit", data);
            }

            if (data.CategoryID == 0)
            {
                CommonDataService.AddCategory(data);
            }
            else
            {
                CommonDataService.UpdateCategory(data);
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
            var data = CommonDataService.ListOfCategorys(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new Models.CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["CategorySearchCondition"] = condition;
            return View(result);
        }
    }
}