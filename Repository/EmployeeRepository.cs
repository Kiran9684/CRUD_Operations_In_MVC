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
    public class EmployeeRepository
    {
        private SqlConnection empConnection; 

        private void createConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["EmployeeConnection"].ConnectionString.ToString();
            empConnection = new SqlConnection(conString);
        }

        //To get all the employee list

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            /*  We are using disconnected data access i.e sql data adapter*/
           
               
                createConnection();
                string procName = "usp_GetEmployees";
                // SqlCommand command = new SqlCommand(procName, empConnection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(procName, empConnection);
                dataAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //SqlDataAdapter will handle connection open and close. 

                //create a new data table. 
                DataTable employeeTable = new DataTable();
                dataAdapter.Fill(employeeTable); //To fill the sql employee table data to in-memory created table.

                foreach(DataRow empRow in employeeTable.Rows)
                {
                    employees.Add
                    (
                        new Employee()
                        {
                            EmployeeId =Convert.ToInt32(empRow["EmployeeId"]),
                            Name = Convert.ToString(empRow["Name"].ToString()),
                            Gender = Convert.ToString(empRow["Gender"].ToString()),
                            City = Convert.ToString(empRow["City"].ToString()),
                            Salary = Convert.ToDouble(empRow["Salary"])
                        }
                    ) ;
                }
                
            return employees;
        }

        public Employee getEmployee(int id)
        {
            Employee employee = new Employee();
            try
            {
               
                createConnection();
                string procName = "usp_GetEmployeeById";
                SqlCommand sqlCommand = new SqlCommand(procName, empConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EmpId", id);
                empConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                        employee.Name = Convert.ToString(reader["Name"]);
                        employee.Gender = Convert.ToString(reader["Gender"]);
                        employee.City = Convert.ToString(reader["City"]);
                        employee.Salary = Convert.ToDouble(reader["Salary"]);
                    }
                }
                /* 2nd Method, very efficient one*/
                /*
                 * You can make use of already existing proc which gets list of employees,
                 * Then you can get the employee from that list and return it to the view, 
                 * without connecting to data base.
                 */

            }
            
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                empConnection.Close();
            }
            return employee;
        }

        public List<Employee> GetAllEmployeeByDeptId(int id)
        {
            List<Employee> employees = new List<Employee>();
            /*  We are using disconnected data access i.e sql data adapter*/


            createConnection();
            string procName = "usp_GetEmployeeByDepId";
           
            SqlDataAdapter dataAdapter = new SqlDataAdapter(procName, empConnection);
            dataAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            dataAdapter.SelectCommand.Parameters.AddWithValue("@DeptId", id);

            //create a new data table. 
            DataTable employeeTable = new DataTable();
            dataAdapter.Fill(employeeTable); //To fill the sql employee table data to in-memory created table.

            foreach (DataRow empRow in employeeTable.Rows)
            {
                employees.Add
                (
                    new Employee()
                    {
                        EmployeeId = Convert.ToInt32(empRow["EmployeeId"]),
                        Name = Convert.ToString(empRow["Name"].ToString()),
                        Gender = Convert.ToString(empRow["Gender"].ToString()),
                        City = Convert.ToString(empRow["City"].ToString()),
                        Salary = Convert.ToDouble(empRow["Salary"])
                    }
                );
            }

            return employees;
        }

        public int InsertEmployee(Employee employee)
        {
            createConnection();

            string proc = "usp_InsertEmployee";

            SqlCommand sqlCommand = new SqlCommand(proc, empConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            /*Preparing insert proc input parameters*/

            SqlParameter EmpName = new SqlParameter() { ParameterName = "@Name",Value = employee.Name,DbType = System.Data.DbType.String };
            SqlParameter EmpGender = new SqlParameter() { ParameterName = "@Gender",Value = employee.Gender, DbType = System.Data.DbType.String };
            SqlParameter EmpCity = new SqlParameter() { ParameterName = "@City", Value = employee.City, DbType = System.Data.DbType.String };
            SqlParameter EmpSalary = new SqlParameter() { ParameterName = "@Salary", Value = employee.Salary, DbType = System.Data.DbType.Decimal };
            SqlParameter EmpDeptId = new SqlParameter() { ParameterName = "@DepId", Value = employee.DeptId, DbType = System.Data.DbType.Int32 };

            sqlCommand.Parameters.Add(EmpName);
            sqlCommand.Parameters.Add(EmpGender);
            sqlCommand.Parameters.Add(EmpCity);
            sqlCommand.Parameters.Add(EmpSalary);
            sqlCommand.Parameters.Add(EmpDeptId);

            empConnection.Open();
            int count = sqlCommand.ExecuteNonQuery();
            empConnection.Close();
            return count;
        }

        public int UpdateEmployee(Employee employee)
        {
            createConnection();
            string proc = "usp_UpdateEmployee";

            SqlCommand sqlCommand = new SqlCommand(proc, empConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter EmpId = new SqlParameter() { ParameterName = "@EmpId", Value = employee.EmployeeId, DbType = System.Data.DbType.Int32 };
            SqlParameter EmpName = new SqlParameter() { ParameterName = "@Name", Value = employee.Name, DbType = System.Data.DbType.String };
            SqlParameter EmpGender = new SqlParameter() { ParameterName = "@Gender", Value = employee.Gender, DbType = System.Data.DbType.String };
            SqlParameter EmpCity = new SqlParameter() { ParameterName = "@City", Value = employee.City, DbType = System.Data.DbType.String };
            SqlParameter EmpSalary = new SqlParameter() { ParameterName = "@Salary", Value = employee.Salary, DbType = System.Data.DbType.Decimal };
            SqlParameter EmpDeptId = new SqlParameter() { ParameterName = "@DepId", Value = employee.DeptId, DbType = System.Data.DbType.Int32 };

            sqlCommand.Parameters.Add(EmpId);
            sqlCommand.Parameters.Add(EmpName);
            sqlCommand.Parameters.Add(EmpGender);
            sqlCommand.Parameters.Add(EmpCity);
            sqlCommand.Parameters.Add(EmpSalary);
            sqlCommand.Parameters.Add(EmpDeptId);

            empConnection.Open();
            int count = sqlCommand.ExecuteNonQuery();
            empConnection.Close();
            return count;
        }

    }
}