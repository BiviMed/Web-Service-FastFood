using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WSFastFood.Models.Entities;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(70)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Salt { get; set; } = null!;

    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
