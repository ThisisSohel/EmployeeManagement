using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 60 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number must be between 10 and 15 numeric digits")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile? Image { get; set; }

        public string? ImageUri { get; set; }
    }
}
