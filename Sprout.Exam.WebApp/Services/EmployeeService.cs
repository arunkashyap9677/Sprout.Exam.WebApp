using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sprout.Exam.Common.Enums;
using EmployeeType = Sprout.Exam.Common.Enums.EmployeeType;
using Sprout.Exam.Common.Data;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Common;

namespace Sprout.Exam.WebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IList<EmployeeDto>>> GetAllEmployees()
        {
            ServiceResponse<IList<EmployeeDto>> response = new ServiceResponse<IList<EmployeeDto>>();
            try
            {
                var employeeResultList = await _unitOfWork.Employees.GetAll();
                if (employeeResultList == null)
                {
                    response.ResponseStatus = ResponseStatus.NoContent;
                    response.Result = null;
                    return response;
                }
                var employeeListDto = _mapper.Map<IList<EmployeeDto>>(employeeResultList);
                response.ResponseStatus = ResponseStatus.Success;
                response.Result = employeeListDto;
                return response;
            }

            catch (Exception e)
            {
                response.ResponseStatus = ResponseStatus.InternalServerError;
                response.Result = null;
                return response;
            }
        }

        public async Task<ServiceResponse<EmployeeDto>> GetEmployeeById(int id)
        {
            ServiceResponse<EmployeeDto> response = new ServiceResponse<EmployeeDto>();
            try
            {
                var employeeResult = await _unitOfWork.Employees.Get(emp => emp.Id == id);
                if (employeeResult == null)
                {
                    response.ResponseStatus = ResponseStatus.NoContent;
                    response.Result = null;
                    return response;
                }
                var employeeDto = _mapper.Map<EmployeeDto>(employeeResult);
                response.ResponseStatus = ResponseStatus.Success;
                response.Result = employeeDto;
                return response;
            }

            catch (Exception e)
            {
                response.ResponseStatus = ResponseStatus.InternalServerError;
                response.Result = null;
                return response;
            }
        }

        public async Task<ServiceResponse<int>> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            try
            {
                var employee = _mapper.Map<Employee>(createEmployeeDto);
                await _unitOfWork.Employees.Insert(employee);
                await _unitOfWork.Save();
                response.ResponseStatus = ResponseStatus.Created;
                response.Identifier = $"/api/employees/{employee.Id}";
                response.Result = employee.Id;
                return response;

            }

            catch (Exception e)
            {
                response.ResponseStatus = ResponseStatus.InternalServerError;
                response.Result = -1;
                return response;
            }
        }

        public async Task<ServiceResponse<EmployeeDto>> UpdateEmployee(EditEmployeeDto editEmployeeDto)
        {
            ServiceResponse<EmployeeDto> response = new ServiceResponse<EmployeeDto>();
            try
            {
                var employeeResult = await _unitOfWork.Employees.Get(emp => emp.Id == editEmployeeDto.Id);
                if(employeeResult == null) 
                {
                    response.ResponseStatus = ResponseStatus.NoContent;
                    response.Result = null;
                    return response;
                }
                var updatedEmployee = _mapper.Map(editEmployeeDto, employeeResult);
                 _unitOfWork.Employees.Update(updatedEmployee);
                await _unitOfWork.Save();
                response.ResponseStatus = ResponseStatus.Success;
                response.Result = _mapper.Map<EmployeeDto>(updatedEmployee);
                return response;
            }

            catch (Exception e)
            {
                response.ResponseStatus = ResponseStatus.InternalServerError;
                response.Result = null;
                return response;
            }
        }

        public async Task<ServiceResponse<int>> DeleteEmployeeById(int id)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            try
            {
                await _unitOfWork.Employees.Delete(id);
                await _unitOfWork.Save();
                response.ResponseStatus = ResponseStatus.Success;
                response.Result = id;
                return response;
            }

            catch (Exception e)
            {
                response.ResponseStatus = ResponseStatus.InternalServerError;
                response.Result = -1;
                return response;
            }
        }

        public decimal CalculateSalary(ISalaryOfEmployee salary)
        {
            return salary.CalculateSalary();
        }
    }
}
