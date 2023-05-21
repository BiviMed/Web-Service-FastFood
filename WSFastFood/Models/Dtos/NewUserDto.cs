using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WSFastFood.Models.Dtos
{   
    public class NewUserDto
    {
        private const string MSG = "Campo Obligatorio";

        [Required(ErrorMessage = MSG)]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = MSG)]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(30)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = MSG)]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = MSG)]
        [StringLength(70)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = MSG)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = MSG)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string VerifyPassword { get; set; } = null!;

        [StringLength(30)]
        public string? Role { get; set; } = null!;

        
    }
}
