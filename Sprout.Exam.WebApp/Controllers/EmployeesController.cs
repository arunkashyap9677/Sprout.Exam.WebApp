using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Services;
using Sprout.Exam.Common.Data;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly GenericRequestController processRequest;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var result = await Task.FromResult(StaticEmployees.ResultList);
            var result = await _employeeService.GetAllEmployees();
            return GenericRequestController.ServiceResponse(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
             var result = await _employeeService.GetEmployeeById(id);
             return GenericRequestController.ServiceResponse(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditEmployeeDto input)
        {
            var result = await _employeeService.UpdateEmployee(input);
            return GenericRequestController.ServiceResponse(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {

            var result = await _employeeService.CreateEmployee(input);
            return GenericRequestController.ServiceResponse(result);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployeeById(id);
            return Ok(id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id,[FromBody] SalaryStructure salaryStructure)
        {
            var resultEmployee =  await _employeeService.GetEmployeeById(id);

            if (resultEmployee == null) return NotFound();
            var type = (EmployeeType)resultEmployee.Result.TypeId;
            switch(type)
            {
                case EmployeeType.Regular:
                    {
                        var salary = _employeeService.CalculateSalary(new SalaryPermamentEmployee(salaryStructure.absentdays));
                        return Ok(salary);
                    }

                case EmployeeType.Contractual:
                    {
                        var salary = _employeeService.CalculateSalary(new SalaryContractualEmployee(salaryStructure.workedDays));
                        return Ok(salary);
                    }
                default:
                    return NotFound("Employee Type not found");
            };

        }

    }
}
