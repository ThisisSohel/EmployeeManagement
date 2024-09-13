using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
	public class EmployeeCreateViewModel
	{
		public int Id { get; set; }
		public string EmployeeName { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public byte[] Photo { get; set; }
	}
}
