CREATE DATABASE INFO104_GRUPO3_PROYECTO
GO

USE INFO104_GRUPO3_PROYECTO
GO

CREATE TABLE usuarios
(
    usuarioID int identity (1,1) PRIMARY KEY,
    nombre nvarchar(100) NOT NULL,
    correoElectronico nvarchar(100) NOT NULL,
    telefono nvarchar(50) NOT NULL
)
GO
CREATE TABLE tecnicos
(
    tecnicoID int identity (100,100) PRIMARY KEY,
    nombre varchar(100) NOT NULL,
    especialidad varchar(100) NOT NULL
)
GO
CREATE TABLE equipos
(
    equipoID int identity (5,5) PRIMARY KEY,
    tipoEquipo nvarchar(100) NOT NULL,
    modelo nvarchar(100) NOT NULL,
    usuarioID int,
    CONSTRAINT fk_usuarioID FOREIGN KEY (usuarioID) REFERENCES usuarios(usuarioID)
)
GO
CREATE TABLE reparaciones
(
    reparacionID int identity (10,10) PRIMARY KEY,
    equipoID int,
    fechaSolicitud datetime NOT NULL CONSTRAINT df_fechaSolicitud DEFAULT GETDATE(),
    estado nvarchar(100) NOT NULL
    CONSTRAINT fk_equipoID FOREIGN KEY (equipoID) REFERENCES equipos(equipoID)
)
GO
CREATE TABLE detallesReparacion
(
    detalleID int identity (10,10) PRIMARY KEY,
    reparacionID int,
    descripcion varchar(100) NOT NULL,
    fechaInicio datetime NOT NULL CONSTRAINT df_fechaInicio DEFAULT GETDATE(),
    fechaFin datetime,
    CONSTRAINT fk_detalles_reparacionID FOREIGN KEY (reparacionID) REFERENCES reparaciones(reparacionID)
)
GO
CREATE TABLE asignaciones
(
    asignacionID int identity (1,1) PRIMARY KEY,
    reparacionID int,
    tecnicoID int,
    FechaAsignacion datetime NOT NULL CONSTRAINT df_fechaAsignacion DEFAULT GETDATE(),
    CONSTRAINT fk_asignaciones_reparacionID FOREIGN KEY (reparacionID) REFERENCES reparaciones(reparacionID),
    CONSTRAINT fk_tecnicoID FOREIGN KEY (tecnicoID) REFERENCES tecnicos(tecnicoID)
)
GO

CREATE TABLE cuentas 
(
	id int identity (1,1),
	correo varchar(50) NOT NULL,
	clave varchar(50) NOT NULL,
	nombre varchar(50) NOT NULL,
	
	CONSTRAINT pk_idCuenta PRIMARY KEY(id),
	CONSTRAINT uq_correo UNIQUE (correo) 
)
GO

CREATE TABLE roles
(
	id int identity (1000,1),
	descripcion varchar(50) NOT NULL,
	
	CONSTRAINT pk_rol PRIMARY KEY(id),
	
)
GO

CREATE TABLE cuentaRol
(
	id int identity (100,5),
	idCuenta int ,
	idRol int ,
	fecha datetime CONSTRAINT df_fecha DEFAULT GETDATE(),
	CONSTRAINT pk_idUsuarioRol PRIMARY KEY(id),
	CONSTRAINT fk_IdCuenta FOREIGN KEY (idCuenta) REFERENCES cuentas(id),
	CONSTRAINT fk_IdRol FOREIGN KEY (idRol) REFERENCES roles(id)
)
GO

/*               PROCEDIMIENTOS TABLA CUENTAS Y ROLES            */

GO
CREATE PROCEDURE agregarCuenta
@correo nvarchar(50),
@clave nvarchar(50),
@nombre nvarchar(50)
    AS
        BEGIN
            INSERT INTO cuentas (correo,clave,nombre) VALUES (@correo,@clave,@nombre)
        END
GO

CREATE PROCEDURE borrarCuenta
@id int
    AS 
        BEGIN
            DELETE cuentas WHERE id = @id
        END
GO

CREATE PROCEDURE modificarCuenta
@id int,
@correo nvarchar(50),
@clave nvarchar(50),
@nombre nvarchar(50)
    AS
        BEGIN
            UPDATE cuentas SET correo = @correo, clave = @clave, nombre = @nombre WHERE id = @id
        END
GO

CREATE PROCEDURE validarCuenta
@correo varchar(50),
@clave varchar(50)
as
	begin
		select correo, clave, nombre from cuentas where correo = @correo AND clave = @clave
	end

GO
CREATE PROCEDURE agregarRol
@descripcion nvarchar(50)
    AS
        BEGIN
            INSERT INTO roles (descripcion) VALUES (@descripcion)
        END
GO

CREATE PROCEDURE borrarRol
@id int
    AS 
        BEGIN
            DELETE roles WHERE id = @id
        END
GO

CREATE PROCEDURE modificarRol
@id int,
@descripcion nvarchar(50)
    AS
        BEGIN
            UPDATE roles SET descripcion = @descripcion WHERE id = @id
        END
GO

GO
CREATE PROCEDURE agregarCuentaRol
@idCuenta int,
@idRol int,
@fecha datetime
    AS
        BEGIN
            INSERT INTO cuentaRol (idCuenta,idRol,fecha) VALUES (@idCuenta,@idRol,@fecha)
        END
GO

CREATE PROCEDURE borrarCuentaRol
@id int
    AS 
        BEGIN
            DELETE cuentaRol WHERE id = @id
        END
GO

CREATE PROCEDURE modificarCuentaRol
@id int,
@idCuenta int,
@idRol int,
@fecha datetime
    AS
        BEGIN
            UPDATE cuentaRol SET idCuenta = @idCuenta,idRol = @idRol, fecha = @fecha WHERE id = @id
        END
GO

/*               PROCEDIMIENTOS TABLA USUARIOS            */

GO
CREATE PROCEDURE agregarusuario
@nombre nvarchar(100),
@correoElectronico nvarchar(100),
@telefono nvarchar(50)
    AS
        BEGIN
            INSERT INTO usuarios (nombre,correoElectronico,telefono) VALUES (@nombre,@correoElectronico,@telefono)
        END
GO
CREATE PROCEDURE borrarusuario
@usuarioID int
    AS 
        BEGIN
            DELETE usuarios WHERE usuarioID = @usuarioID
        END
GO
CREATE PROCEDURE consultarusuario
@usuarioID int
    AS 
        BEGIN
            SELECT * FROM usuarios WHERE usuarioID = @usuarioID
        END
GO
CREATE PROCEDURE modificarusuario
@usuarioID int,
@nombre nvarchar(100),
@correoElectronico nvarchar(100),
@telefono nvarchar(50)
    AS
        BEGIN
            UPDATE usuarios SET nombre = @nombre, correoElectronico = @correoElectronico, telefono = @telefono WHERE usuarioID = @usuarioID
        END
GO



GO
CREATE PROCEDURE agregartecnico
@nombre nvarchar(100),
@especialidad nvarchar(100)
    AS
        BEGIN
            INSERT INTO tecnicos (nombre,especialidad) VALUES (@nombre,@especialidad)
        END
GO

CREATE PROCEDURE borrartecnico
@tecnicoID int
    AS 
        BEGIN
            DELETE tecnicos WHERE tecnicoID = @tecnicoID
        END
GO

CREATE PROCEDURE modificartecnico
@tecnicoID int,
@nombre nvarchar(100),
@especialidad nvarchar(100)
    AS
        BEGIN
            UPDATE tecnicos SET nombre = @nombre, especialidad = @especialidad WHERE tecnicoID = @tecnicoID
        END
GO


/*               PROCEDIMIENTOS TABLA EQUIPOS            */
CREATE PROCEDURE agregarequipo
@tipoEquipo nvarchar(100),
@modelo nvarchar(100),
@usuarioID int
    AS
        BEGIN
            INSERT INTO equipos (tipoEquipo,modelo, usuarioID) VALUES (@tipoEquipo,@modelo,@usuarioID)
        END
GO

CREATE PROCEDURE borrarequipo
@equipoID int
    AS 
        BEGIN
            DELETE equipos WHERE equipoID = @equipoID
        END
GO

CREATE PROCEDURE consultarequipo
@equipoID int
    AS 
        BEGIN
            SELECT * FROM equipos WHERE equipoID = @equipoID
        END
GO

CREATE PROCEDURE modificarequipo
@equipoID int,
@tipoEquipo nvarchar(100),
@modelo nvarchar(100),
@usuarioID int
    AS
        BEGIN
            UPDATE equipos SET tipoEquipo = @tipoEquipo, modelo = @modelo, usuarioID = @usuarioID WHERE equipoID = @equipoID
        END
GO


/*               PROCEDIMIENTOS TABLA REPAROS            */
CREATE PROCEDURE agregarReparo
@equipoID int,
@fechaSolicitud datetime,
@estado nvarchar(100)
	AS
		BEGIN
			INSERT INTO reparaciones (equipoID,fechaSolicitud,estado) VALUES (@equipoID,@fechaSolicitud,@estado)
		END
GO

CREATE PROCEDURE borrarReparo
@reparacionID int
    AS 
        BEGIN
            DELETE reparaciones WHERE reparacionID = @reparacionID
        END
GO

CREATE PROCEDURE modificarReparo
@reparacionID int,
@equipoID int,
@fechaSolicitud datetime,
@estado nvarchar(100)
	AS
		BEGIN
			UPDATE reparaciones SET equipoID = @equipoID, fechaSolicitud = @fechaSolicitud, estado = @estado WHERE reparacionID = @reparacionID
		END
GO

/*               PROCEDIMIENTOS TABLA REPAROS DETALLES            */
CREATE PROCEDURE agregarDetalles
@reparacionID int,
@descripcion varchar(100),
@fechaInicio datetime,
@fechaFin datetime
	AS
		BEGIN
			INSERT INTO detallesReparacion (reparacionID,descripcion,fechaInicio,fechaFin) VALUES (@reparacionID,@descripcion,@fechaInicio,@fechaFin)
		END
GO

CREATE PROCEDURE borrarDetalles
@detalleID int
    AS 
        BEGIN
            DELETE detallesReparacion WHERE detalleID = @detalleID
        END
GO

CREATE PROCEDURE modificarDetalles
@detalleID int,
@reparacionID int,
@descripcion varchar(100),
@fechaInicio datetime,
@fechaFin datetime
	AS
		BEGIN
			UPDATE detallesReparacion SET reparacionID = @reparacionID, descripcion = @descripcion, fechaInicio = @fechaInicio, fechaFin = @fechaFin WHERE detalleID = @detalleID
		END
GO


/*               PROCEDIMIENTOS TABLA REPAROS ASIGNACIONES            */
CREATE PROCEDURE agregarAsignaciones
@reparacionID int,
@tecnicoID int,
@FechaAsignacion datetime
	AS
		BEGIN
			INSERT INTO asignaciones (reparacionID,tecnicoID,FechaAsignacion) VALUES (@reparacionID,@tecnicoID,@FechaAsignacion)
		END
GO

CREATE PROCEDURE borrarAsignacion
@asignacionID int
    AS 
        BEGIN
            DELETE asignaciones WHERE asignacionID = @asignacionID
        END
GO

CREATE PROCEDURE modificarAsignacion
@asignacionID int,
@reparacionID int,
@tecnicoID int,
@FechaAsignacion datetime
	AS
		BEGIN
			UPDATE asignaciones SET reparacionID = @reparacionID, tecnicoID = @tecnicoID, FechaAsignacion = @FechaAsignacion WHERE asignacionID = @asignacionID
		END
GO

/*               PROCEDIMIENTOS INNER JOIN          */
CREATE TABLE filtros
(
    filtro nvarchar(50)
)
GO

INSERT INTO filtros (filtro) VALUES ('filtro1'),('filtro2'),('filtro3'),('filtro4'),('filtro5')
GO
CREATE PROCEDURE filtro1
@codigo int
AS
	BEGIN
		SELECT U.nombre, E.tipoEquipo FROM usuarios U
		INNER JOIN equipos E ON E.usuarioID = U.usuarioID
		WHERE  U.usuarioID = @codigo OR E.equipoID = @codigo
	END
GO

GO
CREATE PROCEDURE filtro2
@codigo int
AS
	BEGIN
		SELECT U.nombre, E.tipoEquipo, R.estado FROM usuarios U
		INNER JOIN equipos E ON E.usuarioID = U.usuarioID
		INNER JOIN reparaciones R ON R.equipoID = E.equipoID
		WHERE U.usuarioID = @codigo OR E.equipoID = @codigo OR  R.reparacionID = @codigo 
	END
GO

GO
CREATE PROCEDURE filtro3
@codigo int
AS
	BEGIN
		SELECT U.nombre, E.tipoEquipo, R.estado, A.FechaAsignacion FROM usuarios U
		INNER JOIN equipos E ON E.usuarioID = U.usuarioID
		INNER JOIN reparaciones R ON R.equipoID = E.equipoID
		INNER JOIN asignaciones A ON A.reparacionID = R.reparacionID
		WHERE U.usuarioID = @codigo OR E.equipoID = @codigo OR  R.reparacionID = @codigo OR A.asignacionID = @codigo
	END
GO

GO
CREATE PROCEDURE filtro4
@codigo int
AS
	BEGIN
		SELECT U.nombre, E.tipoEquipo, R.estado, A.FechaAsignacion,T.nombre FROM usuarios U
		INNER JOIN equipos E ON E.usuarioID = U.usuarioID
		INNER JOIN reparaciones R ON R.equipoID = E.equipoID
		INNER JOIN asignaciones A ON A.reparacionID = R.reparacionID
		INNER JOIN tecnicos T ON T.tecnicoID = A.tecnicoID
		WHERE U.usuarioID = @codigo OR T.tecnicoID = @codigo OR E.equipoID = @codigo OR  R.reparacionID = @codigo OR A.asignacionID = @codigo 
	END
GO

GO
CREATE PROCEDURE filtro5
@codigo int
AS
	BEGIN
		SELECT U.nombre, E.tipoEquipo, R.estado, A.FechaAsignacion,T.nombre, DR.descripcion FROM usuarios U
		INNER JOIN equipos E ON E.usuarioID = U.usuarioID
		INNER JOIN reparaciones R ON R.equipoID = E.equipoID
		INNER JOIN asignaciones A ON A.reparacionID = R.reparacionID
		INNER JOIN tecnicos T ON T.tecnicoID = A.tecnicoID
		INNER JOIN detallesReparacion DR on R.reparacionID = DR.reparacionID 
		WHERE U.usuarioID = @codigo OR T.tecnicoID = @codigo OR E.equipoID = @codigo OR  R.reparacionID = @codigo OR A.asignacionID = @codigo OR  DR.detalleID = @codigo
	END
GO