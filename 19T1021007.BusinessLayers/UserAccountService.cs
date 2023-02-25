using _19T1021007.DataLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021007.DomainModels;
using System.Configuration;

namespace _19T1021007.BusinessLayers
{
    /// <summary>
    /// các chức năng liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;

        static UserAccountService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
            customerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
        }

        public static UserAccount Authorize(AccountTypes accountType, string username, string password)
        {
            if(accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.Authorize(username, password);
            }
            else
            {
                return customerAccountDB.Authorize(username, password);
            }
        } 

        public static bool ChangePassword(AccountTypes accountType, string username, string oldPassword, string newPassword)
        {
            if (accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.ChangePassword(username, oldPassword, newPassword);
            }
            else
            {
                return customerAccountDB.ChangePassword(username, oldPassword, newPassword);
            }
        }
    }
}
