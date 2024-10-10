using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmarchivosBackend.Models;

public partial class AlmarchivosContext : DbContext
{
    public AlmarchivosContext()
    {
    }

    public AlmarchivosContext(DbContextOptions<AlmarchivosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ING_CAMILO_C;Database=almarchivos;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__214BF527A757BA3D");

            entity.Property(e => e.IdPersona).HasColumnName("id_Persona");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(42)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdTipoId).HasColumnName("id_tipo_id");
            entity.Property(e => e.Nombres)
                .HasMaxLength(42)
                .IsUnicode(false);
            entity.Property(e => e.NumeroIdentificacion).HasColumnName("Numero_identificacion");

            entity.HasOne(d => d.IdTipo).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdTipoId)
                .HasConstraintName("FK_ti_per");
        });

        modelBuilder.Entity<TipoIdentificacion>(entity =>
        {
            entity.HasKey(e => e.IdTi).HasName("PK__tipo_ide__014840CAD2055480");

            entity.ToTable("tipo_identificacion");

            entity.Property(e => e.IdTi).HasColumnName("id_ti");
            entity.Property(e => e.TipoIdentificacion1)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("tipo_identificacion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD9D753FAE");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.FechaCreacion)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdPersona).HasColumnName("id_Persona");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(42)
                .IsUnicode(false)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_Usuario_Personas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
