using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021007.DomainModels;

namespace _19T1021007.Web.Models
{
    public class ProductSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// danh sách các sản phẩm
        /// </summary>
        public List<Product> Data { get; set; }
    }
}