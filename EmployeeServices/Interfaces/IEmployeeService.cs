using EmployeeModels.DbModel;
using EmployeeModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDetailView> GetAllEmployee();
        List<EmployeeDesignationView> GetDesignations();
        EmployeeDetailView GetEmployeeById(int? id);
        void DeleteEmployee(int? id);
        void UpdateEmployee(EmployeeDetailView empDetail); 
        void AddEmployee(EmployeeDetailView empDetail);
    }
}
