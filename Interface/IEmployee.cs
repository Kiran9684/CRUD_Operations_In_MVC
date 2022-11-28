using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Operations_In_MVC.Interface
{
    interface IEmployee
    {
        int EmployeeId { get; set; }
      
        string Gender { get; set; }
        string City { get; set; }
        double Salary { get; set; }
        int DeptId { get; set; }
    }
}
