use Licorera
go



create table Marcas (
	idMarca int identity(1,1) primary key not null,
	vNombre varchar(100) not null,
);

INSERT INTO Marcas (vNombre) VALUES ('Jose Cuervo');

select * from Marcas;

create table Tipos (
	idTipo int identity(1,1) primary key not null,
	vNombre varchar(100) not null,
);

create table Proveedores (
	idProveedor int identity(1,1) primary key not null,
	vNombre varchar(100) not null,
	iCedula int not null,
	CONSTRAINT A_cedula UNIQUE(iCedula),
	/*constraint chk_albergue check (iCedula like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),--valida que esten los nueve digitos pertecientes a la cedula*/
	iTelefono int not null,
	vCorreo varchar(50) not null,
	vDireccion varchar(200) not null
);

create table Licores (
	idLicor int identity(1,1) primary key,--es auto incrementable
	idMarca int not null,
	idTipo int not null,
	idProveedor int not null,
	vDescripción varchar(300),
	iUnidades int not null,
	iPrecio int not null,
	Foto_Licor varchar(max), --foto principal,
	Foto_Detalles varchar(max); --foto Detalles
	foreign key (idMarca) references Marcas(idMarca),
	foreign key (idTipo) references Tipos(idTipo),
	foreign key (idProveedor) references Proveedores(idProveedor)
);

create table medios_Pago (
	idMedio int identity(1,1) primary key,--es auto incrementable
	vNombre varchar(50) not null,
);

create table estados_Pedido (
	idEstado int identity(1,1) primary key,--es auto incrementable
	vNombre varchar(50) not null,
);

create table Pedidos (
	idPedido int identity(1,1) primary key,--es auto incrementable
	vNombre_Cliente varchar(50) not null,
	vTelefono varchar(20) not null,
	vEmail varchar(50),
	vDireccion_Envio varchar(800) not null,
	dFecha date not null,
	idEstado_Pedido int,
	idMedio_Pago int,
	bEstado_Pago bit not null,
	identificacion varchar(20),
	foreign key (idEstado_Pedido) references estados_Pedido(idEstado),
	foreign key (idMedio_Pago) references medios_Pago(idMedio)
);

create table Detalles_Pedido (
	idDetalle int identity(1,1) primary key,--es auto incrementable
	idPedido int not null,
	idLicor int not null,
	iPrecio int not null,
	iCantidad int not null,
	foreign key (idLicor) references Licores(idLicor),
	foreign key (idPedido) references Pedidos(idPedido),
);


---------------------Para usuarios-----------------------------

USE [Licorera]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 12/6/2020 11:55:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](100) NULL,
	[cedula] [nvarchar](max) NULL,
	[nombre] [nvarchar](max) NULL,
	[apellido1] [nvarchar](max) NULL,
	[apellido2] [nvarchar](max) NULL,
	[Correo] [nvarchar](max) NULL,
	[direccion] [nvarchar](max) NULL,
	[telefono] [nvarchar](max) NULL,
	[telefono2] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



/****** Object:  Table [dbo].[Roles]    Script Date: 12/6/2020 11:55:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsersInRoles](
	[User_UserId] [int] NOT NULL,
	[Role_RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RoleUsers] PRIMARY KEY CLUSTERED 
(
	[Role_RoleId] ASC,
	[User_UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_Role_RoleId] FOREIGN KEY([Role_RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_Role_RoleId]
GO

ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Users_User_UserId] FOREIGN KEY([User_UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Users_User_UserId]
GO



------------------------------------PROC---------------------------

USE [Licorera]
GO
/****** Object:  StoredProcedure [dbo].[sp_getRolesForUser]    Script Date: 12/6/2020 11:56:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jordan Larios
-- =============================================
CREATE PROCEDURE [dbo].[sp_getRolesForUser]
	@userName varchar(20)
	
AS
BEGIN
	SET NOCOUNT ON;


	
	select r.NombreRol from UsersInRoles ur
	inner join Users u on ur.User_UserId=u.UserId
	inner join Roles r on ur.Role_RoleId = r.RoleId
	where u.UserName=@userName

END

-----------------------------------------------------------------------

USE [Licorera]
GO
/****** Object:  StoredProcedure [dbo].[sp_getUsuariosRole]    Script Date: 12/6/2020 11:57:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jordan Larios
-- =============================================
Create PROCEDURE [dbo].[sp_getUsuariosRole]
	
	@roleName varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	

	
	select u.UserName from UsersInRoles ur
	inner join Users u on ur.User_UserId=u.UserId
	inner join Roles r on ur.Role_RoleId = r.RoleId
	where r.NombreRol=@roleName 

	 
	 
END

--------------------------------------------------------------------------------


USE [Licorera]
GO
/****** Object:  StoredProcedure [dbo].[sp_isUserInRole]    Script Date: 12/6/2020 11:57:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jordan Larios
-- =============================================
CREATE PROCEDURE [dbo].[sp_isUserInRole]
	@userName varchar(20),
	@roleName varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	declare @resultado bit =0;

	if exists(
	select * from UsersInRoles ur
	inner join Users u on ur.User_UserId = u.UserId
	inner join Roles r on ur.Role_RoleId = r.RoleId
	where r.NombreRol = @roleName and u.UserName = @userName)
	set @resultado = 1


	 
	 select @resultado
END

USE [Licorera]
GO
-- =============================================
-- Author:		Jordan Larios
-- =============================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreaUsuarioRol]
@IdUsuario as int,
@IdRol as int
AS
BEGIN

INSERT INTO [dbo].[UsersInRoles]
           (
           [User_UserId]
		   ,[Role_RoleId])
     values 
           (@IdUsuario
           ,@IdRol)

END






USE [Licorera]
GO
/****** Object:  StoredProcedure [dbo].[EliminaUsuarioRol]    Script Date: 12/11/2020 8:23:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jordan Larios
-- =============================================
create PROCEDURE [dbo].[EliminaUsuarioRol]
@IdUsuario as int
AS
BEGIN

	Delete UsersInRoles where User_UserId=@IdUsuario
END

