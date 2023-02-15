using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021007.DataLayers
{
    /// <summary>
    /// định nghĩa các phép xử lý dữ liệu chung (generic)
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// tìm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">trang cần xem</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang, nếu pagesize = 0 => khong phan trang </param>
        /// <param name="searchValue">giá trị tìm kiếm, nếu = "" thì lấy toàn bộ dữ liệu </param>
        /// <returns></returns>
        List<T> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// đếm số dòng thỏa tìm kiếm
        /// </summary>
        /// <param name="seachValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// lấy 1 dòng dữ liệu dựa vào id
        /// </summary>
        /// <param name="id">id của dữ liệu</param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// bổ sung dữ liệu 
        /// </summary>
        /// <param name="data">dữ liệu</param>
        /// <returns>id cua du lieu bo sung</returns>
        int Add(T data);
        /// <summary>
        /// cập nhập dữu liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// xóa dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// kiểm tra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
