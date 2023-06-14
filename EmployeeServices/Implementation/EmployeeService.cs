using EmployeeModels.DbModel;
using EmployeeModels.ViewModel;
using EmployeeRepositorys.Interfaces;
using EmployeeServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Implements
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }
        /// <summary>Adds the employee.</summary>
        /// <param name="empDetail">The emp detail.</param>
        public void AddEmployee(EmployeeDetailView empDetail)
        {
            try
            {
                var empViewModel = new EmployeeDetail()
                {
                    Name = empDetail.Name,
                    DesignationId = empDetail.DesignationId,
                    ProfilePicture = empDetail.ProfilePicture,
                    Salary = empDetail.Salary,
                    DateOfBirth = empDetail.DateOfBirth,
                    Email = empDetail.Email,
                    Address = empDetail.Address,
                    Designation = empDetail.Designation
                };
                employeeRepository.AddEmployee(empViewModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Deletes the employee.</summary>
        /// <param name="id">The identifier.</param>
        public void DeleteEmployee(int? id)
        {
            employeeRepository.DeleteEmployee(id);
        }
        /// <summary>Updates the employee.</summary>
        /// <param name="empDetail">The emp detail.</param>
        public void UpdateEmployee(EmployeeDetailView empDetail)
        {
            try
            {
                EmployeeDetail existingEmployee = employeeRepository.GetEmployeeById(empDetail.Id);
                // Update the properties of the existing employee with the values from the updated detail
                existingEmployee.Name = empDetail.Name;
                existingEmployee.Designation = empDetail.Designation;
                existingEmployee.ProfilePicture = empDetail.ProfilePicture;
                existingEmployee.Salary = empDetail.Salary;
                existingEmployee.DateOfBirth = empDetail.DateOfBirth;
                existingEmployee.Email = empDetail.Email;
                existingEmployee.Address = empDetail.Address;
                // Update the employee using the repository
                employeeRepository.UpdateEmployee(existingEmployee);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Gets all employee.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<EmployeeDetailView> GetAllEmployee()
        {
            try
            {
                List<EmployeeDetailView> employeeList = new List<EmployeeDetailView>();
                var viewModelList = employeeRepository.GetAllEmployee();
                if (viewModelList != null && viewModelList.Count > 0)
                {
                    foreach (var item in viewModelList)
                    {
                        employeeList.Add(new EmployeeDetailView
                        {
                            Id = item.Id,
                            Name = item.Name,
                            DesignationId = item.DesignationId,
                            ProfilePicture = item.ProfilePicture,
                            Salary = item.Salary,
                            DateOfBirth = item.DateOfBirth,
                            Email = item.Email,
                            Address = item.Address,
                            Designation = item.Designation
                        });
                    }
                }
                return employeeList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Gets the employee by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public EmployeeDetailView GetEmployeeById(int? id)
        {
            try
            {
                EmployeeDetail viewModel = employeeRepository.GetEmployeeById(id);
                EmployeeDetailView employee = new EmployeeDetailView
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    DesignationId = viewModel.DesignationId,
                    ProfilePicture = viewModel.ProfilePicture,
                    Salary = viewModel.Salary,
                    DateOfBirth = viewModel.DateOfBirth,
                    Email = viewModel.Email,
                    Address = viewModel.Address
                };
                return employee;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Gets the designations.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<EmployeeDesignationView> GetDesignations()
        {
            try
            {
                List<EmployeeDesignation> viewModelList = employeeRepository.GetDesignations();
                List<EmployeeDesignationView> employeeList = viewModelList.Select(v => new EmployeeDesignationView
                {
                    DesignationId = v.DesignationId,
                    Designation = v.Designation,

                }).ToList();
                return employeeList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

