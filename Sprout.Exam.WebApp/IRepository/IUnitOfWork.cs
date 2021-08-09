using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.IRepository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<EmployeeType> EmployeeTypes { get; }
        Task Save();
    }
}
