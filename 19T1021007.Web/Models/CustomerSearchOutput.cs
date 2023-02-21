using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021007.Web.Models
{
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        // <summary>
        /// Danh sách các 
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}