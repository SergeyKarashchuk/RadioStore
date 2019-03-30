--drop database RadioStore;

use master;


create database RadioStore;


use RadioStore;

create table [Categories]
(
	CategoryId int primary key identity,
	CategoryName nvarchar(100) not null,
	CategoryImage nvarchar(500) null,
	CategoryParentId int null
)

create table [Products]
(
	ProductId int primary key identity,
	ProductName nvarchar(100) not null,
	ProductImage nvarchar(500) null,
	CategoryId int null,
	foreign key (CategoryId) references Categories(CategoryId)
)

create table [PriceCount]
(
	PriceCountId int primary key identity,
	ProductCount int not null unique check(ProductCount > 0)
)


create table [PriceProduct]
(
	ProductId int not null,
	PriceCountId int not null,
	Price money not null check(Price > 0)
	foreign key (ProductId) references Products(ProductId),
	foreign key (PriceCountId) references PriceCount(PriceCountId)
)

create table News
(
	NewsId int primary key identity,
	NewsName nvarchar(100) not null,
	NewsImage nvarchar(500) null,
	NewsDiscription nvarchar(1000) not null,
	NewsDate Date not null,
	NewsOutRef nvarchar(500) null,
	NewsVisible int null
)

create table Carts
(
	CartId int primary key identity,
	UserId nvarchar(128) not null
)

create table CartItems
(
	CartId int not null,
	ProductId int not null,
	ProductAmount int not null check(ProductAmount > 0), 
	foreign key (CartId) references Carts(CartId),
	foreign key (ProductId) references Products(ProductId)
)

insert into [PriceCount] ([ProductCount])
values
	(1),
	(10),
	(100),
	(1000)


select *
from PriceCount