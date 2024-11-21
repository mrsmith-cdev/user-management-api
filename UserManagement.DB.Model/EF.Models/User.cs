using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserManagementDBModel.EF.Models;

[Index("Email", Name = "UQ__Users__A9D10534478CCC02", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string FirstName { get; set; } = null!;

    [StringLength(128)]
    public string? LastName { get; set; }

    [StringLength(256)]
    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;
}
