using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Api_ProjectManagement.Models;

namespace Api_ProjectManagement.Database
{
    public partial class ProjectManagementDBContext : DbContext
    {
        public ProjectManagementDBContext()
        {
        }

        public ProjectManagementDBContext(DbContextOptions<ProjectManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Archivo> Archivos { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<HistorialTarea> HistorialTareas { get; set; } = null!;
        public virtual DbSet<Prioridad> Prioridads { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyectos { get; set; } = null!;
        public virtual DbSet<ProyectoUsuario> ProyectoUsuarios { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=LAPTOP-5FHPONOH;Database=ProjectManagementDB;User Id=UsrProjectManagement;Password=UsrProjectManagement2023;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasKey(e => e.IdArchivo)
                    .HasName("PK__Archivos__26B92111915179D8");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.UrlArchivo).HasMaxLength(500);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("PK__Comentar__DDBEFBF98A3FB52C");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdArchivoNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdArchivo)
                    .HasConstraintName("Fk_Comentario_Refe_Archivo");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdTarea)
                    .HasConstraintName("Fk_Comentario_Refe_Tarea");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_Comentario_Refe_Usuario");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado__FBB0EDC174FEDBD8");

                entity.ToTable("Estado");

                entity.HasIndex(e => e.Nombre, "UQ__Estado__75E3EFCF28CFE0B4")
                    .IsUnique();

                entity.Property(e => e.Nombre).HasMaxLength(30);
            });

            modelBuilder.Entity<HistorialTarea>(entity =>
            {
                entity.HasKey(e => e.IdHistorialTarea)
                    .HasName("PK__Historia__7A8790175C09C187");

                entity.Property(e => e.FechaCambio).HasColumnType("datetime");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.HistorialTareas)
                    .HasForeignKey(d => d.IdTarea)
                    .HasConstraintName("Fk_HistorialTareas_Refe_Tarea");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistorialTareas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_HistorialTareas_Refe_Usuario");
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("PK__Priorida__0FC70DD51699884F");

                entity.ToTable("Prioridad");

                entity.HasIndex(e => e.Nombre, "UQ__Priorida__75E3EFCF523FD037")
                    .IsUnique();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PK__Proyecto__F4888673C879F5DA");

                entity.Property(e => e.Descripcion).HasMaxLength(800);

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("Fk_Proyecto_Refe_Estado");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_Proyecto_Refe_Usuario");
            });

            modelBuilder.Entity<ProyectoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdProyectoUsuarios)
                    .HasName("PK__Proyecto__EFCF35317220DAE9");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.ProyectoUsuarios)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("Fk_royectoUsuarios_Refe_Proyectos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ProyectoUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_ProyectoUsuarios_Refe_Usuario");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Roles__2A49584CA6E9E677");

                entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCF8BD76D68")
                    .IsUnique();

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.IdTarea)
                    .HasName("PK__Tareas__EADE909808C2F6F3");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.HasOne(d => d.IdArchivoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdArchivo)
                    .HasConstraintName("Fk_Tareas_Refe_Archivos");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("Fk_Tareas_Refe_Estado");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdPrioridad)
                    .HasConstraintName("Fk_Tareas_Refe_Prioridad");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("Fk_Tareas_Refe_Proyecto");

                entity.HasOne(d => d.IdTareaPadreNavigation)
                    .WithMany(p => p.InverseIdTareaPadreNavigation)
                    .HasForeignKey(d => d.IdTareaPadre)
                    .HasConstraintName("Fk_Tareas_Refe_Tareas");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_Tareas_Refe_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__5B65BF97342D1871");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Contrasenia).HasMaxLength(300);

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Foto).HasMaxLength(500);

                entity.Property(e => e.Nombres).HasMaxLength(50);
            });

            modelBuilder.Entity<UsuariosRole>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioRol)
                    .HasName("PK__Usuarios__6806BF4AB664B3D5");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuariosRoles)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("Fk_UsuarioRol_Refe_Rol");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuariosRoles)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_UsuarioRol_Refe_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
