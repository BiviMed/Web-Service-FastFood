using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WSFastFood.Models.Entities;

[Table("Order")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
