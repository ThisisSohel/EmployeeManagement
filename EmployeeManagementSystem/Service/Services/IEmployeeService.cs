using DAO.Entities;
using Service.DTO;
using Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public interface IEmployeeService
	{
		Task<IEnumerable<Employee>> GetAllEmployeesAsync();
		Task<Employee> GetEmployeeByIdAsync(int id);
		Task AddEmployeeAsync(Employee employee);
		Task UpdateEmployeeAsync(Employee employee);
		Task DeleteEmployeeAsync(int id);
        Task<(IEnumerable<Employee> Employees, int TotalRecords, int FilteredRecords)> GetEmployeesAsync(
                int pageNumber,
                int pageSize,
                string name,
                string email,
                string mobile,
                DateTime? dateOfBirth,
                string sortColumn,
                string sortDirection);
    }

}
