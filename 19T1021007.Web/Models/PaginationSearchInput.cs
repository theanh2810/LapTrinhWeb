using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021007.Web.Models
{
    public class PaginationSearchInput
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SearchValue { get; set; }
    }

    public class ProductSearchInput
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int categoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplierID { get; set; }

    }
    public class OrderSearchInput
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SearchValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }
    }
}