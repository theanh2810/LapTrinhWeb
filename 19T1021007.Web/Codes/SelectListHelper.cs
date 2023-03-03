using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021007.BusinessLayers;
using _19T1021007.DataLayers;

namespace _19T1021007.Web
{
    /// <summary>
    /// cung cấp các hàm hỗ trợ để lấy dữ liệu cho DropDownList
    /// </summary>
    public class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list= new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--- Chọn quốc gia ---"
            });
            foreach(var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list;
        }

        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--- Chọn loại hàng ---"
            });
            foreach (var item in CommonDataService.ListOfCategorys())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }
            return list;
        }

        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--- Chọn nhà cung cấp ---"
            });
            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }
            return list;
        }
    }
}