using CRUD_Operations_In_MVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Operations_In_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        //public ActionResult Index()
        //{
        //    return View();
        //}

        /*Action method to bring the available list of departments from DB*/

        public ActionResult GetDepartments()
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();
            return View(departmentRepository.GetDepartments());
        }

    }
}