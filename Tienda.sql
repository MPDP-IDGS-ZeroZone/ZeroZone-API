create database Tienda
--
use Tienda

--Productos--
create table Productos (
idproducto int primary key identity (1,1) not null,
nombre varchar (70) not null,
precio decimal (10,2) not null,
idusuariosocio int not null,
);
--
select * from Productos

--Usuarios Socios--
create table Usuarios_socios (
idusuariosocio int primary key identity (1,1) not null,
contraseña nvarchar (max) not null,
rol nvarchar (20) not null,
mail nvarchar (50) not null,
idsocio int not null
);
--
select * from Usuarios_socios

--Socios--
create table Socios (
idsocio int primary key identity (1,1) not null,
nombre nvarchar (70) not null,
apellidos nvarchar (70) not null,
fecha_nacimiento date not null
);
--
select * from Socios

--Carrito--
create table Carrito (
idcarrito int primary key identity (1,1) not null,
fecha_registro date not null,
idcliente int not null,
idproductos int not null,
monto decimal (10,2) not null
);
--
select * from Carrito

--Clientes--
create table Clientes (
idcliente int primary key identity (1,1) not null,
nombre varchar (70) not null,
apellidos varchar (70) not null,
fecha_nacimeinto date not null,
usuario varchar (50) not null,
contraseña varchar (50) not null,
rol varchar (20) not null
);
--
select * from Clientes

--Referencias--
alter table Productos add constraint Productos_fk0 foreign key (idusuariosocio)
references Usuarios_socios (idusuariosocio);

alter table Usuarios_socios add constraint Usuarios_socios_fk0 foreign key (idsocio)
references Socios (idsocio);

alter table Carrito add constraint Carrito_fk0 foreign key (idcliente)
references Clientes (idcliente);

alter table Carrito add constraint Carrito_fk1 foreign key (idproductos)
references Productos (idproducto);