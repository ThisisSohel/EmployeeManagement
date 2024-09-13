using FluentNHibernate.Mapping;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAO.ModelsMapping
{
	public class EmployeeMapping : ClassMap<Employee>
	{
		public EmployeeMapping() 
		{
			Table("Employees");
			Id(x => x.Id);
			Map(x => x.EmployeeName);
			Map(x => x.Email);
			Map(x => x.Mobile);
			Map(x => x.DateOfBirth);
			Map(x => x.Photo);
		}

	}
}
