using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WSFastFood.Models.Dtos
{
    public class EditUserDto
    {
        private const string MSG = "Campo Obligatorio";

        [Required(ErrorMessage = MSG)]
        public int Id { get; set; }

        [Required(ErrorMessage = MSG)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = MSG)]
        public string LastName { get; set; } = null!;

        public string? Phone { get; set; } = null!;

        [Required(ErrorMessage = MSG)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = MSG)]
        public string Role { get; set; } = null!;
    }
}
