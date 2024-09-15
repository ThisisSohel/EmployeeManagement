using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class EmployeeEditViewModel
    {
        // Id is needed to identify the employee being edited
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number must be between 10 and 15 characters")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        // Optional property for the image file upload
        public IFormFile? Image { get; set; }

        // Property to display the current image
        public string? ImageUri { get; set; }
    }
}
