using DAO.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Request;
using Service.Services;
using WEB.Models;

namespace WEB.Controllers
{
	public class EmployeeController : Controller
	{
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
		{
			return View();
		}

        [HttpGet]
        public async Task<IActionResult> GetEmployees(int draw, int start, int length, string name, string email, string mobile, DateTime? dateOfBirth,  string sortColumn, string sortDirection)
        {
            var pageNumber = (start / length) + 1;
            var pageSize = length;

            var (employees, totalRecords, filteredRecords) = await _employeeService.GetEmployeesAsync(
                pageNumber,
                pageSize,
                name,
                email,
                mobile,
                dateOfBirth,
                sortColumn,
                sortDirection);

            return Json(new
            {
                draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = employees
            });
        }

        public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fullName = model.Fname + " " + model.Lname;

                    var entity = new Employee
                    {
                        Name = fullName,
                        Email = model.Email,
                        Mobile = model.Mobile,
                        DateOfBirth = model.DateOfBirth,
                    };

                    if (model.Image != null && model.Image.Length > 0)
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        var filePath = Path.Combine(uploads, uniqueFileName);

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        entity.ImageUri = $"/images/{uniqueFileName}";
                    }

                    await _employeeService.AddEmployeeAsync(entity);

                    TempData["SuccessMessage"] = "Employee added successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var employee = new Employee();
            var model = new EmployeeEditViewModel();
            try
            {
                employee = _employeeService.GetEmployeeByIdAsync(id).Result;

                if (employee == null)
                {
                    return NotFound();
                }
                model.Id = employee.Id;
                model.Name = employee.Name;
                model.Email = employee.Email;
                model.Mobile = employee.Mobile;
                model.DateOfBirth = employee.DateOfBirth;
                model.ImageUri = employee.ImageUri;
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = await _employeeService.GetEmployeeByIdAsync(id);
                    if (entity == null)
                    {
                        return NotFound();
                    }

                    entity.Name = model.Name;
                    entity.Email = model.Email;
                    entity.Mobile = model.Mobile;
                    entity.DateOfBirth = model.DateOfBirth;

                    if (model.Image != null && model.Image.Length > 0)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(model.Image.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        var filePath = Path.Combine(uploads, fileName);

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        entity.ImageUri = $"/images/{fileName}";
                    }

                    await _employeeService.UpdateEmployeeAsync(entity);

                    TempData["SuccessMessage"] = "Employee updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                TempData["SuccessMessage"] = "Employee deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
