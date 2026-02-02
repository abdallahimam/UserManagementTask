using System.ComponentModel.DataAnnotations;

namespace UserManagementTask.WebUI.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(100)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Password { get; set; }
    }
}
