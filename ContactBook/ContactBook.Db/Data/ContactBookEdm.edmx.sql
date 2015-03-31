
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/31/2015 08:31:12
-- Generated from EDMX file: C:\Development\github\contactbook\ContactBook\ContactBook.Db\Data\ContactBookEdm.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ContactBook];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ContactBookContactNumber]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Number] DROP CONSTRAINT [FK_ContactBookContactNumber];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookContactEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Email] DROP CONSTRAINT [FK_ContactBookContactEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookContactBookAndGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_ContactByGroup] DROP CONSTRAINT [FK_ContactBookContactBookAndGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactGroupContactBookAndGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_ContactByGroup] DROP CONSTRAINT [FK_ContactGroupContactBookAndGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookContactAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Address] DROP CONSTRAINT [FK_ContactBookContactAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_Custom_NumberTypeContactNumber]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Number] DROP CONSTRAINT [FK_Custom_NumberTypeContactNumber];
GO
IF OBJECT_ID(N'[dbo].[FK_Custom_AddressTypeContactAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Address] DROP CONSTRAINT [FK_Custom_AddressTypeContactAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_Custom_EmailTypeContactEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Email] DROP CONSTRAINT [FK_Custom_EmailTypeContactEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookIM]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_IM] DROP CONSTRAINT [FK_ContactBookIM];
GO
IF OBJECT_ID(N'[dbo].[FK_Custom_IMTypeIM]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_IM] DROP CONSTRAINT [FK_Custom_IMTypeIM];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookContactWebsite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Website] DROP CONSTRAINT [FK_ContactBookContactWebsite];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookContactRelationship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Relationship] DROP CONSTRAINT [FK_ContactBookContactRelationship];
GO
IF OBJECT_ID(N'[dbo].[FK_Custom_RelationshipTypeContactRelationship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Relationship] DROP CONSTRAINT [FK_Custom_RelationshipTypeContactRelationship];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookContactSpecialDates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_SpecialDates] DROP CONSTRAINT [FK_ContactBookContactSpecialDates];
GO
IF OBJECT_ID(N'[dbo].[FK_Custom_SpecialDateTypeContactSpecialDates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_SpecialDates] DROP CONSTRAINT [FK_Custom_SpecialDateTypeContactSpecialDates];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactBookContactInternetCall]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_InternetCall] DROP CONSTRAINT [FK_ContactBookContactInternetCall];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_Contacts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_Contact] DROP CONSTRAINT [FK_CB_ContactBookCB_Contacts];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_RelationshipType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_RelationshipType] DROP CONSTRAINT [FK_CB_ContactBookCB_RelationshipType];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_NumberType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_NumberType] DROP CONSTRAINT [FK_CB_ContactBookCB_NumberType];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_AddressType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_AddressType] DROP CONSTRAINT [FK_CB_ContactBookCB_AddressType];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_SpecialDateType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_SpecialDateType] DROP CONSTRAINT [FK_CB_ContactBookCB_SpecialDateType];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_EmailType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_EmailType] DROP CONSTRAINT [FK_CB_ContactBookCB_EmailType];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_GroupType] DROP CONSTRAINT [FK_CB_ContactBookCB_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_CB_ContactBookCB_IMType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CB_IMType] DROP CONSTRAINT [FK_CB_ContactBookCB_IMType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CB_Contact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_Contact];
GO
IF OBJECT_ID(N'[dbo].[CB_Number]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_Number];
GO
IF OBJECT_ID(N'[dbo].[CB_Email]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_Email];
GO
IF OBJECT_ID(N'[dbo].[CB_Address]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_Address];
GO
IF OBJECT_ID(N'[dbo].[CB_GroupType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_GroupType];
GO
IF OBJECT_ID(N'[dbo].[CB_NumberType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_NumberType];
GO
IF OBJECT_ID(N'[dbo].[CB_ContactByGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_ContactByGroup];
GO
IF OBJECT_ID(N'[dbo].[CB_AddressType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_AddressType];
GO
IF OBJECT_ID(N'[dbo].[CB_EmailType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_EmailType];
GO
IF OBJECT_ID(N'[dbo].[CB_IM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_IM];
GO
IF OBJECT_ID(N'[dbo].[CB_IMType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_IMType];
GO
IF OBJECT_ID(N'[dbo].[CB_Website]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_Website];
GO
IF OBJECT_ID(N'[dbo].[CB_Relationship]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_Relationship];
GO
IF OBJECT_ID(N'[dbo].[CB_RelationshipType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_RelationshipType];
GO
IF OBJECT_ID(N'[dbo].[CB_SpecialDates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_SpecialDates];
GO
IF OBJECT_ID(N'[dbo].[CB_SpecialDateType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_SpecialDateType];
GO
IF OBJECT_ID(N'[dbo].[CB_InternetCall]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_InternetCall];
GO
IF OBJECT_ID(N'[dbo].[CB_ContactBook]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CB_ContactBook];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CB_Contact'
CREATE TABLE [dbo].[CB_Contact] (
    [ContactId] bigint IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(100)  NOT NULL,
    [Lastname] nvarchar(100)  NULL,
    [Middlename] nvarchar(max)  NULL,
    [Suffix] nvarchar(10)  NULL,
    [PhFirstname] nvarchar(100)  NULL,
    [PhMiddlename] nvarchar(max)  NULL,
    [PhSurname] nvarchar(max)  NULL,
    [CompanyName] nvarchar(max)  NULL,
    [JobTitle] nvarchar(max)  NULL,
    [Notes] nvarchar(max)  NULL,
    [NickName] nvarchar(50)  NULL,
    [BookId] bigint  NOT NULL
);
GO

-- Creating table 'CB_Number'
CREATE TABLE [dbo].[CB_Number] (
    [NumberId] int IDENTITY(1,1) NOT NULL,
    [ContactId] bigint  NOT NULL,
    [Number] nvarchar(30)  NOT NULL,
    [NumberTypeId] int  NULL
);
GO

-- Creating table 'CB_Email'
CREATE TABLE [dbo].[CB_Email] (
    [EmailId] int IDENTITY(1,1) NOT NULL,
    [ContactId] bigint  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [EmailTypeId] int  NULL
);
GO

-- Creating table 'CB_Address'
CREATE TABLE [dbo].[CB_Address] (
    [AddressId] int IDENTITY(1,1) NOT NULL,
    [ContactId] bigint  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [AddressTypeId] int  NULL
);
GO

-- Creating table 'CB_GroupType'
CREATE TABLE [dbo].[CB_GroupType] (
    [GroupId] int IDENTITY(1,1) NOT NULL,
    [Group_TypeName] nvarchar(100)  NOT NULL,
    [BookId] bigint  NULL
);
GO

-- Creating table 'CB_NumberType'
CREATE TABLE [dbo].[CB_NumberType] (
    [NumberTypeId] int IDENTITY(1,1) NOT NULL,
    [Number_TypeName] nvarchar(100)  NOT NULL,
    [BookId] bigint  NULL
);
GO

-- Creating table 'CB_ContactByGroup'
CREATE TABLE [dbo].[CB_ContactByGroup] (
    [GroupRelationId] int IDENTITY(1,1) NOT NULL,
    [ContactId] bigint  NOT NULL,
    [GroupId] int  NULL
);
GO

-- Creating table 'CB_AddressType'
CREATE TABLE [dbo].[CB_AddressType] (
    [AddressTypeId] int IDENTITY(1,1) NOT NULL,
    [Address_TypeName] nvarchar(100)  NOT NULL,
    [BookId] bigint  NULL
);
GO

-- Creating table 'CB_EmailType'
CREATE TABLE [dbo].[CB_EmailType] (
    [EmailTypeId] int IDENTITY(1,1) NOT NULL,
    [Email_TypeName] nvarchar(100)  NOT NULL,
    [BookId] bigint  NULL
);
GO

-- Creating table 'CB_IM'
CREATE TABLE [dbo].[CB_IM] (
    [IMId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [ContactId] bigint  NOT NULL,
    [IMTypeId] int  NOT NULL
);
GO

-- Creating table 'CB_IMType'
CREATE TABLE [dbo].[CB_IMType] (
    [IMTypeId] int IDENTITY(1,1) NOT NULL,
    [IM_TypeName] nvarchar(30)  NOT NULL,
    [IMLogoPath] nvarchar(2000)  NULL,
    [BookId] bigint  NULL
);
GO

-- Creating table 'CB_Website'
CREATE TABLE [dbo].[CB_Website] (
    [WebsiteId] int IDENTITY(1,1) NOT NULL,
    [Website] nvarchar(1000)  NULL,
    [ContactId] bigint  NOT NULL
);
GO

-- Creating table 'CB_Relationship'
CREATE TABLE [dbo].[CB_Relationship] (
    [RelationshipId] int IDENTITY(1,1) NOT NULL,
    [ContactId] bigint  NOT NULL,
    [RelationshipTypeId] int  NULL
);
GO

-- Creating table 'CB_RelationshipType'
CREATE TABLE [dbo].[CB_RelationshipType] (
    [RelationshipTypeId] int IDENTITY(1,1) NOT NULL,
    [Relationship_TypeName] nvarchar(max)  NOT NULL,
    [BookId] bigint  NULL
);
GO

-- Creating table 'CB_SpecialDates'
CREATE TABLE [dbo].[CB_SpecialDates] (
    [SpecialDateId] int IDENTITY(1,1) NOT NULL,
    [ContactId] bigint  NOT NULL,
    [Dates] datetime  NOT NULL,
    [SpecialDateTpId] int  NOT NULL
);
GO

-- Creating table 'CB_SpecialDateType'
CREATE TABLE [dbo].[CB_SpecialDateType] (
    [SpecialDateTpId] int IDENTITY(1,1) NOT NULL,
    [Date_TypeName] nvarchar(100)  NOT NULL,
    [BookId] bigint  NULL
);
GO

-- Creating table 'CB_InternetCall'
CREATE TABLE [dbo].[CB_InternetCall] (
    [InternetCallId] int IDENTITY(1,1) NOT NULL,
    [InternetCallNumber] nvarchar(100)  NOT NULL,
    [ContactId] bigint  NOT NULL
);
GO

-- Creating table 'CB_ContactBook'
CREATE TABLE [dbo].[CB_ContactBook] (
    [BookId] bigint IDENTITY(1,1) NOT NULL,
    [BookName] nvarchar(300)  NOT NULL,
    [Enabled] bit  NOT NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ContactId] in table 'CB_Contact'
ALTER TABLE [dbo].[CB_Contact]
ADD CONSTRAINT [PK_CB_Contact]
    PRIMARY KEY CLUSTERED ([ContactId] ASC);
GO

-- Creating primary key on [NumberId] in table 'CB_Number'
ALTER TABLE [dbo].[CB_Number]
ADD CONSTRAINT [PK_CB_Number]
    PRIMARY KEY CLUSTERED ([NumberId] ASC);
GO

-- Creating primary key on [EmailId] in table 'CB_Email'
ALTER TABLE [dbo].[CB_Email]
ADD CONSTRAINT [PK_CB_Email]
    PRIMARY KEY CLUSTERED ([EmailId] ASC);
GO

-- Creating primary key on [AddressId] in table 'CB_Address'
ALTER TABLE [dbo].[CB_Address]
ADD CONSTRAINT [PK_CB_Address]
    PRIMARY KEY CLUSTERED ([AddressId] ASC);
GO

-- Creating primary key on [GroupId] in table 'CB_GroupType'
ALTER TABLE [dbo].[CB_GroupType]
ADD CONSTRAINT [PK_CB_GroupType]
    PRIMARY KEY CLUSTERED ([GroupId] ASC);
GO

-- Creating primary key on [NumberTypeId] in table 'CB_NumberType'
ALTER TABLE [dbo].[CB_NumberType]
ADD CONSTRAINT [PK_CB_NumberType]
    PRIMARY KEY CLUSTERED ([NumberTypeId] ASC);
GO

-- Creating primary key on [GroupRelationId] in table 'CB_ContactByGroup'
ALTER TABLE [dbo].[CB_ContactByGroup]
ADD CONSTRAINT [PK_CB_ContactByGroup]
    PRIMARY KEY CLUSTERED ([GroupRelationId] ASC);
GO

-- Creating primary key on [AddressTypeId] in table 'CB_AddressType'
ALTER TABLE [dbo].[CB_AddressType]
ADD CONSTRAINT [PK_CB_AddressType]
    PRIMARY KEY CLUSTERED ([AddressTypeId] ASC);
GO

-- Creating primary key on [EmailTypeId] in table 'CB_EmailType'
ALTER TABLE [dbo].[CB_EmailType]
ADD CONSTRAINT [PK_CB_EmailType]
    PRIMARY KEY CLUSTERED ([EmailTypeId] ASC);
GO

-- Creating primary key on [IMId] in table 'CB_IM'
ALTER TABLE [dbo].[CB_IM]
ADD CONSTRAINT [PK_CB_IM]
    PRIMARY KEY CLUSTERED ([IMId] ASC);
GO

-- Creating primary key on [IMTypeId] in table 'CB_IMType'
ALTER TABLE [dbo].[CB_IMType]
ADD CONSTRAINT [PK_CB_IMType]
    PRIMARY KEY CLUSTERED ([IMTypeId] ASC);
GO

-- Creating primary key on [WebsiteId] in table 'CB_Website'
ALTER TABLE [dbo].[CB_Website]
ADD CONSTRAINT [PK_CB_Website]
    PRIMARY KEY CLUSTERED ([WebsiteId] ASC);
GO

-- Creating primary key on [RelationshipId] in table 'CB_Relationship'
ALTER TABLE [dbo].[CB_Relationship]
ADD CONSTRAINT [PK_CB_Relationship]
    PRIMARY KEY CLUSTERED ([RelationshipId] ASC);
GO

-- Creating primary key on [RelationshipTypeId] in table 'CB_RelationshipType'
ALTER TABLE [dbo].[CB_RelationshipType]
ADD CONSTRAINT [PK_CB_RelationshipType]
    PRIMARY KEY CLUSTERED ([RelationshipTypeId] ASC);
GO

-- Creating primary key on [SpecialDateId] in table 'CB_SpecialDates'
ALTER TABLE [dbo].[CB_SpecialDates]
ADD CONSTRAINT [PK_CB_SpecialDates]
    PRIMARY KEY CLUSTERED ([SpecialDateId] ASC);
GO

-- Creating primary key on [SpecialDateTpId] in table 'CB_SpecialDateType'
ALTER TABLE [dbo].[CB_SpecialDateType]
ADD CONSTRAINT [PK_CB_SpecialDateType]
    PRIMARY KEY CLUSTERED ([SpecialDateTpId] ASC);
GO

-- Creating primary key on [InternetCallId] in table 'CB_InternetCall'
ALTER TABLE [dbo].[CB_InternetCall]
ADD CONSTRAINT [PK_CB_InternetCall]
    PRIMARY KEY CLUSTERED ([InternetCallId] ASC);
GO

-- Creating primary key on [BookId] in table 'CB_ContactBook'
ALTER TABLE [dbo].[CB_ContactBook]
ADD CONSTRAINT [PK_CB_ContactBook]
    PRIMARY KEY CLUSTERED ([BookId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ContactId] in table 'CB_Number'
ALTER TABLE [dbo].[CB_Number]
ADD CONSTRAINT [FK_ContactBookContactNumber]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactNumber'
CREATE INDEX [IX_FK_ContactBookContactNumber]
ON [dbo].[CB_Number]
    ([ContactId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_Email'
ALTER TABLE [dbo].[CB_Email]
ADD CONSTRAINT [FK_ContactBookContactEmail]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactEmail'
CREATE INDEX [IX_FK_ContactBookContactEmail]
ON [dbo].[CB_Email]
    ([ContactId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_ContactByGroup'
ALTER TABLE [dbo].[CB_ContactByGroup]
ADD CONSTRAINT [FK_ContactBookContactBookAndGroup]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactBookAndGroup'
CREATE INDEX [IX_FK_ContactBookContactBookAndGroup]
ON [dbo].[CB_ContactByGroup]
    ([ContactId]);
GO

-- Creating foreign key on [GroupId] in table 'CB_ContactByGroup'
ALTER TABLE [dbo].[CB_ContactByGroup]
ADD CONSTRAINT [FK_ContactGroupContactBookAndGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[CB_GroupType]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactGroupContactBookAndGroup'
CREATE INDEX [IX_FK_ContactGroupContactBookAndGroup]
ON [dbo].[CB_ContactByGroup]
    ([GroupId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_Address'
ALTER TABLE [dbo].[CB_Address]
ADD CONSTRAINT [FK_ContactBookContactAddress]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactAddress'
CREATE INDEX [IX_FK_ContactBookContactAddress]
ON [dbo].[CB_Address]
    ([ContactId]);
GO

-- Creating foreign key on [NumberTypeId] in table 'CB_Number'
ALTER TABLE [dbo].[CB_Number]
ADD CONSTRAINT [FK_Custom_NumberTypeContactNumber]
    FOREIGN KEY ([NumberTypeId])
    REFERENCES [dbo].[CB_NumberType]
        ([NumberTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Custom_NumberTypeContactNumber'
CREATE INDEX [IX_FK_Custom_NumberTypeContactNumber]
ON [dbo].[CB_Number]
    ([NumberTypeId]);
GO

-- Creating foreign key on [AddressTypeId] in table 'CB_Address'
ALTER TABLE [dbo].[CB_Address]
ADD CONSTRAINT [FK_Custom_AddressTypeContactAddress]
    FOREIGN KEY ([AddressTypeId])
    REFERENCES [dbo].[CB_AddressType]
        ([AddressTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Custom_AddressTypeContactAddress'
CREATE INDEX [IX_FK_Custom_AddressTypeContactAddress]
ON [dbo].[CB_Address]
    ([AddressTypeId]);
GO

-- Creating foreign key on [EmailTypeId] in table 'CB_Email'
ALTER TABLE [dbo].[CB_Email]
ADD CONSTRAINT [FK_Custom_EmailTypeContactEmail]
    FOREIGN KEY ([EmailTypeId])
    REFERENCES [dbo].[CB_EmailType]
        ([EmailTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Custom_EmailTypeContactEmail'
CREATE INDEX [IX_FK_Custom_EmailTypeContactEmail]
ON [dbo].[CB_Email]
    ([EmailTypeId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_IM'
ALTER TABLE [dbo].[CB_IM]
ADD CONSTRAINT [FK_ContactBookIM]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookIM'
CREATE INDEX [IX_FK_ContactBookIM]
ON [dbo].[CB_IM]
    ([ContactId]);
GO

-- Creating foreign key on [IMTypeId] in table 'CB_IM'
ALTER TABLE [dbo].[CB_IM]
ADD CONSTRAINT [FK_Custom_IMTypeIM]
    FOREIGN KEY ([IMTypeId])
    REFERENCES [dbo].[CB_IMType]
        ([IMTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Custom_IMTypeIM'
CREATE INDEX [IX_FK_Custom_IMTypeIM]
ON [dbo].[CB_IM]
    ([IMTypeId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_Website'
ALTER TABLE [dbo].[CB_Website]
ADD CONSTRAINT [FK_ContactBookContactWebsite]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactWebsite'
CREATE INDEX [IX_FK_ContactBookContactWebsite]
ON [dbo].[CB_Website]
    ([ContactId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_Relationship'
ALTER TABLE [dbo].[CB_Relationship]
ADD CONSTRAINT [FK_ContactBookContactRelationship]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactRelationship'
CREATE INDEX [IX_FK_ContactBookContactRelationship]
ON [dbo].[CB_Relationship]
    ([ContactId]);
GO

-- Creating foreign key on [RelationshipTypeId] in table 'CB_Relationship'
ALTER TABLE [dbo].[CB_Relationship]
ADD CONSTRAINT [FK_Custom_RelationshipTypeContactRelationship]
    FOREIGN KEY ([RelationshipTypeId])
    REFERENCES [dbo].[CB_RelationshipType]
        ([RelationshipTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Custom_RelationshipTypeContactRelationship'
CREATE INDEX [IX_FK_Custom_RelationshipTypeContactRelationship]
ON [dbo].[CB_Relationship]
    ([RelationshipTypeId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_SpecialDates'
ALTER TABLE [dbo].[CB_SpecialDates]
ADD CONSTRAINT [FK_ContactBookContactSpecialDates]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactSpecialDates'
CREATE INDEX [IX_FK_ContactBookContactSpecialDates]
ON [dbo].[CB_SpecialDates]
    ([ContactId]);
GO

-- Creating foreign key on [SpecialDateTpId] in table 'CB_SpecialDates'
ALTER TABLE [dbo].[CB_SpecialDates]
ADD CONSTRAINT [FK_Custom_SpecialDateTypeContactSpecialDates]
    FOREIGN KEY ([SpecialDateTpId])
    REFERENCES [dbo].[CB_SpecialDateType]
        ([SpecialDateTpId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Custom_SpecialDateTypeContactSpecialDates'
CREATE INDEX [IX_FK_Custom_SpecialDateTypeContactSpecialDates]
ON [dbo].[CB_SpecialDates]
    ([SpecialDateTpId]);
GO

-- Creating foreign key on [ContactId] in table 'CB_InternetCall'
ALTER TABLE [dbo].[CB_InternetCall]
ADD CONSTRAINT [FK_ContactBookContactInternetCall]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[CB_Contact]
        ([ContactId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactInternetCall'
CREATE INDEX [IX_FK_ContactBookContactInternetCall]
ON [dbo].[CB_InternetCall]
    ([ContactId]);
GO

-- Creating foreign key on [BookId] in table 'CB_Contact'
ALTER TABLE [dbo].[CB_Contact]
ADD CONSTRAINT [FK_CB_ContactBookCB_Contacts]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_Contacts'
CREATE INDEX [IX_FK_CB_ContactBookCB_Contacts]
ON [dbo].[CB_Contact]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_RelationshipType'
ALTER TABLE [dbo].[CB_RelationshipType]
ADD CONSTRAINT [FK_CB_ContactBookCB_RelationshipType]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_RelationshipType'
CREATE INDEX [IX_FK_CB_ContactBookCB_RelationshipType]
ON [dbo].[CB_RelationshipType]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_NumberType'
ALTER TABLE [dbo].[CB_NumberType]
ADD CONSTRAINT [FK_CB_ContactBookCB_NumberType]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_NumberType'
CREATE INDEX [IX_FK_CB_ContactBookCB_NumberType]
ON [dbo].[CB_NumberType]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_AddressType'
ALTER TABLE [dbo].[CB_AddressType]
ADD CONSTRAINT [FK_CB_ContactBookCB_AddressType]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_AddressType'
CREATE INDEX [IX_FK_CB_ContactBookCB_AddressType]
ON [dbo].[CB_AddressType]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_SpecialDateType'
ALTER TABLE [dbo].[CB_SpecialDateType]
ADD CONSTRAINT [FK_CB_ContactBookCB_SpecialDateType]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_SpecialDateType'
CREATE INDEX [IX_FK_CB_ContactBookCB_SpecialDateType]
ON [dbo].[CB_SpecialDateType]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_EmailType'
ALTER TABLE [dbo].[CB_EmailType]
ADD CONSTRAINT [FK_CB_ContactBookCB_EmailType]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_EmailType'
CREATE INDEX [IX_FK_CB_ContactBookCB_EmailType]
ON [dbo].[CB_EmailType]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_GroupType'
ALTER TABLE [dbo].[CB_GroupType]
ADD CONSTRAINT [FK_CB_ContactBookCB_Group]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_Group'
CREATE INDEX [IX_FK_CB_ContactBookCB_Group]
ON [dbo].[CB_GroupType]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_IMType'
ALTER TABLE [dbo].[CB_IMType]
ADD CONSTRAINT [FK_CB_ContactBookCB_IMType]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CB_ContactBookCB_IMType'
CREATE INDEX [IX_FK_CB_ContactBookCB_IMType]
ON [dbo].[CB_IMType]
    ([BookId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------