create database SegundoParcialAP2
go
use SegundoParcialAP2
go

create table Cuentas(
CuentaId int not null identity primary key,
Fecha date,
Nombre varchar(15),
Balance money

);

create table Prestamos(
PrestamoId int not null identity primary key,
Fecha date,
CuentaId int not null,
Capital money,
TasaInteres float,
Tiempo int

);

create table PrestamosDetalles(
Id int not null identity primary key,
PrestamoId int not null,
NoCuota int,
Interes float,
Capital money,
ValorPrestamo money,
Balance money 
);