create database Weather;

create table Users(
	Id bigint primary key,
	ChatId bigint not null,
	SelectedCity varchar(255)
);

create table WeatherHistory(
	Id int identity(1,1) primary key,
	Date DateTime not null,
	Query varchar(255) not null,
	UserId bigint not null,
	foreign key(UserId) references Users(Id)
);

--drop table WeatherHistory;
--drop table Users;

--SELECT * FROM Users;
--SELECT * FROM WeatherHistory;

