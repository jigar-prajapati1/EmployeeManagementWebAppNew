using Dapper;
using EmployeeModels.DbModel;
using EmployeeModels.ViewModel;
using EmployeeRepositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeRepositorys.Implement
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public SqlConnection con;
        public EmployeeRepository()
        {
            // Retrieve connection string from configuration file
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);
        }
        /// <summary>Gets the designations.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<EmployeeDesignation> GetDesignations()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    return connection.Query<EmployeeDesignation>("GetEmployeesDesignation", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Adds the employee.</summary>
        /// <param name="employee">The employee.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<EmployeeDetail> AddEmployee(EmployeeDetail employee)
        {
            try
            {
                var parameter = new DynamicParameters();
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    parameter.Add("@Name", employee.Name);
                    parameter.Add("@DesignationId", employee.DesignationId);
                    parameter.Add("@ProfilePicture", employee.ProfilePicture);
                    parameter.Add("@Salary", employee.Salary);
                    parameter.Add("@DateOfBirth", employee.DateOfBirth);
                    parameter.Add("@Email", employee.Email);
                    parameter.Add("@Address", employee.Address);
                    return connection.Query<EmployeeDetail>("AddNewEmpDetails", parameter, commandType: CommandType.StoredProcedure).ToList();
                }
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
        public List<EmployeeDetail> GetAllEmployee()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    return connection.Query<EmployeeDetail>("GetEmployees", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Gets the employee by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public EmployeeDetail GetEmployeeById(int? id)
        {
            try
            {
                var parameter = new DynamicParameters();
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    parameter.Add("@Id", id);
                    return connection.QuerySingleOrDefault<EmployeeDetail>("GetEmpById", parameter, commandType: CommandType.StoredProcedure);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Updates the employee.</summary>
        /// <param name="employee">The employee.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<EmployeeDetail> UpdateEmployee(EmployeeDetail employee)
        {
            try
            {
                var parameter = new DynamicParameters();
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    parameter.Add("@Id", employee.Id);
                    parameter.Add("@Name", employee.Name);
                    parameter.Add("@DesignationId", employee.DesignationId);
                    parameter.Add("@ProfilePicture", employee.ProfilePicture);
                    parameter.Add("@Salary", employee.Salary);
                    parameter.Add("@DateOfBirth", employee.DateOfBirth);
                    parameter.Add("@Email", employee.Email);
                    parameter.Add("@Address", employee.Address);
                    return connection.Query<EmployeeDetail>("UpdateEmpDetails", parameter, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Deletes the employee.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public EmployeeDetail DeleteEmployee(int? id)
        {
            try
            {
                var parameter = new DynamicParameters();
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    parameter.Add("@Id", id);
                    return connection.QuerySingleOrDefault<EmployeeDetail>("DeleteEmpById", parameter, commandType: CommandType.StoredProcedure);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

