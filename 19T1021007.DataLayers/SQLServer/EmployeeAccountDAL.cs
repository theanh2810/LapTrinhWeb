using _19T1021007.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021007.DataLayers.SQLServer
{
    public class EmployeeAccountDAL : _BaseDAL, IUserAccountDAL
    {
        /// <summary>
        /// cài đặt cho tại khoản của nhân viên
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using(var cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE Email = @Email and Password = @Password";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    string fullname = Convert.ToString(dbReader["FirstName"]) + " " + Convert.ToString(dbReader["LastName"]);
                    data = new UserAccount()
                    {
                        UserID = Convert.ToString(dbReader["EmployeeID"]),
                        UserName = Convert.ToString(dbReader["Email"]),
                        FullName = fullname,
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = "",
                        RoleNames = ""
                    };
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
