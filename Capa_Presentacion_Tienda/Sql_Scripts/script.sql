USE [GITANEANDO]
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marcas](
	[IdMarcas] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[Activo] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMarcas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](200) NULL,
	[IdCategoria] [int] NULL,
	[Precio] [decimal](10, 2) NULL,
	[Stock] [int] NULL,
	[UrlImagen] [varchar](300) NULL,
	[NombreImagen] [varchar](50) NULL,
	[Activo] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
	[IdUsuario] [int] NULL,
	[IdProvincia] [int] NULL,
	[IdCiudad] [int] NULL,
	[IdBarrio] [int] NULL,
	[IdLocal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NULL,
	[IdProducto] [int] NULL,
	[Cantidad] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ObtenerCarritoCliente]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[fn_ObtenerCarritoCliente](
@IdCliente int
)
returns table
as
return(
select p.IdProducto,m.Descripcion [desMarca], p.Nombre, p.Precio, c.Cantidad, p.UrlImagen, p.NombreImagen

from Carrito c
inner join Producto p on p.IdProducto = c.IdProducto 
inner join Marcas m on m.IdMarcas = p.IdMarca
where c.IdCliente = @IdCliente	

)
GO
/****** Object:  Table [dbo].[Favoritos]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favoritos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdProducto] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_ObtenerFavoritoUsuario]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[fn_ObtenerFavoritoUsuario](  
@IdUsuario int  
)  
returns table  
as  
return(  
select p.IdProducto, p.Descripcion, p.Nombre, p.Precio, p.UrlImagen, p.NombreImagen , p.IdProvincia, p.IdCiudad, p.IdBarrio, p.IdLocal
  
from Favoritos f  
inner join Producto p on p.IdProducto = f.IdProducto   
where f.IdUsuario = @IdUsuario   
  
)
GO
/****** Object:  Table [dbo].[Barrios]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Barrios](
	[IdBarrio] [int] IDENTITY(1,1) NOT NULL,
	[IdCiudad] [int] NOT NULL,
	[IdProvincia] [int] NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBarrio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[Activo] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudades]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudades](
	[IdCiudad] [int] IDENTITY(1,1) NOT NULL,
	[IdProvincia] [int] NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCiudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Pass] [varchar](200) NULL,
	[Restablecer] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleVenta]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleVenta](
	[IdDetalleVenta] [int] IDENTITY(1,1) NOT NULL,
	[IdVenta] [int] NULL,
	[IdProducto] [int] NULL,
	[Cantidad] [int] NULL,
	[Total] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalleVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locales]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locales](
	[Descripcion] [varchar](50) NULL,
	[IdLocal] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLocal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincias]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincias](
	[IdProvincia] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Pass] [varchar](200) NULL,
	[Restablecer] [bit] NULL,
	[Activo] [bit] NULL,
	[FechaRegistro] [datetime] NULL,
	[Administrador] [bit] NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Celular] [varchar](50) NULL,
	[IdProvincia] [int] NULL,
	[IdCiudad] [int] NULL,
	[IdBarrio] [int] NULL,
	[Tienda] [varchar](50) NULL,
	[IdLocal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[IdVenta] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NULL,
	[TotalProducto] [int] NULL,
	[MontoTotal] [decimal](10, 2) NULL,
	[Contacto] [varchar](50) NULL,
	[IdDistrito] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Direccion] [varchar](100) NULL,
	[IdTransaccion] [varchar](50) NULL,
	[FechaVenta] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categoria] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Categoria] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT ((0)) FOR [Restablecer]
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Marcas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Marcas] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Producto] ADD  DEFAULT ((0)) FOR [Precio]
GO
ALTER TABLE [dbo].[Producto] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Producto] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF__Usuario__Restabl__4D94879B]  DEFAULT ((1)) FOR [Restablecer]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((0)) FOR [Administrador]
GO
ALTER TABLE [dbo].[Venta] ADD  DEFAULT (getdate()) FOR [FechaVenta]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD FOREIGN KEY([IdVenta])
REFERENCES [dbo].[Venta] ([IdVenta])
GO
ALTER TABLE [dbo].[Favoritos]  WITH CHECK ADD  CONSTRAINT [IdUsuario_Favoritos] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Favoritos] CHECK CONSTRAINT [IdUsuario_Favoritos]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [Idlocal_] FOREIGN KEY([IdLocal])
REFERENCES [dbo].[Locales] ([IdLocal])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [Idlocal_]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [IdUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [IdUsuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [IdBarrio] FOREIGN KEY([IdBarrio])
REFERENCES [dbo].[Barrios] ([IdBarrio])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [IdBarrio]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [IdCiudad] FOREIGN KEY([IdCiudad])
REFERENCES [dbo].[Ciudades] ([IdCiudad])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [IdCiudad]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [Idlocal] FOREIGN KEY([IdLocal])
REFERENCES [dbo].[Locales] ([IdLocal])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [Idlocal]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [IdProvincia] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincias] ([IdProvincia])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [IdProvincia]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
/****** Object:  StoredProcedure [dbo].[Nuevo_EditarProducto]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

      Create proc [dbo].[Nuevo_EditarProducto](  
@IdProducto int ,  
@Nombre varchar(100),  
@Descripcion varchar(100),  
@IdMarca varchar(100),  
@IdCategoria varchar(100),  
@IdUsuario varchar(100),  
@Precio decimal (10, 2),  
@Stock int,   
@Activo bit,  
@Mensaje varchar(500) output,  
@Resultado int output  
)  
as  
begin  
  SET @Resultado = 0  
  If exists ( select * from Producto where IdProducto = @IdProducto)  
  begin  
  update Producto set   
  Nombre = @Nombre,  
  Descripcion = @Descripcion,  
  IdMarca = @IdMarca,  
  IdCategoria = @IdCategoria,  
  Precio = @Precio,  
  Stock = @Stock,  
  Activo = @Activo  
  where IdProducto = @IdProducto  
  set @Resultado = 1  
  end  
  else  
  set @Mensaje = 'El producto ya existe'  
  end
GO
/****** Object:  StoredProcedure [dbo].[nuevo_RegistrarProducto]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[nuevo_RegistrarProducto](          
@Nombre varchar(100),          
@Descripcion varchar(100),           
@IdCategoria varchar(100),          
@IdUsuario varchar(100),          
@Precio decimal (10, 2),          
@Stock int,          
@Activo bit,          
@IdProvincia int,          
@IdCiudad int,          
@IdBarrio int,    
@IdLocal int,  
@Mensaje varchar(500) output,          
@Resultado int output          
)          
as          
begin          
  SET @Resultado = 0          
  If not exists ( select * from Producto where Nombre = @Nombre and Descripcion = @Descripcion and IdCategoria = @IdCategoria )          
  begin          
   insert into Producto(Nombre, Descripcion, IdCategoria, Precio, Stock, Activo,IdUsuario, IdProvincia, IdCiudad, IdBarrio,IdLocal)          
   values (@Nombre, @Descripcion, @IdCategoria, @Precio, @Stock, @Activo,@IdUsuario, @IdProvincia, @IdCiudad, @IdBarrio,@IdLocal)          
          
   set @Resultado = SCOPE_IDENTITY()          
   end           
   else           
   set @Mensaje = 'El producto ya existe'          
   end
GO
/****** Object:  StoredProcedure [dbo].[sp_AgregarACarrito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[sp_AgregarACarrito] (
    @IdCliente int,
    @IdProducto int,
    @Cantidad int
)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Carrito (IdCliente, IdProducto, Cantidad)
        VALUES (@IdCliente, @IdProducto, @Cantidad);
    END TRY
    BEGIN CATCH
        -- Manejar el error aquí si es necesario
    END CATCH
END; 
GO
/****** Object:  StoredProcedure [dbo].[sp_AgregarFavorito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE proc [dbo].[sp_AgregarFavorito](  
  @IdUsuario int,  
  @IdProducto int,   
  @Mensaje varchar (500) output,  
  @Resultado bit output  
  )  
  as  
  begin   
     set @Resultado = 1  
     set @Mensaje = ''  
  
     declare @existeFavorito bit = iif (exists(select * from Favoritos where IdUsuario = @IdUsuario and IdProducto = @IdProducto),1 ,0)  
 
       if( @existeFavorito = 1)  
	   begin
          set @Resultado = 0  
          set @Mensaje = 'El producto ya existe en favoritos'  
		  end	
       else    
	   begin
	      set @Resultado = 1
          insert into  Favoritos (IdUsuario,IdProducto) values (@IdUsuario,@IdProducto)  
		  end
      end   
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarCategoria]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_EditarCategoria](
@IdCategoria int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado bit output
)
as 
begin
     SET @Resultado = 0
	 If not exists ( select * from Categoria where Descripcion = @Descripcion and IdCategoria != @IdCategoria)
	 begin

	 update top (1) Categoria set 
	 Descripcion = @Descripcion,
	 Activo = @Activo
	 where IdCategoria = @IdCategoria 
	 set @Resultado = 1
	 end 
	 else
	 set @Mensaje = 'La categoria ya existe'
	 end 
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarMarca]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_EditarMarca](
@IdMarca int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado bit output
)
as 
begin
     SET @Resultado = 0
	 If not exists ( select * from Marcas where Descripcion = @Descripcion and IdMarcas != @IdMarca)
	 begin

	 update top (1) Marcas set 
	 Descripcion = @Descripcion,
	 Activo = @Activo
	 where IdMarcas = @IdMarca 
	 set @Resultado = 1
	 end 
	 else
	 set @Mensaje = 'La marca ya existe'
	 end 
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarProducto]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   CREATE proc [dbo].[sp_EditarProducto](  
@IdProducto int ,  
@Nombre varchar(100),  
@Descripcion varchar(100),  
@IdCategoria varchar(100),  
@Precio decimal (10, 2),  
@Stock int,   
@Activo bit,  
@Mensaje varchar(500) output,  
@Resultado int output  
)  
as  
begin  
  SET @Resultado = 0  
  If exists ( select * from Producto where IdProducto = @IdProducto)  
  begin  
  update Producto set   
  Nombre = @Nombre,  
  Descripcion = @Descripcion,  
  IdCategoria = @IdCategoria,  
  Precio = @Precio,  
  Stock = @Stock,  
  Activo = @Activo  
  where IdProducto = @IdProducto
  set @Resultado = 1  
  end  
  else  
  set @Mensaje = 'El producto ya existe'  
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_EditarUsuario]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_EditarUsuario](
   @IdUsuario int,
   @Nombre varchar (50),
@Apellido varchar (50),
@Email varchar(50),
@Activo bit ,
@Mensaje varchar (500) output,
@Resultado bit output
        )
		as
		begin
		set @Resultado = 0
		if not exists ( Select * from Usuario where Email = @Email and IdUsuario != @IdUsuario)
		begin

		update top (1) Usuario set
        Nombre = @Nombre,
		Apellido = @Apellido,
		Email = @Email,	
		Activo = @Activo
		where IdUsuario = @IdUsuario

		set @Resultado = 1
	end 
	else 
    set @Mensaje = 'El correo de usuario ya existe'
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarCarrito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_EliminarCarrito](
@IdCliente int,
@IdProducto int,
@Resultado bit output
)
as
begin 
      set @Resultado = 1
	  declare @cantidadProducto int =( select cantidad from Carrito where IdCliente = @IdCliente and IdProducto = @IdProducto)

	  begin try 
	      begin transaction operacion

		  update Producto set Stock = Stock + @cantidadProducto where IdProducto = @IdProducto
		  delete top (1) from Carrito where IdCliente = @IdCliente and IdProducto = @IdProducto

		  commit transaction operacion 

	 end try
	 begin catch
	     set @Resultado = 0
		 rollback transaction operacion
	end catch
end
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarCategoria]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	 create proc [dbo].[sp_EliminarCategoria](
	 	 
@IdCategoria int,
@Mensaje varchar(500) output,
@Resultado bit output
	 )
	 as 
begin
     SET @Resultado = 0
	 if not exists (select * from Producto p inner join Categoria c 
	 on c.IdCategoria = p.IdCategoria 
	 where p.IdCategoria = @IdCategoria)
	 begin 
	    delete top (1) from Categoria where IdCategoria = @IdCategoria
		set @Resultado = 1
		end
		else 
		set @Mensaje = 'La categoria se encuentra relacionada con un producto'
		end
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarFavorito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_EliminarFavorito](  
@IdUsuario int,  
@IdProducto int,  
@Resultado bit output  
)  
as  
   begin try
  
    delete top (1) from Favoritos where IdUsuario = @IdUsuario and IdProducto = @IdProducto  
    set @Resultado = 1 
  end try
  begin catch 
      set @Resultado = 0  
 end catch
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarFavoritoConIdProducto]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_EliminarFavoritoConIdProducto](    
   
@IdProducto int,    
@Resultado bit output    
)    
as    
   begin try  
    
    delete from Favoritos where IdProducto = @IdProducto    
    set @Resultado = 1   
  end try  
  begin catch   
      set @Resultado = 0    
 end catch  
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarMarca]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	 create proc [dbo].[sp_EliminarMarca](
	 	 
@IdMarca int,
@Mensaje varchar(500) output,
@Resultado bit output
	 )
	 as 
begin
     SET @Resultado = 0
	 if not exists (select * from Producto p inner join Marcas m 
	 on m.IdMarcas = p.IdMarca
	 where p.IdMarca = @IdMarca)
	 begin 
	    delete top (1) from Marcas where IdMarcas = @IdMarca
		set @Resultado = 1
		end
		else 
		set @Mensaje = 'La marca se encuentra relacionada con un producto'
		end


		select @IdMarca, Descripcion, Activo  from Marcas
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarProducto]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_EliminarProducto](
	 @IdProducto int ,
	 @Mensaje varchar(500) output,
     @Resultado bit output
	 )
	 as
	 begin 
	 set @Resultado = 0
	 if not exists ( Select * from DetalleVenta DV inner join Producto P
	 on p.IdProducto = dv.IdProducto 
	 where p.IdProducto = @IdProducto)
	 begin 
	 delete top (1) from Producto where IdProducto = @IdProducto
	 set @Resultado = 1
	 end 
	 else 
	 set @Mensaje = 'El producto se encuentra relacionado con una venta'
	 end
GO
/****** Object:  StoredProcedure [dbo].[sp_ExisteCarrito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ExisteCarrito](
@IdCliente int,
@IdProducto int,
@Resultado bit output
)
as
begin
  if exists (select * from Carrito where IdCliente = @IdCliente and IdProducto = @IdProducto)
  set @Resultado = 1
  else 
  set @Resultado = 0
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_ExisteFavorito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ExisteFavorito](  
@IdUsuario int,  
@IdProducto int,  
@Resultado bit output  
)  
as  
begin  
  if exists (select * from Favoritos where IdUsuario = @IdUsuario and IdProducto = @IdProducto)  
  set @Resultado = 1  
  else   
  set @Resultado = 0  
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_OperacionCarrito]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create proc [dbo].[sp_OperacionCarrito](
  @IdCliente int,
  @IdProducto int,
  @Sumar bit,
  @Mensaje varchar (500) output,
  @Resultado bit output
  )
  as
  begin 
     set @Resultado = 1
	 set @Mensaje = ''

	 declare @existeCarrito bit = iif (exists(select * from Carrito where IdCliente = @IdCliente and IdProducto = @IdProducto),1 ,0)
	 declare @stockProducto int = (select stock from Producto where IdProducto = @IdProducto)

	 begin try 

	     begin transaction operacion

		    if (@Sumar = 1)
			 begin 

			    if (@stockProducto >0)
				begin 

				   if( @existeCarrito = 1)
				      update Carrito set Cantidad = Cantidad +1 where IdCliente = @IdCliente and IdProducto = @IdProducto
				   else 
				      insert into  Carrito (IdCliente,IdProducto,Cantidad) values (@IdCliente,@IdProducto,1)

				   update Producto set Stock = stock +1 where IdProducto = @IdProducto
				end 
				else 
				begin 
				    set @Resultado = 0
					set @Mensaje = 'El producto no cuenta con stock disponible'
				end
			end
			else 
			begin 
			     update Carrito set Cantidad = Cantidad -1 where IdCliente = @IdCliente and IdProducto = @IdProducto
				 update Producto set Stock = Stock -1 where IdProducto = @IdProducto
			end

			commit transaction  operacion

		end try 
		begin catch
		    set @Resultado = 0 
			set @Mensaje = ERROR_MESSAGE()
			rollback transaction operacion
		end catch
end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarCategoria]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_RegistrarCategoria](

@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)

as 
begin
     SET @Resultado = 0
	 If not exists ( select * from Categoria where Descripcion = @Descripcion)
	 begin
	 insert into Categoria (Descripcion, Activo) values (@Descripcion, @Activo)

	 set @Resultado = SCOPE_IDENTITY()
	 end
	 else
	 set  @Mensaje = 'La categoria ya existe'
	 end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarCliente]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create proc [dbo].[sp_RegistrarCliente](
@Nombre varchar(100),
@Apellido varchar(100),
@Email varchar(100),
@Pass varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin 
      set @Resultado = 0
	  if not exists (Select * from Cliente where Email = @Email)
	  begin 
	  insert into Cliente (Nombre, Apellido, Email, Pass, Restablecer) values
	  (@Nombre, @Apellido, @Email, @Pass,0)

	  set @Resultado = SCOPE_IDENTITY()
	  end
	  else 
	  set @Mensaje = 'El correo del usuario ya existe'
	  end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarLocal]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_RegistrarLocal](  
	 @Descripcion varchar (50),
	 @Mensaje varchar (500) output, 
	 @Resultado int output
	 )
	 as 
	 begin 
	 set @resultado = -1 

	 if not exists ( select * from locales where Descripcion = @Descripcion) 
	 begin 
       INSERT INTO Locales (Descripcion) values(@Descripcion)

	   set @Resultado = SCOPE_IDENTITY()
	   end	
	   else 
	   set @Resultado = 0
	   set @Mensaje = 'El nombre de la tienda ya existe'
	   end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarMarca]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_RegistrarMarca](

@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)

as 
begin
     SET @Resultado = 0
	 If not exists ( select * from Marcas where Descripcion = @Descripcion)
	 begin
	 insert into Marcas(Descripcion, Activo) values (@Descripcion, @Activo)

	 set @Resultado = SCOPE_IDENTITY()
	 end
	 else
	 set  @Mensaje = 'La marca ya existe'
	 end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarProducto]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[sp_RegistrarProducto](
@Nombre varchar(100),
@Descripcion varchar(100),
@IdMarca varchar(100),
@IdCategoria varchar(100),
@Precio decimal (10, 2),
@Stock int,
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
  SET @Resultado = 0
	 If not exists ( select * from Producto where Nombre = @Nombre and Descripcion = @Descripcion and IdCategoria = @IdCategoria and IdMarca= @IdMarca)
	 begin
	  insert into Producto(Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, Activo)
	  values (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Stock, @Activo)

	  set @Resultado = SCOPE_IDENTITY()
	  end 
	  else 
	  set @Mensaje = 'El producto ya existe'
	  end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuario]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_RegistrarUsuario](
@Nombre varchar (50),
@Apellido varchar (50),
@Email varchar(50),
@Pass varchar (200),
@Activo bit ,
@Mensaje varchar (500) output,
@Resultado int output
)
as 
begin 
   set @resultado = 0

   if not exists ( select * from Usuario where Email = @Email)
   begin
   insert into Usuario (Nombre , Apellido, Email, Pass, Activo) values
   (@Nombre , @Apellido, @Email, @Pass, @Activo)

   set @Resultado = SCOPE_IDENTITY()
   end
   else 
   set @Mensaje = 'El correo de usuario ya existe'
   end
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuarioNuevo]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   CREATE proc [dbo].[sp_RegistrarUsuarioNuevo](        
@Nombre varchar (50),        
@Apellido varchar (50),        
@Email varchar(50),        
@Pass varchar (200),        
@Activo bit ,      
@Direccion varchar (200),        
@Celular varchar (200),        
@IdLocal int ,   
@Tienda varchar(50),  
@IdProvincia int ,        
@IdCiudad int ,        
@IdBarrio int ,      
        
@Mensaje varchar (500) output,        
@Resultado int output        
)        
as         
begin         
   set @resultado = 0        
        
   if not exists ( select * from Usuario where Email = @Email)        
   begin        
   insert into Usuario (Nombre , Apellido, Email, Pass, Activo, Direccion, Celular, IdLocal, Tienda, IdProvincia,IdCiudad,IdBarrio) values        
   (@Nombre , @Apellido, @Email, @Pass, @Activo,@Direccion,@Celular, @IdLocal ,@Tienda ,@IdProvincia ,@IdCiudad,@IdBarrio)        
        
   set @Resultado = SCOPE_IDENTITY()        
   end        
   else         
   set @Mensaje = 'El correo de usuario ya existe'        
   end  
GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteDashboard]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ReporteDashboard]
as begin 

select 
(select count (*) from Cliente )[TotalCliente],
(select ISNULL(sum(cantidad),0)from DetalleVenta) [TotalVenta],
(Select COUNT(*) from Producto)[TotalProducto]

end
GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteVentas]    Script Date: 29/8/2023 09:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ReporteVentas](
@fechaInicio varchar(10),
@fechaFin varchar (10),
@idTransaccion varchar(50)
)
as
begin 

set dateformat dmy;

select Convert (char (10), v.FechaVenta ,103 )[FechaVenta], concat(c.Nombre, '', c.Apellido)[Cliente],
p.nombre[Producto], p.precio, dv.Cantidad, dv.Total, v.IdTransaccion

from DetalleVenta dv
inner join producto p on p.IdProducto = dv.IdProducto
inner join Venta v on v.IdVenta = dv.IdVenta
inner join cliente c on c.IdCliente = v.IdCliente

where convert (date, v.FechaVenta) between @fechaInicio and @fechaFin
and  v.IdTransaccion = iif (@idTransaccion = '', v.IdTransaccion, @idTransaccion)


end
GO
