using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IGenericRepository<Employee> _employees;
        private IGenericRepository<EmployeeType> _employeeTypes;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<Employee> Employees => _employees ??= new GenericRepository<Employee>(_dbContext);
        public IGenericRepository<EmployeeType> EmployeeTypes => _employeeTypes ??= new GenericRepository<EmployeeType>(_dbContext);

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
