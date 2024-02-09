using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAPI.Models;

public partial class UsuariosOesatContext : DbContext
{
    public UsuariosOesatContext()
    {
    }

    public UsuariosOesatContext(DbContextOptions<UsuariosOesatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=E000158\\SQLEXPRESS;Initial Catalog=UsuariosOESAT;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF9711EF52A6");

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(12);
            entity.Property(e => e.Rol).HasMaxLength(10);
            entity.Property(e => e.User).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
