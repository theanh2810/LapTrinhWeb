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
    }
}