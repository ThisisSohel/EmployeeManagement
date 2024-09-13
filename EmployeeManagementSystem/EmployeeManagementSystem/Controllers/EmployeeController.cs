using CustomException;
using Models.ViewModel;
using Service.EmployeesService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

		[HttpGet]
		public ActionResult Load()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult> LoadAllAsync()
		{
			var employeeViewModelList = new List<EmployeeViewModel>();

			try
			{
				var employeeList = await _employeeService.GetAllAsync();

				foreach (var employee in employeeList)
				{
					var supplierViewModel = new EmployeeViewModel
					{
						Id = employee.Id,
						EmployeeName = employee.EmployeeName,
						Email = employee.Email,
						Mobile = employee.Mobile,
						DateOfBirth = employee.DateOfBirth,
						Photo = employee.Photo,
					};
					employeeViewModelList.Add(supplierViewModel);
				}

			}
			catch (Exception ex)
			{

			}

			return Json(new
			{
				recordsTotal = employeeViewModelList.Count,
				recordsFiltered = employeeViewModelList.Count,
				data = employeeViewModelList,
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> CreateAsync(EmployeeCreateViewModel employeeCreateViewModel)
		{
			bool isValid = false;
			string message = string.Empty;

			try
			{
				if (employeeCreateViewModel != null)
				{
					await _employeeService.CreateAsync(employeeCreateViewModel);

					message = "Employee is created successfully!";
					isValid = true;
				}
				else
				{
					message = "Something is wrong! Please try again!";
				}

			}
			catch (DuplicateValueException ex)
			{
				message = ex.Message;
			}
			catch (DuplicateNumberException ex)
			{
				message = ex.Message;
			}
			catch (InvalidNameException ex)
			{
				message = ex.Message;
			}
			catch (InvalidExpressionException ex)
			{
				message = ex.Message;
			}
			catch (Exception ex)
			{
				message = "Something went wrong!";
			}

			return Json(new
			{
				Message = message,
				IsValid = isValid
			}, JsonRequestBehavior.AllowGet);
		}
	}
}