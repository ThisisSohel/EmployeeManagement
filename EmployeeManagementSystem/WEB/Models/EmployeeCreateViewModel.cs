using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 60 characters")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name can only contain letters and spaces")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 60 characters")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name can only contain letters and spaces")]
        public string Lname { get; set; }

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
        public IFormFile Image { get; set; }
    }

}
