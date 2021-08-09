using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Data
{
    public class EmployeeType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public virtual IList<Employee> Employees { get; set; }

    }
}
