﻿using _19T1021007.BusinessLayers;
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
        private const string SHIPPER_SEARCH = "ShipperSearchCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[SHIPPER_SEARCH] as Models.PaginationSearchInput;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new Models.ShipperSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["ShipperSearchCondition"] = condition;
            return View(result);
        }
    }
}