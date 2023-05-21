using System.ComponentModel.DataAnnotations;
using WSFastFood.Models.Entities;

namespace WSFastFood.Models.Dtos
{
    public class GenerateOrderDto
    {
        private const string MSG = "Campo Obligatorio";


        [Required(ErrorMessage = MSG)]
        public int UserId { get; set; }

        [Required(ErrorMessage = MSG)]
        public List<OrderDetailDto> OrderDetails { get; set; } = null!;

        //public DateTime Date { get; set; }

        //public int Total { get; set; }
    }
}
