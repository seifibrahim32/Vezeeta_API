CREATE DATABASE Vezeeta;
--DROP DATABASE Vezeeta;

use Vezeeta;

CREATE TABLE Users (
	userID int PRIMARY KEY IDENTITY(1,1),
	emailUser nvarchar(50) NOT NULL UNIQUE,
	passwordUser nvarchar(50),
	personType nvarchar(10) NOT NULL UNIQUE
);

CREATE TABLE Patients (
	patientID int PRIMARY KEY IDENTITY(1,1),
	emailPatient nvarchar(50) UNIQUE NOT NULL,
	passwordPatient nvarchar(50),
	firstName nvarchar(50),
	lastName nvarchar(50),
	imagePath nvarchar(50),
	patientPhone nvarchar(50),
	dateOfBirth DATE,
	gender int,
	personType nvarchar(10) FOREIGN KEY REFERENCES Users(personType) DEFAULT 'Patient'
);

CREATE TABLE Doctors (
	doctorID int FOREIGN KEY REFERENCES Users(userID),
	emailDoctor nvarchar(50) UNIQUE NOT NULL,
	fullName nvarchar(50),
	passwordDoctor nvarchar(50),
	phone nvarchar(50) UNIQUE NOT NULL,
	specialization nvarchar(50),
	imagePath nvarchar(50),
	personType nvarchar(10) FOREIGN KEY REFERENCES Users(personType) DEFAULT 'Doctor'
);

CREATE TABLE Appointments (
	doctorID int FOREIGN KEY REFERENCES Users(userID),
	emailDoctor nvarchar(50) UNIQUE NOT NULL,
	fullName nvarchar(50) PRIMARY KEY, 
	phone nvarchar(50) UNIQUE NOT NULL,
	specialization nvarchar(50), 
);

INSERT INTO [dbo].[Users](emailUser,passwordUser,personType) VALUES('sefsd@gmail.com','vddfd','Patient');
INSERT INTO [dbo].[Users](emailUser,passwordUser,personType) VALUES('efsasd@gmail.com','vddfjd','Doctor');   

INSERT INTO [dbo].[Doctors](emailDoctor,passwordDoctor,personType,phone) VALUES('sefsd@gmail.com','vddfd','Doctor','213123123');
INSERT INTO [dbo].[Doctors](emailDoctor,passwordDoctor,personType,phone) VALUES('efsssd@gmail.com','vddfd','Doctor','2131231231');
INSERT INTO [dbo].[Doctors](emailDoctor,passwordDoctor,personType,phone) VALUES('efsnmsssd@gmail.com','vddfd','Doctor','1213123123'); 

INSERT INTO [dbo].[Patients](emailPatient,passwordPatient,personType,dateOfBirth) VALUES('sefsd@gmail.com','vddfd','Patient','2018-04-01');
INSERT INTO [dbo].[Patients](emailPatient,passwordPatient,personType,dateOfBirth) VALUES('efsssd@gmail.com','vddfd','Patient','2018-04-01');
INSERT INTO [dbo].[Patients](emailPatient,passwordPatient,personType,dateOfBirth) VALUES('efsnFSsssd@gmail.com','vddfd','Patient','2118-04-01');

-- TRUNCATE TABLE [dbo].[Users]
-- DROP TABLE [dbo].[Patient]
-- Delete from [dbo].[Users] where personType = 'Djoctor'

--ALTER TABLE Doctors

--ADD CONSTRAINT personType
--DEFAULT 'Doctor' FOR personType;

select * from [dbo].[Users]
select * from [dbo].[Doctors]
select * from [dbo].[Patients]
-- DELETE from [dbo].[Users] where personType is null