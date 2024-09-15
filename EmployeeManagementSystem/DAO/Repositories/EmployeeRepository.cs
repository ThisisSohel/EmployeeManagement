using DAO.DB;
using DAO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDbContext _context;

		public EmployeeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			return await _context.Employees.ToListAsync();
		}

		public async Task<Employee> GetEmployeeByIdAsync(int id)
		{
			return await _context.Employees.FindAsync(id);
		}

		public async Task AddEmployeeAsync(Employee employee)
		{
			await _context.Employees.AddAsync(employee);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateEmployeeAsync(Employee employee)
		{
			_context.Employees.Update(employee);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteEmployeeAsync(int id)
		{
			var employee = await _context.Employees.FindAsync(id);
			if (employee != null)
			{
				_context.Employees.Remove(employee);
				await _context.SaveChangesAsync();
			}
		}

        public async Task<(IEnumerable<Employee> Employees, int TotalRecords, int FilteredRecords)> GetEmployeesAsync(
        int pageNumber,
        int pageSize,
        string name,
        string email,
        string mobile,
        DateTime? dateOfBirth,
        string sortColumn,
        string sortDirection)
        {
            try
            {
                // Here I am Ensuring that datatable pageNumber is at least 1.
                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                // Here I am getting the total number of employees from the database.
                var totalRecords = await _context.Employees.CountAsync();

                // Here I am making a query for filtering the data.
                var query = _context.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(e => e.Name.Contains(name));
                }
                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(e => e.Email.Contains(email));
                }
                if (!string.IsNullOrEmpty(mobile))
                {
                    query = query.Where(e => e.Mobile.Contains(mobile));
                }
                if (dateOfBirth.HasValue)
                {
                    query = query.Where(e => e.DateOfBirth.Date == dateOfBirth.Value.Date);
                }

                // Here again I am counting the filered number.
                var filteredRecords = await query.CountAsync();

                // Here I am using this switch case for appling the soring
                switch (sortColumn)
                {
                    case "name":
                        query = sortDirection == "asc" ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name);
                        break;
                    case "email":
                        query = sortDirection == "asc" ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email);
                        break;
                    case "mobile":
                        query = sortDirection == "asc" ? query.OrderBy(e => e.Mobile) : query.OrderByDescending(e => e.Mobile);
                        break;
                    case "dateOfBirth":
                        query = sortDirection == "asc" ? query.OrderBy(e => e.DateOfBirth) : query.OrderByDescending(e => e.DateOfBirth);
                        break;
                    default:
                        query = query.OrderBy(e => e.Name); 
                        break;
                }

                // Here I am appling pagination 
                var employees = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (employees, totalRecords, filteredRecords);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
