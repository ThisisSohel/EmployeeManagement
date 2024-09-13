using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string EmployeeName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Mobile { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual byte[] Photo { get; set; }
    }
}
