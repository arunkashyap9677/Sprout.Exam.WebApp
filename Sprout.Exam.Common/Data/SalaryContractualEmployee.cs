using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.Data
{
    public class SalaryContractualEmployee : ISalaryOfEmployee
    {
        private readonly int _salaryPerDayContractEmployee = 500;
        private  decimal _daysWorked;
        public SalaryContractualEmployee(decimal daysWorked)
        {
            _daysWorked = daysWorked;
        }
        public decimal CalculateSalary()
        {
            //ASSUMING THAT SALARY WILL BE PAID EITHER FOR HALF DAY OR FULL DAY
            var decimalpart = _daysWorked - (int)_daysWorked;
            int halfday = decimal.Compare(decimalpart, 0.5m);
            if (halfday == 1 || halfday == 0)
                _daysWorked = (int)_daysWorked + 0.5m;
            else
                _daysWorked = (int)_daysWorked;
            decimal totalSalary = _daysWorked * _salaryPerDayContractEmployee;
            if (totalSalary <= 0)
                return 0;
            return  Math.Round(totalSalary,2);
        }
    }
}
