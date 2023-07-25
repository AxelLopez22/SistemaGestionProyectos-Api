using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_ProjectManagement.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archivos",
                columns: table => new
                {
                    IdArchivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Archivos__26B92111915179D8", x => x.IdArchivo);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estado__FBB0EDC174FEDBD8", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "Prioridad",
                columns: table => new
                {
                    IdPrioridad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Priorida__0FC70DD51699884F", x => x.IdPrioridad);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__2A49584CA6E9E677", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__5B65BF97342D1871", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    IdProyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FechaFin = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proyecto__F4888673C879F5DA", x => x.IdProyecto);
                    table.ForeignKey(
                        name: "Fk_Proyecto_Refe_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado");
                    table.ForeignKey(
                        name: "Fk_Proyecto_Refe_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    IdUsuarioRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    IdRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__6806BF4AB664B3D5", x => x.IdUsuarioRol);
                    table.ForeignKey(
                        name: "Fk_UsuarioRol_Refe_Rol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                    table.ForeignKey(
                        name: "Fk_UsuarioRol_Refe_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ProyectoUsuarios",
                columns: table => new
                {
                    IdProyectoUsuarios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    IdProyecto = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proyecto__EFCF35317220DAE9", x => x.IdProyectoUsuarios);
                    table.ForeignKey(
                        name: "Fk_ProyectoUsuarios_Refe_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "Fk_royectoUsuarios_Refe_Proyectos",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "IdProyecto");
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    FechaFin = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    IdProyecto = table.Column<int>(type: "int", nullable: true),
                    IdTareaPadre = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    IdArchivo = table.Column<int>(type: "int", nullable: true),
                    IdPrioridad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tareas__EADE909808C2F6F3", x => x.IdTarea);
                    table.ForeignKey(
                        name: "Fk_Tareas_Refe_Archivos",
                        column: x => x.IdArchivo,
                        principalTable: "Archivos",
                        principalColumn: "IdArchivo");
                    table.ForeignKey(
                        name: "Fk_Tareas_Refe_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado");
                    table.ForeignKey(
                        name: "Fk_Tareas_Refe_Prioridad",
                        column: x => x.IdPrioridad,
                        principalTable: "Prioridad",
                        principalColumn: "IdPrioridad");
                    table.ForeignKey(
                        name: "Fk_Tareas_Refe_Proyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "IdProyecto");
                    table.ForeignKey(
                        name: "Fk_Tareas_Refe_Tareas",
                        column: x => x.IdTareaPadre,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea");
                    table.ForeignKey(
                        name: "Fk_Tareas_Refe_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    IdComentario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    IdTarea = table.Column<int>(type: "int", nullable: true),
                    IdArchivo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comentar__DDBEFBF98A3FB52C", x => x.IdComentario);
                    table.ForeignKey(
                        name: "Fk_Comentario_Refe_Archivo",
                        column: x => x.IdArchivo,
                        principalTable: "Archivos",
                        principalColumn: "IdArchivo");
                    table.ForeignKey(
                        name: "Fk_Comentario_Refe_Tarea",
                        column: x => x.IdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea");
                    table.ForeignKey(
                        name: "Fk_Comentario_Refe_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "HistorialTareas",
                columns: table => new
                {
                    IdHistorialTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTarea = table.Column<int>(type: "int", nullable: true),
                    IdEstadoNuevo = table.Column<int>(type: "int", nullable: true),
                    FechaCambio = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__7A8790175C09C187", x => x.IdHistorialTarea);
                    table.ForeignKey(
                        name: "Fk_HistorialTareas_Refe_Tarea",
                        column: x => x.IdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea");
                    table.ForeignKey(
                        name: "Fk_HistorialTareas_Refe_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdArchivo",
                table: "Comentarios",
                column: "IdArchivo");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdTarea",
                table: "Comentarios",
                column: "IdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuario",
                table: "Comentarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "UQ__Estado__75E3EFCF28CFE0B4",
                table: "Estado",
                column: "Nombre",
                unique: true,
                filter: "[Nombre] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialTareas_IdTarea",
                table: "HistorialTareas",
                column: "IdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialTareas_IdUsuario",
                table: "HistorialTareas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "UQ__Priorida__75E3EFCF523FD037",
                table: "Prioridad",
                column: "Nombre",
                unique: true,
                filter: "[Nombre] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdEstado",
                table: "Proyectos",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_IdUsuario",
                table: "Proyectos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoUsuarios_IdProyecto",
                table: "ProyectoUsuarios",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoUsuarios_IdUsuario",
                table: "ProyectoUsuarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__75E3EFCF8BD76D68",
                table: "Roles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdArchivo",
                table: "Tareas",
                column: "IdArchivo");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdEstado",
                table: "Tareas",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdPrioridad",
                table: "Tareas",
                column: "IdPrioridad");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdProyecto",
                table: "Tareas",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdTareaPadre",
                table: "Tareas",
                column: "IdTareaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdUsuario",
                table: "Tareas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_IdRol",
                table: "UsuariosRoles",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_IdUsuario",
                table: "UsuariosRoles",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "HistorialTareas");

            migrationBuilder.DropTable(
                name: "ProyectoUsuarios");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Archivos");

            migrationBuilder.DropTable(
                name: "Prioridad");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
