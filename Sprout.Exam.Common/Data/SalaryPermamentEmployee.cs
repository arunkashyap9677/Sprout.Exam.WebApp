using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.Data
{
    public class SalaryPermamentEmployee : ISalaryOfEmployee
    {
        private readonly decimal _basicSalaryPermanentEmployee = 20000;
        private readonly decimal _taxInPercent = 12;
        private decimal _absentDays;

        public SalaryPermamentEmployee(decimal absentDays)
        {
            _absentDays = absentDays;
        }
        public decimal CalculateSalary()
        {
            //ASSUMING THAT EMPLOYEE WILL BE ABSENT FOR EITHER HALF DAY OR FULL DAY
            decimal absentDeduction = 0;
            var decimalpart = _absentDays - (int)_absentDays;
            int fulldayOff = decimal.Compare(decimalpart, 0.5m);
            if (fulldayOff == 1 || fulldayOff == 0)
                _absentDays = (int)_absentDays + 1;
            else if(decimal.Compare(decimalpart, 0.0m) !=0 && fulldayOff ==-1)
                _absentDays = (int)_absentDays + 0.5m;

            //if there are No absent days
            if (_absentDays <= 0)
                absentDeduction = 0;
            else
                absentDeduction = (_basicSalaryPermanentEmployee / 22)*_absentDays;
            decimal totalSalary = _basicSalaryPermanentEmployee - absentDeduction - (_basicSalaryPermanentEmployee * (decimal)0.12);
            if (totalSalary <= 0)
                return 0;
            return Math.Round(totalSalary, 2);
        }
    }
}
