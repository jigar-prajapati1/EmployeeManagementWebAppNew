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

                if (employee == null)
                {
                    ViewBag.ErrorMessage = "Employee not found.";
                    return View("Error");
                }
                return View(employee);
               
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
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
                    string firstname = file.FileName;
                    string path = Server.MapPath("~/Images/");
                    string st = path + firstname;
                    file.SaveAs(path + firstname);
                    empDetail.ProfilePicture = firstname;
                }
                //pass details to repository
                _employeeService.AddEmployee(empDetail);
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
                    string firstname = file.FileName;
                    string path = Server.MapPath("~/Images/");
                    string st = path + firstname;
                    file.SaveAs(path + firstname);
                    detail.ProfilePicture = firstname;
                }
                // Update employee details using the service
                _employeeService.UpdateEmployee(detail);
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
        [HttpGet]
        public ActionResult DeleteEmployee(int? id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
               
            }
            catch
            {
                ViewBag.AlertMsg = "Failed to delete employee details";
            }
            return View("DeleteEmployee");
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