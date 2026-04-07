using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SistemadeAvisosEscolares.Models.Entities;

public partial class AvisosEscolaresContext : DbContext
{
    public AvisosEscolaresContext()
    {
    }

    public AvisosEscolaresContext(DbContextOptions<AvisosEscolaresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumnos> Alumnos { get; set; }

    public virtual DbSet<Avisos> Avisos { get; set; }

    public virtual DbSet<Avisosalumnos> Avisosalumnos { get; set; }

    public virtual DbSet<Maestros> Maestros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=AvisosEscolares", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alumnos>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PRIMARY");

            entity.ToTable("alumnos");

            entity.Property(e => e.Matricula).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<Avisos>(entity =>
        {
            entity.HasKey(e => e.IdAviso).HasName("PRIMARY");

            entity.ToTable("avisos");

            entity.Property(e => e.Contenido).HasColumnType("text");
            entity.Property(e => e.FechaCaducidad).HasColumnType("datetime");
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.TipoAviso).HasMaxLength(20);
            entity.Property(e => e.Titulo).HasMaxLength(200);
        });

        modelBuilder.Entity<Avisosalumnos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("avisosalumnos");

            entity.Property(e => e.FechaLeido).HasColumnType("datetime");
            entity.Property(e => e.FechaRecibido).HasColumnType("datetime");
        });

        modelBuilder.Entity<Maestros>(entity =>
        {
            entity.HasKey(e => e.IdMaestro).HasName("PRIMARY");

            entity.ToTable("maestros");

            entity.Property(e => e.Grupo).HasMaxLength(20);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
