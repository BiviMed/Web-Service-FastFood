using System.ComponentModel.DataAnnotations;
using WSFastFood.Validations;

namespace WSFastFood.Models.Dtos
{
    public class OrderDetailDto
    {
        private const string MSG = "Campo Obligatorio";

        [Required(ErrorMessage = MSG)]
        public int ProductId { get; set; }        

        [Required(ErrorMessage = MSG)]
        [Quantity]
        public int Quantity { get; set; }

        //public decimal Price { get; set; }

        //public decimal Amount { get; set; }
    }
}
