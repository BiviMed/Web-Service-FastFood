using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WSFastFood.Validations;

namespace WSFastFood.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Nombre")]
        public string Name { get; set; } = null!;

        [Required]
        [Price]
        [Display(Name = "Precio")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
    }
}
