﻿using System;
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
}