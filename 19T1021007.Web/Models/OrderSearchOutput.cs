using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021007.Web.Models
{
    public class OrderSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách các đơn đặt hàng
        /// </summary>
        public List<Order> Data { get; set; }
    }
}