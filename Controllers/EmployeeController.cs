using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Operations_In_MVC.Models;
using CRUD_Operations_In_MVC.Repository;

namespace CRUD_Operations_In_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult GetAllEmployees()
        {

            EmployeeRepository employeeRepository = new EmployeeRepository();
            return View(employeeRepository.GetAllEmployees());

        }

        public ActionResult GetEmployee(int id)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return View(employeeRepository.getEmployee(id));
        }

        public ActionResult GetEmployeeByDeptId(int deptId)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            return View(employeeRepository.GetAllEmployeeByDeptId(deptId));
        }

        [HttpGet]
        [ActionName("CreateEmployee")]
        public ActionResult CreateEmp_Get()
        {
            ViewBag.DepartmentIdList = GenerateDeptIdDropDown();
            return View();
        }

        [HttpPost]
        [ActionName("CreateEmployee")]
        public ActionResult CreateEmp_Post()
        {
            /*Re-rendering the departmentId Dropdown list in post method as well*/
            ViewBag.DepartmentIdList = GenerateDeptIdDropDown();

            /*
            if (ModelState.IsValid)
            {
                foreach(string key in formCollection.AllKeys)
                {
                    Response.Write("Key = " + key + " ");
                    Response.Write("Value  ="+ formCollection[key]);
                    Response.Write("<br/>");
                }
            }
            */

            /*
            Employee employee = new Employee();
            employee.Name = formCollection["Name"];
            employee.City = formCollection["City"];
            employee.Salary = Convert.ToDouble(formCollection["Salary"]);
            employee.Gender = formCollection["Gender"];
            employee.DeptId = Convert.ToInt32(formCollection["DeptId"]);
            */
            Employee employee = new Employee();

            UpdateModel<Employee>(employee);

            EmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.InsertEmployee(employee);

            return RedirectToAction("CreateEmployee");
        }

        private List<SelectListItem> GenerateDeptIdDropDown()
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();

            /*To make dept Id dropdown in create employee view*/
            List<SelectListItem> DeptIdList = new List<SelectListItem>();
            foreach (Department department in departmentRepository.GetDepartments())
            {
                DeptIdList.Add(new SelectListItem() { Text = department.DeptName.ToString(), Value = department.DeptId.ToString() });
            }

            return DeptIdList;
        }

        [HttpGet]
        [ActionName("EditEmployee")]
        public ActionResult Edit_Get(int id)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            Employee employee = employeeRepository.getEmployee(id);
            ViewBag.DepartmentIdList = GenerateDeptIdDropDown();
            return View(employee);
        }

        [HttpPost]
        [ActionName("EditEmployee")]
        public ActionResult Edit_Post(int id)
        {
            /*Re-rendering the departmentId Dropdown list in post method as well*/
            ViewBag.DepartmentIdList = GenerateDeptIdDropDown();

            EmployeeRepository employeeRepository = new EmployeeRepository();

            Employee employee = employeeRepository.getEmployee(id);

            UpdateModel(employee, new string[] { "Gender", "City", "Salary", "DeptId" });

            return View(employee);
        }
    }
}