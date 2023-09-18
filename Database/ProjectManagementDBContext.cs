using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Api_ProjectManagement.Models;
using Api_ProjectManagement.Common.DTOs;

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
        public virtual DbSet<AsignacionIncidencium> AsignacionIncidencia { get; set; } = null!;
        public virtual DbSet<CategoriaIncidencia> CategoriaIncidencias { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<GruposNotificacione> GruposNotificaciones { get; set; } = null!;
        public virtual DbSet<HistorialIncidencia> HistorialIncidencias { get; set; } = null!;
        public virtual DbSet<HistorialTarea> HistorialTareas { get; set; } = null!;
        public virtual DbSet<Incidencia> Incidencias { get; set; } = null!;
        public virtual DbSet<Nota> Notas { get; set; } = null!;
        public virtual DbSet<Prioridad> Prioridads { get; set; } = null!;
        public virtual DbSet<PrioridadIncidencium> PrioridadIncidencia { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyectos { get; set; } = null!;
        public virtual DbSet<ProyectoUsuario> ProyectoUsuarios { get; set; } = null!;
        public virtual DbSet<Reproducibilidad> Reproducibilidads { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Severidad> Severidads { get; set; } = null!;
        public virtual DbSet<Sistema> Sistemas { get; set; } = null!;
        public virtual DbSet<StatusIncidencia> StatusIncidencias { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; } = null!;
        public virtual DbSet<UsuariosSistema> UsuariosSistemas { get; set; } = null!;
        public virtual DbSet<VersionSistema> VersionSistemas { get; set; } = null!;
        public virtual DbSet<sp_ListarTareas> Sp_ListarTareas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=AXEL_LOPEZ;Database=ProjectManagementDB;User Id=sa;Password=AxelLopez;Trusted_Connection=False;");
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

            modelBuilder.Entity<AsignacionIncidencium>(entity =>
            {
                entity.HasKey(e => e.IdAsignacion)
                    .HasName("PK__Asignaci__A7235DFFA1337D65");

                entity.ToTable("AsignacionIncidencia", "HelpDesk");

                entity.HasOne(d => d.IdIncidenciaNavigation)
                    .WithMany(p => p.AsignacionIncidencia)
                    .HasForeignKey(d => d.IdIncidencia)
                    .HasConstraintName("Fk_Asignacion_Refe_Incidencia");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.AsignacionIncidencia)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_Asignacion_Refe_Usuarios");
            });

            modelBuilder.Entity<CategoriaIncidencia>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A10E7630E72");

                entity.ToTable("CategoriaIncidencias", "HelpDesk");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("PK__Comentar__DDBEFBF98A3FB52C");

                entity.HasIndex(e => e.IdArchivo, "IX_Comentarios_IdArchivo");

                entity.HasIndex(e => e.IdTarea, "IX_Comentarios_IdTarea");

                entity.HasIndex(e => e.IdUsuario, "IX_Comentarios_IdUsuario");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

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
                    .IsUnique()
                    .HasFilter("([Nombre] IS NOT NULL)");

                entity.Property(e => e.Nombre).HasMaxLength(30);
            });

            modelBuilder.Entity<GruposNotificacione>(entity =>
            {
                entity.Property(e => e.IdConexion).HasMaxLength(200);
            });

            modelBuilder.Entity<HistorialIncidencia>(entity =>
            {
                entity.ToTable("HistorialIncidencias", "HelpDesk");

                entity.Property(e => e.AntiguoValor).HasMaxLength(200);

                entity.Property(e => e.NombreCampo).HasMaxLength(200);

                entity.Property(e => e.NuevoValor).HasMaxLength(200);

                entity.HasOne(d => d.IdIncidenciaNavigation)
                    .WithMany(p => p.HistorialIncidencia)
                    .HasForeignKey(d => d.IdIncidencia)
                    .HasConstraintName("Fk_Historial_Refe_Incidencia");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.HistorialIncidencia)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_Historial_Refe_Usuario");
            });

            modelBuilder.Entity<HistorialTarea>(entity =>
            {
                entity.HasKey(e => e.IdHistorialTarea)
                    .HasName("PK__Historia__7A8790175C09C187");

                entity.HasIndex(e => e.IdTarea, "IX_HistorialTareas_IdTarea");

                entity.HasIndex(e => e.IdUsuario, "IX_HistorialTareas_IdUsuario");

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

            modelBuilder.Entity<Incidencia>(entity =>
            {
                entity.HasKey(e => e.IdIncidencia)
                    .HasName("PK__Incidenc__542A8F6C4409A022");

                entity.ToTable("Incidencias", "HelpDesk");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Resumen).HasMaxLength(200);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("Fk_Incidencia_Refe_Categoria");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdPrioridad)
                    .HasConstraintName("Fk_Incidencia_Refe_Prioridad");

                entity.HasOne(d => d.IdReproducibilidadNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdReproducibilidad)
                    .HasConstraintName("Fk_Incidencia_Refe_Reproducibilidad");

                entity.HasOne(d => d.IdSeveridadNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdSeveridad)
                    .HasConstraintName("Fk_Incidencia_Refe_Severidad");

                entity.HasOne(d => d.IdSistemaNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdSistema)
                    .HasConstraintName("Fk_Incidencias_Refe_Sistema");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdStatus)
                    .HasConstraintName("FK_Incidencia_Refe_Status");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Incidencia_Refe_Usuarios");
            });

            modelBuilder.Entity<Nota>(entity =>
            {
                entity.HasKey(e => e.IdNota)
                    .HasName("PK__Notas__4B2ACFF251FF0A2C");

                entity.ToTable("Notas", "HelpDesk");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdIncidenciaNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdIncidencia)
                    .HasConstraintName("Fk_Notas_Refe_Incidencia");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_Notas_Refe_Usuario");
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("PK__Priorida__0FC70DD51699884F");

                entity.ToTable("Prioridad");

                entity.HasIndex(e => e.Nombre, "UQ__Priorida__75E3EFCF523FD037")
                    .IsUnique()
                    .HasFilter("([Nombre] IS NOT NULL)");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<PrioridadIncidencium>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("PK__Priorida__0FC70DD5FE79F6F7");

                entity.ToTable("PrioridadIncidencia", "HelpDesk");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PK__Proyecto__F4888673C879F5DA");

                entity.HasIndex(e => e.IdEstado, "IX_Proyectos_IdEstado");

                entity.HasIndex(e => e.IdUsuario, "IX_Proyectos_IdUsuario");

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

                entity.HasIndex(e => e.IdProyecto, "IX_ProyectoUsuarios_IdProyecto");

                entity.HasIndex(e => e.IdUsuario, "IX_ProyectoUsuarios_IdUsuario");

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

            modelBuilder.Entity<Reproducibilidad>(entity =>
            {
                entity.HasKey(e => e.IdReproducibilidad)
                    .HasName("PK__Reproduc__5B7E6014D9E043C0");

                entity.ToTable("Reproducibilidad", "HelpDesk");

                entity.Property(e => e.Nombre).HasMaxLength(100);
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

            modelBuilder.Entity<Severidad>(entity =>
            {
                entity.HasKey(e => e.IdSeveridad)
                    .HasName("PK__Severida__96959DB1D7F75059");

                entity.ToTable("Severidad", "HelpDesk");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Sistema>(entity =>
            {
                entity.HasKey(e => e.IdSistema)
                    .HasName("PK__Sistemas__48B026F4809200EB");

                entity.ToTable("Sistemas", "HelpDesk");

                entity.Property(e => e.Descripcion).HasMaxLength(1000);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(300);
            });

            modelBuilder.Entity<StatusIncidencia>(entity =>
            {
                entity.HasKey(e => e.IdEstatus)
                    .HasName("PK__StatusIn__B32BA1C7B34AF0B5");

                entity.ToTable("StatusIncidencias", "HelpDesk");

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.IdTarea)
                    .HasName("PK__Tareas__EADE909808C2F6F3");

                entity.HasIndex(e => e.IdArchivo, "IX_Tareas_IdArchivo");

                entity.HasIndex(e => e.IdEstado, "IX_Tareas_IdEstado");

                entity.HasIndex(e => e.IdPrioridad, "IX_Tareas_IdPrioridad");

                entity.HasIndex(e => e.IdProyecto, "IX_Tareas_IdProyecto");

                entity.HasIndex(e => e.IdTareaPadre, "IX_Tareas_IdTareaPadre");

                entity.HasIndex(e => e.IdUsuario, "IX_Tareas_IdUsuario");

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

                entity.HasIndex(e => e.IdRol, "IX_UsuariosRoles_IdRol");

                entity.HasIndex(e => e.IdUsuario, "IX_UsuariosRoles_IdUsuario");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuariosRoles)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("Fk_UsuarioRol_Refe_Rol");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuariosRoles)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("Fk_UsuarioRol_Refe_Usuario");
            });

            modelBuilder.Entity<UsuariosSistema>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioSistema)
                    .HasName("PK__Usuarios__090CF683E6BD8460");

                entity.ToTable("UsuariosSistemas", "HelpDesk");

                entity.HasOne(d => d.IdSistemaNavigation)
                    .WithMany(p => p.UsuariosSistemas)
                    .HasForeignKey(d => d.IdSistema)
                    .HasConstraintName("Fk_UsuariosSistemas_Refe_Sistemas");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuariosSistemas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_UsuariosSistemas_Refe_Usuarios");
            });

            modelBuilder.Entity<VersionSistema>(entity =>
            {
                entity.HasKey(e => e.IdVersion)
                    .HasName("PK__VersionS__8C53B80973DBF122");

                entity.ToTable("VersionSistema", "HelpDesk");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.VersionProyecto).HasMaxLength(50);

                entity.HasOne(d => d.IdSistemaNavigation)
                    .WithMany(p => p.VersionSistemas)
                    .HasForeignKey(d => d.IdSistema)
                    .HasConstraintName("Fk_Version_Refe_Sistema");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
