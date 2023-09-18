IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Archivos] (
    [IdArchivo] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    [UrlArchivo] nvarchar(500) NOT NULL,
    CONSTRAINT [PK__Archivos__26B92111915179D8] PRIMARY KEY ([IdArchivo])
);
GO

CREATE TABLE [Estado] (
    [IdEstado] int NOT NULL IDENTITY,
    [Nombre] nvarchar(30) NULL,
    CONSTRAINT [PK__Estado__FBB0EDC174FEDBD8] PRIMARY KEY ([IdEstado])
);
GO

CREATE TABLE [Prioridad] (
    [IdPrioridad] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NULL,
    CONSTRAINT [PK__Priorida__0FC70DD51699884F] PRIMARY KEY ([IdPrioridad])
);
GO

CREATE TABLE [Roles] (
    [IdRol] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK__Roles__2A49584CA6E9E677] PRIMARY KEY ([IdRol])
);
GO

CREATE TABLE [Sp_ListarTareas] (
    [IdTarea] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NOT NULL,
    [FechaFin] datetime2 NOT NULL,
    [Prioridad] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Sp_ListarTareas] PRIMARY KEY ([IdTarea])
);
GO

CREATE TABLE [Usuarios] (
    [IdUsuario] int NOT NULL IDENTITY,
    [Nombres] nvarchar(50) NOT NULL,
    [Apellidos] nvarchar(50) NOT NULL,
    [Correo] nvarchar(100) NOT NULL,
    [Contrasenia] nvarchar(300) NOT NULL,
    [Foto] nvarchar(500) NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK__Usuarios__5B65BF97342D1871] PRIMARY KEY ([IdUsuario])
);
GO

CREATE TABLE [Proyectos] (
    [IdProyecto] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    [Descripcion] nvarchar(800) NOT NULL,
    [FechaInicio] datetime NOT NULL DEFAULT ((getdate())),
    [FechaFin] datetime NULL,
    [IdEstado] int NULL,
    [IdUsuario] int NULL,
    CONSTRAINT [PK__Proyecto__F4888673C879F5DA] PRIMARY KEY ([IdProyecto]),
    CONSTRAINT [Fk_Proyecto_Refe_Estado] FOREIGN KEY ([IdEstado]) REFERENCES [Estado] ([IdEstado]),
    CONSTRAINT [Fk_Proyecto_Refe_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario])
);
GO

CREATE TABLE [UsuariosRoles] (
    [IdUsuarioRol] int NOT NULL IDENTITY,
    [IdUsuario] int NULL,
    [IdRol] int NULL,
    CONSTRAINT [PK__Usuarios__6806BF4AB664B3D5] PRIMARY KEY ([IdUsuarioRol]),
    CONSTRAINT [Fk_UsuarioRol_Refe_Rol] FOREIGN KEY ([IdRol]) REFERENCES [Roles] ([IdRol]),
    CONSTRAINT [Fk_UsuarioRol_Refe_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario])
);
GO

CREATE TABLE [ProyectoUsuarios] (
    [IdProyectoUsuarios] int NOT NULL IDENTITY,
    [IdUsuario] int NULL,
    [IdProyecto] int NULL,
    [Estado] bit NULL DEFAULT (((1))),
    CONSTRAINT [PK__Proyecto__EFCF35317220DAE9] PRIMARY KEY ([IdProyectoUsuarios]),
    CONSTRAINT [Fk_ProyectoUsuarios_Refe_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario]),
    CONSTRAINT [Fk_royectoUsuarios_Refe_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [Proyectos] ([IdProyecto])
);
GO

CREATE TABLE [Tareas] (
    [IdTarea] int NOT NULL IDENTITY,
    [Nombre] nvarchar(200) NOT NULL,
    [Descripcion] nvarchar(max) NOT NULL,
    [FechaInicio] datetime NULL DEFAULT ((getdate())),
    [FechaFin] datetime NULL,
    [IdEstado] int NULL,
    [IdProyecto] int NULL,
    [IdTareaPadre] int NULL,
    [IdUsuario] int NULL,
    [IdArchivo] int NULL,
    [IdPrioridad] int NULL,
    CONSTRAINT [PK__Tareas__EADE909808C2F6F3] PRIMARY KEY ([IdTarea]),
    CONSTRAINT [Fk_Tareas_Refe_Archivos] FOREIGN KEY ([IdArchivo]) REFERENCES [Archivos] ([IdArchivo]),
    CONSTRAINT [Fk_Tareas_Refe_Estado] FOREIGN KEY ([IdEstado]) REFERENCES [Estado] ([IdEstado]),
    CONSTRAINT [Fk_Tareas_Refe_Prioridad] FOREIGN KEY ([IdPrioridad]) REFERENCES [Prioridad] ([IdPrioridad]),
    CONSTRAINT [Fk_Tareas_Refe_Proyecto] FOREIGN KEY ([IdProyecto]) REFERENCES [Proyectos] ([IdProyecto]),
    CONSTRAINT [Fk_Tareas_Refe_Tareas] FOREIGN KEY ([IdTareaPadre]) REFERENCES [Tareas] ([IdTarea]),
    CONSTRAINT [Fk_Tareas_Refe_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario])
);
GO

CREATE TABLE [Comentarios] (
    [IdComentario] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NOT NULL,
    [Estado] bit NULL DEFAULT (((1))),
    [IdUsuario] int NULL,
    [IdTarea] int NULL,
    [IdArchivo] int NULL,
    CONSTRAINT [PK__Comentar__DDBEFBF98A3FB52C] PRIMARY KEY ([IdComentario]),
    CONSTRAINT [Fk_Comentario_Refe_Archivo] FOREIGN KEY ([IdArchivo]) REFERENCES [Archivos] ([IdArchivo]),
    CONSTRAINT [Fk_Comentario_Refe_Tarea] FOREIGN KEY ([IdTarea]) REFERENCES [Tareas] ([IdTarea]),
    CONSTRAINT [Fk_Comentario_Refe_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario])
);
GO

CREATE TABLE [HistorialTareas] (
    [IdHistorialTarea] int NOT NULL IDENTITY,
    [IdTarea] int NULL,
    [IdEstadoNuevo] int NULL,
    [FechaCambio] datetime NULL,
    [IdUsuario] int NULL,
    CONSTRAINT [PK__Historia__7A8790175C09C187] PRIMARY KEY ([IdHistorialTarea]),
    CONSTRAINT [Fk_HistorialTareas_Refe_Tarea] FOREIGN KEY ([IdTarea]) REFERENCES [Tareas] ([IdTarea]),
    CONSTRAINT [Fk_HistorialTareas_Refe_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario])
);
GO

CREATE INDEX [IX_Comentarios_IdArchivo] ON [Comentarios] ([IdArchivo]);
GO

CREATE INDEX [IX_Comentarios_IdTarea] ON [Comentarios] ([IdTarea]);
GO

CREATE INDEX [IX_Comentarios_IdUsuario] ON [Comentarios] ([IdUsuario]);
GO

CREATE UNIQUE INDEX [UQ__Estado__75E3EFCF28CFE0B4] ON [Estado] ([Nombre]) WHERE [Nombre] IS NOT NULL;
GO

CREATE INDEX [IX_HistorialTareas_IdTarea] ON [HistorialTareas] ([IdTarea]);
GO

CREATE INDEX [IX_HistorialTareas_IdUsuario] ON [HistorialTareas] ([IdUsuario]);
GO

CREATE UNIQUE INDEX [UQ__Priorida__75E3EFCF523FD037] ON [Prioridad] ([Nombre]) WHERE [Nombre] IS NOT NULL;
GO

CREATE INDEX [IX_Proyectos_IdEstado] ON [Proyectos] ([IdEstado]);
GO

CREATE INDEX [IX_Proyectos_IdUsuario] ON [Proyectos] ([IdUsuario]);
GO

CREATE INDEX [IX_ProyectoUsuarios_IdProyecto] ON [ProyectoUsuarios] ([IdProyecto]);
GO

CREATE INDEX [IX_ProyectoUsuarios_IdUsuario] ON [ProyectoUsuarios] ([IdUsuario]);
GO

CREATE UNIQUE INDEX [UQ__Roles__75E3EFCF8BD76D68] ON [Roles] ([Nombre]);
GO

CREATE INDEX [IX_Tareas_IdArchivo] ON [Tareas] ([IdArchivo]);
GO

CREATE INDEX [IX_Tareas_IdEstado] ON [Tareas] ([IdEstado]);
GO

CREATE INDEX [IX_Tareas_IdPrioridad] ON [Tareas] ([IdPrioridad]);
GO

CREATE INDEX [IX_Tareas_IdProyecto] ON [Tareas] ([IdProyecto]);
GO

CREATE INDEX [IX_Tareas_IdTareaPadre] ON [Tareas] ([IdTareaPadre]);
GO

CREATE INDEX [IX_Tareas_IdUsuario] ON [Tareas] ([IdUsuario]);
GO

CREATE INDEX [IX_UsuariosRoles_IdRol] ON [UsuariosRoles] ([IdRol]);
GO

CREATE INDEX [IX_UsuariosRoles_IdUsuario] ON [UsuariosRoles] ([IdUsuario]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230901043323_Initial', N'6.0.19');
GO

COMMIT;
GO

