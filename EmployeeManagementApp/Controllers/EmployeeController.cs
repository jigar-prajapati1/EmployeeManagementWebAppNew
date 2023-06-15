using EmployeeModels.DbModel;
using EmployeeModels.ViewModel;
using EmployeeServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>Gets the employee by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        public ActionResult GetEmployeeById(int? id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                return View(employee);
            }
            catch
            {
                ViewBag.ErrorMessage = "Employee not found.";
            }
            return View();
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            var designations = _employeeService.GetDesignations();
            ViewBag.Designations = new SelectList(designations, "DesignationId", "Designation");
            return View("AddEmployee");
        }
        /// <summary>Adds the employee.</summary>
        /// <param name="empDetail">The emp detail.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        public ActionResult AddEmployee(EmployeeDetailView empDetail)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    //save image in server
                    HttpPostedFileBase file = Request.Files[0];
                    string firstName = file.FileName;
                    string path = Server.MapPath("~/Images/");
                    string combine = path + firstName;
                    file.SaveAs(path + firstName);
                    empDetail.ProfilePicture = firstName;
                }
                //pass details to repository
                _employeeService.AddEmployee(empDetail);
                TempData["Success"] = "Employee Created Successfully";
                return RedirectToAction("GetAll");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to add employee. Error: " + ex.Message;
                return View();
            }
        }
        /// <summary>Edits the employee.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        public ActionResult EditEmployee(int? id)
        {
            try
            {
                var employees = _employeeService.GetEmployeeById(id);
                if (employees == null)
                {
                    ViewBag.ErrorMessage = "Employee is null";
                }
                return View(employees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Edits the employee.</summary>
        /// <param name="detail">The detail.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        public ActionResult EditEmployee(EmployeeDetailView detail)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    // Save image on the server
                    HttpPostedFileBase file = Request.Files[0];
                    string firstName = file.FileName;
                    string path = Server.MapPath("~/Images/");
                    string combine = path + firstName;
                    file.SaveAs(path + firstName);
                    detail.ProfilePicture = firstName;
                }
                    // Update employee details using the service
                   _employeeService.UpdateEmployee(detail);
                   TempData["Success"] = "Employee Updated Successfully";
                   return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Deletes the employee.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        public ActionResult DeleteEmployee(int? id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
            }
            catch(Exception ex)
            {
                throw ex; 
            }
            return RedirectToAction("GetAll");
        }
        /// <summary>Gets all.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                List<EmployeeDetailView> employees = _employeeService.GetAllEmployee();
                return View(employees);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}