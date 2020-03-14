select * from operacao.ContaOperacao as c order by DataOperacao desc;

Create Table Customer (Id varchar(32) not null, CpfCnpj varchar(14) not null, Name varchar(255) not null, BirthDate datetime null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table LoanRequest (Id varchar(32) not null, IdCustomer varchar(32) not null, VlAmount decimal(14,9) not null, IdTerms int not null, VlIncome decimal(14,9) not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table LoanProcess (Id varchar(32) not null, IdLoanRequest varchar(32) not null, IdStatus int, Result varchar(20) null, RefusedPolicy varchar(255),  VlAmount decimal(14,9) not null, IdTerms int not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table Terms (Id int PRIMARY KEY AUTO_INCREMENT, Description int not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table Status (Id int PRIMARY KEY AUTO_INCREMENT, Description varchar(20) not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);

insert into Terms (Description, Modificado, StatusRow, IdUserInsert) select 6, now(), 'I', -1;
insert into Terms (Description, Modificado, StatusRow, IdUserInsert) select 9, now(), 'I', -1;
insert into Terms (Description, Modificado, StatusRow, IdUserInsert) select 12, now(), 'I', -1;

insert into Status (Description, Modificado, StatusRow, IdUserInsert) select 'Processing', now(), 'I', -1;
insert into Status (Description, Modificado, StatusRow, IdUserInsert) select 'Completed', now(), 'I', -1;

select * from credito.Client;
select * from credito.LoanRequest;
select * from credito.LoanProcess;
select * from credito.Terms;
select * from credito.Status;

drop table credito.Customer;
drop table credito.LoanRequest;
drop table credito.LoanProcess;
drop table credito.Terms;
drop table credito.Status;


Create Table Customer (Id varchar(32) not null, CpfCnpj varchar(14) not null, Name varchar(255) not null, BirthDate datetime null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table LoanRequest (Id varchar(32) not null, IdCustomer varchar(32) not null, VlAmount decimal(14,9) not null, IdTerms varchar(32) not null, VlIncome decimal(14,9) not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table LoanProcess (Id varchar(32) not null, IdLoanRequest varchar(32) not null, IdStatus varchar(32), Result varchar(20) null, RefusedPolicy varchar(255),  VlAmount decimal(14,9) not null, IdTerms varchar(32) not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table Terms (Id varchar(32), Description int not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
Create Table Status (Id varchar(32), Description varchar(20) not null, Modificado datetime not null, StatusRow char(1) not null, IdUserInsert int not null, IdUserUpdate int null);
