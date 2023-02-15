using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021007.DomainModels;
using _19T1021007.DataLayers;
using _19T1021007.DataLayers.FakeDB;
using System.Configuration;

namespace _19T1021007.BusinessLayers
{
    /// <summary>
    /// cung cấp các chức năng xử lý nghiệp vụ liên quan đến quốc gia , khách hàng , người giao hàng , sinh viên, nhà cung cấp
    /// customer, supplier, category , employee, shipper
    /// </summary>
    public static class CommonDataService
    {
        /// <summary>
        /// _19T1021007.DataLayers.SQLServer.CountryDAL
        /// </summary>
        private static ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Category> categoryDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Shipper> shipperDB;
        /// <summary>
        /// 
        /// </summary>
        static CommonDataService()
        {
            //string connectionString = @"Server=\SQLEXPRESS;Database=LiteComerceDB;User Id=sa;Password=123456;";
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            //countryDB = new DataLayers.FakeDB.CountryDAL();
            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);  
        }
        /// <summary>
        /// danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #region chức năng các nghiệp vụ liên quan đến nhà cung cấp

        #endregion
        #region
        /// <summary>
        /// lấy danh sách nhà cung cấp dưới dạng tìm kiếm phân trang
        /// </summary>
        /// <param name="page">trang cần xem</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang, nếu pagesize = 0 => khong phan trang</param>
        /// <param name="searchValue">giá trị tìm kiếm, nếu = "" thì lấy toàn bộ dữ liệu </param>
        /// <param name="rowCount">tham số đầu ra</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount) {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page,pageSize,searchValue).ToList();
        }
        /// <summary>
        /// không phân trang
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue = "")
        {
            return supplierDB.List(1,0, searchValue).ToList();
        }
        /// <summary>
        /// lấy 1 nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int id)
        {
            return supplierDB.Get(id);
        }
        /// <summary>
        /// thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        /// <summary>
        /// cập nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        /// xóa nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int id) 
        {
            return supplierDB.Delete(id);
        }
        /// <summary>
        /// kiểm tra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int id)
        {
            return supplierDB.InUsed(id);
        }
        #endregion

        #region chức năng các nghiệp vụ liên quan người giao hàng
        #endregion
        #region
        /// <summary>
        /// lấy danh sách shipper dưới dạng tìm kiếm phân trang
        /// </summary>
        /// <param name="page">trang cần xem</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang, nếu pagesize = 0 => khong phan trang</param>
        /// <param name="searchValue">giá trị tìm kiếm, nếu = "" thì lấy toàn bộ dữ liệu </param>
        /// <param name="rowCount">tham số đầu ra</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// không phân trang
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(string searchValue = "")
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// lấy 1 shipper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int id)
        {
            return shipperDB.Get(id);
        }
        /// <summary>
        /// thêm shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        /// <summary>
        /// cập nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        /// <summary>
        /// xóa nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int id)
        {
            return shipperDB.Delete(id);
        }
        /// <summary>
        /// kiểm tra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int id)
        {
            return shipperDB.InUsed(id);
        }
        #endregion

        #region chức năng các nghiệp vụ liên quan đến nhân viên
        #endregion
        #region
        /// <summary>
        /// lấy danh sách nhân viên dưới dạng tìm kiếm phân trang
        /// </summary>
        /// <param name="page">trang cần xem</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang, nếu pagesize = 0 => khong phan trang</param>
        /// <param name="searchValue">giá trị tìm kiếm, nếu = "" thì lấy toàn bộ dữ liệu </param>
        /// <param name="rowCount">tham số đầu ra</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// không phân trang
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(string searchValue = "")
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// lấy 1 nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int id)
        {
            return employeeDB.Get(id);
        }
        /// <summary>
        /// thêm nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        /// cập nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }
        /// <summary>
        /// xóa nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int id)
        {
            return employeeDB.Delete(id);
        }
        /// <summary>
        /// kiểm tra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int id)
        {
            return employeeDB.InUsed(id);
        }
        #endregion

        #region chức năng các nghiệp vụ liên quan đến khách hàng

        #endregion
        #region
        /// <summary>
        /// lấy danh sách nhà cung cấp dưới dạng tìm kiếm phân trang
        /// </summary>
        /// <param name="page">trang cần xem</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang, nếu pagesize = 0 => khong phan trang</param>
        /// <param name="searchValue">giá trị tìm kiếm, nếu = "" thì lấy toàn bộ dữ liệu </param>
        /// <param name="rowCount">tham số đầu ra</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// không phân trang
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue = "")
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// lấy 1 nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int id)
        {
            return customerDB.Get(id);
        }
        /// <summary>
        /// thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// cập nhập
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// xóa nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int id)
        {
            return customerDB.Delete(id);
        }
        /// <summary>
        /// kiểm tra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int id)
        {
            return customerDB.InUsed(id);
        }
        #endregion

        #region chức năng các nghiệp vụ liên quan đến loại hàng

        #endregion
        #region
        /// <summary>
        /// lấy danh sách loại hàng dưới dạng tìm kiếm phân trang
        /// </summary>
        /// <param name="page">trang cần xem</param>
        /// <param name="pageSize">số dòng hiển thị trên mỗi trang, nếu pagesize = 0 => khong phan trang</param>
        /// <param name="searchValue">giá trị tìm kiếm, nếu = "" thì lấy toàn bộ dữ liệu </param>
        /// <param name="rowCount">tham số đầu ra</param>
        /// <returns></returns>
        public static List<Category> ListOfCategorys(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// không phân trang
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategorys(string searchValue = "")
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// lấy 1 loại hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Category GetCategory(int id)
        {
            return categoryDB.Get(id);
        }
        /// <summary>
        /// thêm loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        /// <summary>
        /// cập nhập loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        /// <summary>
        /// xóa loại hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int id)
        {
            return categoryDB.Delete(id);
        }
        /// <summary>
        /// kiểm tra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int id)
        {
            return categoryDB.InUsed(id);
        }
        #endregion
    }
}
