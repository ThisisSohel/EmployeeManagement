using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Entities
{
	public class Employee
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string ImageUri { get; set; } = string.Empty;
    }
}
