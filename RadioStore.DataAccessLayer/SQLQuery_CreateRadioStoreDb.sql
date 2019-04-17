--drop database RadioStore;

use master;

create database RadioStore;
go; 

use RadioStore;

create table [Categories]
(
	CategoryId int primary key identity,
	CategoryName nvarchar(100) not null,
	CategoryImage nvarchar(500) null,
	CategoryParentId int null default null
)

create table [Products]
(
	ProductId int primary key identity,
	ProductName nvarchar(100) not null,
	ProductImage nvarchar(500) null,
	IsProductPublished bit not null default 0,
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
	PriceProductId int primary key identity,
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
	NewsPublished bit not null
)

create table Carts
(
	CartId int primary key identity,
	UserId nvarchar(128) not null
)

create table CartItems
(
	CartItemId int primary key identity,
	CartId int not null,
	ProductId int not null,
	ProductAmount int not null check(ProductAmount > 0), 
	foreign key (CartId) references Carts(CartId),
	foreign key (ProductId) references Products(ProductId)
)

create table [SpecificationTypes]
(
	SpecificationTypeId int primary key identity,
	SpecificationName nvarchar(100) not null unique,
	CategoryId int null,
	foreign key (CategoryId) references Categories(CategoryId)
)

create table [ProductSpecifications]
(
	SpecificationId int primary key identity,
	SpecificationTypeId int not null,
	SpecificationValue nvarchar(100) null,
	ProductId int not null,
	foreign key (SpecificationTypeId) references SpecificationTypes(SpecificationTypeId),
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


insert into [Categories]([CategoryImage],[CategoryName],[CategoryParentId])
values 
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/ic.gif', N'Active ñomponents', null),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/condensator.png', N'Passive ñomponents', null),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/led_smd.gif', N'LEDs and indicators', null),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/knopki_klaviatury.gif', N'Buttons and keyboards', null)

select *
from [Categories]

insert into [Categories]([CategoryImage],[CategoryName],[CategoryParentId])
values 
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/ic.gif', N'Microcircuits', 1),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/D2PAK.png', N'Transistors', 1),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/condensator.png', N'Capasitors', 2),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/peremennie_potenciometri_vivodnie.gif', N'Resistors', 2),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/peremennie_potenciometri_vivodnie.gif', N'Resistors', 2)

insert into [Categories]([CategoryImage],[CategoryName],[CategoryParentId])
values 
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/microkontrolleri.gif', N'Microcontrollers', 5),
	(N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/catimages/drivery_transistour.gif', N'Transistor drivers', 5)

insert [Products] ([ProductName], [ProductImage], [CategoryId])
values
	(N'ATmega328PB-AU', N'https://www.rcscomponents.kiev.ua/modules/Asers_Shop/images/productimages/tqfp-32.jpg', 10)
	
select *
from[Products]

insert [PriceProduct] ([ProductId], [PriceCountId], [Price])
values 
	(1, 1, 65.00),
	(1, 2, 58.50),
	(2, 1, 85.90),
	(2, 2, 83.65)
