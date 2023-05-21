using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WSFastFood.Models.Entities;

[Table("OrderDetail")]
public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Amount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;
}
