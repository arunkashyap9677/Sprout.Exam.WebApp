using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Data;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<IList<EmployeeDto>>> GetAllEmployees();
        Task<ServiceResponse<EmployeeDto>> GetEmployeeById(int id);
        Task<ServiceResponse<EmployeeDto>> UpdateEmployee(EditEmployeeDto editEmployeeDto);
        Task<ServiceResponse<int>> CreateEmployee(CreateEmployeeDto createEmployeeDto);
        Task<ServiceResponse<int>> DeleteEmployeeById(int id);
        decimal CalculateSalary(ISalaryOfEmployee salary);

    }
}
