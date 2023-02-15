using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021007.DataLayers.SQLServer
{
    /// <summary>
    /// lớp cơ sở (cha ) của các lớp cài đặt các chức năng xử lý dữ liệu trên csdl Sql Server
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// chuỗi tham số kết nối đến csdl
        /// </summary>
        protected string _connectionString;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString) 
        { 
            _connectionString= connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection() 
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString= _connectionString;
            connection.Open();
            return connection;
        }
    }
}
