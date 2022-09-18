using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Operations_In_MVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public double Salary { get; set; }
        public int DeptId { get; set; }

    }
}
