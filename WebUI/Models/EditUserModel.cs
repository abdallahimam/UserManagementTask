using System.ComponentModel.DataAnnotations;

namespace UserManagementTask.WebUI.Models
{
    public class EditUserModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max length for Username should be 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username can only contain alphabets and numbers.")]
        public string? Username { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Max length for UserFullName should be 200 characters.")]
        [RegularExpression("^[a-zA-Z]+( [a-zA-Z]+)*$", ErrorMessage = "UserFullName can only contain alphabets.")]
        public string? UserFullName { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Max Length for Password should be 200 characters.")]
        [MinLength(6, ErrorMessage = "Min Length for Password should be 6 characters.")]
        public string? Password { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Max Length for ConfirmPassword should be 200 characters.")]
        [MinLength(6, ErrorMessage = "Min Length for ConfirmPassword should be 6 characters.")]
        [Compare("Password", ErrorMessage = "Not matched password.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
    }
}
