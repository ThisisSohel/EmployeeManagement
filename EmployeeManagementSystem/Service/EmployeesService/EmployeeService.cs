using Models.ViewModel;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.EmployeeRepossitory;
using DAO;
using Models;
using System.Text.RegularExpressions;
using CustomException;
using System.Data;

namespace Service.EmployeesService
{
	public interface IEmployeeService
	{
		Task CreateAsync(EmployeeCreateViewModel employeeCreateViewModel);
		Task<List<EmployeeViewModel>> GetAllAsync();
		//Task<EmployeeViewModel> GetByIdAsync(long id);
		//Task UpdateAsync(EmployeeUpdateViewModel model);
		//Task DeleteAsync(long id);
	}
	public class EmployeeService: IEmployeeService
	{
		private readonly IEmployeeDAO _employeeDAO ;
		private readonly ISession _session;
		private readonly ISessionFactory _sessionFactory;

		public EmployeeService(IEmployeeDAO employeeDAO) 
		{
			_employeeDAO = employeeDAO;
		}

		// Here in the contructor I am opening the session for EmployeeDAO
        public EmployeeService()
        {
			_sessionFactory = NHibernateConfig.GetSession();
			_session = _sessionFactory.OpenSession();
			_employeeDAO = new EmployeeDAO(_session);
		}

		public async Task<List<EmployeeViewModel>> GetAllAsync()
		{
			var emoloyee = new List<Employee>();
			var employeeView = new List<EmployeeViewModel>();

			try
			{
				emoloyee = await _employeeDAO.GetAll();

				employeeView = emoloyee.Select(s => new EmployeeViewModel
				{
					Id = s.Id,
					EmployeeName = s.EmployeeName,
					Email = s.Email,
					Mobile = s.Mobile,
					DateOfBirth = s.DateOfBirth,
					Photo = s.Photo,
				}).ToList();
				return employeeView;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// This is an asynchoronous method to create a new employee
		public async Task CreateAsync(EmployeeCreateViewModel employeeCreateViewModel)
		{
			try
			{
				var employeeMainEntity = new Employee();
				var employeeList = await _employeeDAO.GetAll();

				//ModelValidatorMethod(employeeCreateViewModel);

				if(employeeList.Count > 0)
				{
					foreach (var employee in employeeList)
					{
						if (employeeCreateViewModel.Email == employee.Email)
						{
							throw new DuplicateValueException("Email can not be duplicate!");
						}
						if (employeeCreateViewModel.Mobile == employee.Mobile)
						{
							throw new DuplicateNumberException("Mobile number can not be duplicate!");
						}
					}
				}

				using (var transaction =  _session.BeginTransaction())
				{
					employeeMainEntity.EmployeeName = employeeCreateViewModel.EmployeeName;
					employeeMainEntity.Email = employeeCreateViewModel.Email;
					employeeMainEntity.Mobile = employeeCreateViewModel.Mobile;
					employeeMainEntity.DateOfBirth = employeeCreateViewModel.DateOfBirth;
					employeeMainEntity.Photo = employeeCreateViewModel.Photo;

					await _employeeDAO.CreateAsync(employeeMainEntity);
					await transaction.CommitAsync();
				}
			}
			catch(DuplicateNumberException ex)
			{
				throw ex;
			}
			catch(DuplicateValueException ex)
			{
				throw ex;
			}
			catch(InvalidExpressionException ex)
			{
				throw ex;
			}
			catch(InvalidNameException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Createing a private method to validate all the properties!
		private void ModelValidatorMethod(EmployeeCreateViewModel modelToValidate)
		{
			if (string.IsNullOrWhiteSpace(modelToValidate.EmployeeName))
			{
				throw new InvalidNameException("Name can not be empty!");
			}

			if (modelToValidate.EmployeeName?.Trim().Length < 3 || modelToValidate.EmployeeName?.Trim().Length > 30)
			{
				throw new InvalidNameException("Name character should be in between 3 to 30!");
			}

			if (!Regex.IsMatch(modelToValidate.EmployeeName, @"^[a-zA-Z ]+$"))
			{
				throw new InvalidNameException("Name can not contain numbers or special characters! Please input alphabetic characters and space only!");
			}

			if (modelToValidate.Email?.Trim() == null)
			{
				throw new InvalidNameException("Email can not be null");
			}

			if (!Regex.IsMatch(modelToValidate.Email, @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"))
			{
				throw new InvalidExpressionException("Please enter valid email");
			}

			if (!Regex.IsMatch(modelToValidate.Mobile, @"^([0-9\(\)\/\+ \-]*)$"))
			{
				throw new InvalidExpressionException("Invalid number! Please input correct format number!");
			}

			if (modelToValidate.Mobile == null)
			{
				throw new InvalidNameException("Number can not be null");
			}

			if (modelToValidate.DateOfBirth == null)
			{
				throw new InvalidNameException("BOB can not be null");
			}

			if (modelToValidate.Photo == null)
			{
				throw new InvalidNameException("Please upload employee photo");
			}
		}
	}
}
