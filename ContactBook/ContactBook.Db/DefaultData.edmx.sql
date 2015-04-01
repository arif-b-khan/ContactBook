
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/18/2015 09:06:20
-- Generated from EDMX file: C:\Development\github\contactbook\ContactBook\ContactBook.Db\ContactBookEdm.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
use [ContactBook]
--USE [D:\DEVELOPMENT\GITHUB\CONTACTBOOK\CONTACTBOOK\CONTACTBOOK.DB\TESTDB\TESTDB.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO
IF OBJECT_ID(N'[dbo].[CB_NumberType]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_NumberType(BookId, Number_TypeName) values (null, 'Mobile')
	INSERT INTO CB_NumberType(BookId, Number_TypeName) values (null, 'Work')
	INSERT INTO CB_NumberType(BookId, Number_TypeName) values (null, 'Home')
	INSERT INTO CB_NumberType(BookId, Number_TypeName) values (null, 'Main')
	INSERT INTO CB_NumberType(BookId, Number_TypeName) values (null, 'Work Fax')
	INSERT INTO CB_NumberType(BookId, Number_TypeName) values (null, 'Page')
	INSERT INTO CB_NumberType(BookId, Number_TypeName) values (null, 'Other')
END
GO

IF OBJECT_ID(N'[dbo].[CB_EmailType]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_EmailType(BookId,Email_TypeName) values (null, 'Home')
	INSERT INTO CB_EmailType(BookId,Email_TypeName) values (null, 'Work')
	INSERT INTO CB_EmailType(BookId,Email_TypeName) values (null, 'Other')
END
GO
IF OBJECT_ID(N'[dbo].[CB_AddressType]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_AddressType(BookId, Address_TypeName) values (null, 'Home')
	INSERT INTO CB_AddressType(BookId, Address_TypeName) values (null, 'Work')
	INSERT INTO CB_AddressType(BookId, Address_TypeName) values (null, 'Other')
END
GO
IF OBJECT_ID(N'[dbo].[CB_SpecialDateType]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_SpecialDateType(BookId, Date_TypeName) values (null, 'Birthday')
	INSERT INTO CB_SpecialDateType(BookId, Date_TypeName) values (null, 'Anniversary')
	INSERT INTO CB_SpecialDateType(BookId, Date_TypeName) values (null, 'Other')
END
GO
IF OBJECT_ID(N'[dbo].[CB_Group]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_Group(BookId, Group_TypeName) values (null, 'Friends')
	INSERT INTO CB_Group(BookId, Group_TypeName) values (null, 'Family')
	INSERT INTO CB_Group(BookId, Group_TypeName) values (null, 'Co Worker')
END
GO

IF OBJECT_ID(N'[dbo].[CB_RelationshipType]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Assistant')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Father')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Brother')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Mother')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Spouse')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Son')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Daughter')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Uncle')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Aunty')
	INSERT INTO CB_RelationshipType(BookId, Relationship_TypeName) values (null, 'Manager')
END
GO
IF OBJECT_ID(N'[dbo].[CB_IMType]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_IMType(BookId, IM_TypeName, IMLogoPath) values (null, 'Yahoo', '~/')
	INSERT INTO CB_IMType(BookId, IM_TypeName, IMLogoPath) values (null, 'IM', '~/')
	INSERT INTO CB_IMType(BookId, IM_TypeName, IMLogoPath) values (null, 'Facebook', '~/')
	INSERT INTO CB_IMType(BookId, IM_TypeName, IMLogoPath) values (null, 'Skype', '~/')
	INSERT INTO CB_IMType(BookId, IM_TypeName, IMLogoPath) values (null, 'Hangouts', '~/')
	INSERT INTO CB_IMType(BookId, IM_TypeName, IMLogoPath) values (null, 'Jabber', '~/')
END
GO
IF OBJECT_ID(N'[dbo].[CB_ContactBook]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_ContactBook(BookName, [Enabled], Username) values ('axkhan-1', 1, 'testuser');
END
GO
IF OBJECT_ID(N'[dbo].[CB_Contact]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_Contact(BookId,Firstname) values (1, 'arif');
END
GO
IF OBJECT_ID(N'[dbo].[CB_Number]', 'U') IS NOT NULL
BEGIN
	INSERT INTO CB_Number(ContactId, Number, NumberTypeId) values (1, '9768836054', 1);
END
