using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RegistroEstudiantes.Models
{
    public partial class RegistroEstudiantesContext : DbContext
    {
        public RegistroEstudiantesContext()
        {
        }

        public RegistroEstudiantesContext(DbContextOptions<RegistroEstudiantesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estudiantes> Estudiantes { get; set; }
        public virtual DbSet<Materias> Materias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=PANCHO-PC;Database=RegistroEstudiantes;User Id=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiantes>(entity =>
            {
                entity.HasKey(e => e.IdEstudiante);

                entity.ToTable("estudiantes");

                entity.Property(e => e.IdEstudiante).HasColumnName("id_Estudiante");

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IdMateria).HasColumnName("id_Materia");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__estudiant__id_Ma__398D8EEE");
            });

            modelBuilder.Entity<Materias>(entity =>
            {
                entity.HasKey(e => e.IdMateria);

                entity.ToTable("materias");

                entity.Property(e => e.IdMateria).HasColumnName("id_Materia");

                entity.Property(e => e.Nivel).HasColumnName("nivel");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
        }
    }
}
