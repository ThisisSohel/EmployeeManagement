using DAO.Entities;
using DAO.Repositories;
using Service.DTO;
using Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeService(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			try
			{
				return await _employeeRepository.GetAllEmployeesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Internal Server Error!.", ex);
			}
		}

		public async Task<Employee> GetEmployeeByIdAsync(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Invalid Employee ID");

			try
			{
				var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
				if (employee == null)
					throw new KeyNotFoundException("Employee not found");

				return employee;
			}
			catch (Exception ex)
			{
				throw new Exception("Internal Server Error!.", ex);
			}
		}

		public async Task AddEmployeeAsync(Employee employee)
		{
			try
			{
				ValidateEmployee(employee);
                var employeeListToCheckDupliacte = (List<Employee>)await _employeeRepository.GetAllEmployeesAsync();

				foreach(var employeeProperty in employeeListToCheckDupliacte)
				{
					if(employee.Email == employeeProperty.Email)
					{
						throw new Exception("Email can not be duplicate!");
					}
                    if (employee.Mobile == employeeProperty.Mobile)
                    {
                        throw new Exception("Mobile number can not be duplicate!");
                    }
                }

				await _employeeRepository.AddEmployeeAsync(employee);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException("Validation error: " + ex.Message, ex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task UpdateEmployeeAsync(Employee employee)
		{
			if (employee.Id <= 0)
				throw new ArgumentException("Invalid Employee ID");

			try
			{
				ValidateEmployee(employee);

                var employeeListToCheckDupliacte = (List<Employee>)await _employeeRepository.GetAllEmployeesAsync();

                foreach (var employeeProperty in employeeListToCheckDupliacte)
                {
                    if (employee.Email == employeeProperty.Email && employee.Id != employeeProperty.Id)
                    {
                        throw new Exception("Email can not be duplicate!");
                    }
                    if (employee.Mobile == employeeProperty.Mobile && employee.Id != employeeProperty.Id)
                    {
                        throw new Exception("Mobile number can not be duplicate!");
                    }
                }

                var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.Id);
				if (existingEmployee == null)
					throw new KeyNotFoundException("Employee not found");

				await _employeeRepository.UpdateEmployeeAsync(employee);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException("Validation error: " + ex.Message, ex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task DeleteEmployeeAsync(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Invalid Employee ID");

			try
			{
				var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
				if (employee == null)
					throw new KeyNotFoundException("Employee not found");

				await _employeeRepository.DeleteEmployeeAsync(id);
			}
			catch (KeyNotFoundException ex)
			{
				throw new KeyNotFoundException("Deletion error: " + ex.Message, ex);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Private helper method to validate Employee object
		private void ValidateEmployee(Employee employee)
		{
			if (string.IsNullOrWhiteSpace(employee.Name))
				throw new ArgumentException("Employee name is required");

			if (string.IsNullOrWhiteSpace(employee.Email))
				throw new ArgumentException("Employee email is required");

			if (!IsValidEmail(employee.Email))
				throw new ArgumentException("Invalid email format");

			if (string.IsNullOrWhiteSpace(employee.Mobile))
				throw new ArgumentException("Employee mobile number is required");

			if (!IsValidMobile(employee.Mobile))
				throw new ArgumentException("Invalid mobile number format");

			if (employee.DateOfBirth == default)
				throw new ArgumentException("Date of birth is required");

			if (employee.DateOfBirth > DateTime.Now)
				throw new ArgumentException("Date of birth cannot be in the future");
		}

		//Custom email validation private method
		private bool IsValidEmail(string email)
		{
			return email.Contains("@") && email.Contains(".");
		}

        //Custom mobile number validation private method
        private bool IsValidMobile(string mobile)
		{
			return mobile.Length >= 10 && mobile.Length <= 15;
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
                return await _employeeRepository.GetEmployeesAsync(
                    pageNumber,
                    pageSize,
                    name,
                    email,
                    mobile,
                    dateOfBirth,
                    sortColumn,
                    sortDirection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
