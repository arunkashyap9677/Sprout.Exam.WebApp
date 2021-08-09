using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string TIN { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(EmployeeType))]
        [Column("EmployeeTypeId")]
        public int TypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }



    }
}
