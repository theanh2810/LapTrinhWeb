using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021007.DataLayers
{
    /// <summary>
    /// định nghĩa các phép xử lý dữ liệu liên quan đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
