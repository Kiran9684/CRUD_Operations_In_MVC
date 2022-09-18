using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_Operations_In_MVC.Models;
using System.Data;

namespace CRUD_Operations_In_MVC.Repository
{
    public class DepartmentRepository
    {
        SqlConnection deptCon; 
        private void CreateConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString.ToString();
            deptCon = new SqlConnection(conString);
        }

        /*Method to get department list */

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            CreateConnection();
            string proc = "usp_GetDepartments";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(proc, deptCon);
            sqlDataAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            DataTable deptTable = new DataTable();
            sqlDataAdapter.Fill(deptTable);

            foreach(DataRow row in deptTable.Rows)
            {
                departments.Add(
                    new Department()
                    {
                        DeptId = Convert.ToInt32(row["DeptId"]),
                        DeptName = Convert.ToString(row["Name"])
                    });
            }
            return departments;
        }

    }
}