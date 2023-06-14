using EmployeeModels.DbModel;
using EmployeeModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRepositorys.Interfaces
{
    public interface IEmployeeRepository
    {
        List<EmployeeDesignation> GetDesignations();
        List<EmployeeDetail> AddEmployee(EmployeeDetail employee);
        List<EmployeeDetail> GetAllEmployee();
        EmployeeDetail GetEmployeeById(int? id);
        List<EmployeeDetail> UpdateEmployee(EmployeeDetail employee);
        EmployeeDetail DeleteEmployee(int? id);
    }
}
