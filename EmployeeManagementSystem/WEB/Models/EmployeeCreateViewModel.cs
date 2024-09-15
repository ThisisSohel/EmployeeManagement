using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
	public class EmployeeCreateViewModel
	{
		[Required(ErrorMessage = "Name is required")]
		[StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 60 characters")]
		public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Mobile number is required")]
		[StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number must be between 10 and 15 numeric number")]
		public string Mobile { get; set; }

		[Required(ErrorMessage = "Date of birth is required")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime DateOfBirth { get; set; }
		public IFormFile Image { get; set; }
	}
}
