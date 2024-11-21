using System;
using System.Collections.Generic;
using Core.Repository.DataContext;
using Microsoft.EntityFrameworkCore;
using UserManagementDBModel.EF.Models;

namespace UserManagementDBModel.Data;

public partial class SerializationContext : DataContext
{
    public SerializationContext()
    {
    }

    public SerializationContext(DbContextOptions<SerializationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC074EFD2D70");

            entity.Property(e => e.PhoneNumber).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
