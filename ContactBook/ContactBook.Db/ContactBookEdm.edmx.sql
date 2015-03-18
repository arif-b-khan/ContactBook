
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/18/2015 09:06:20
-- Generated from EDMX file: C:\Development\github\contactbook\ContactBook\ContactBook.Db\ContactBookEdm.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CB_ContactBook'
CREATE TABLE [dbo].[CB_ContactBook] (
    [BookId] bigint IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [Firstname] nvarchar(100)  NOT NULL,
    [Lastname] nvarchar(100)  NULL,
    [Middlename] nvarchar(max)  NULL,
    [SuffixId] int  NULL,
    [PhFirstname] nvarchar(100)  NOT NULL,
    [PhMiddlename] nvarchar(max)  NULL,
    [PhSurname] nvarchar(max)  NULL,
    [CompanyName] nvarchar(max)  NULL,
    [JobTitle] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NULL,
    [NickName] nvarchar(50)  NULL
);
GO

-- Creating table 'CB_Number'
CREATE TABLE [dbo].[CB_Number] (
    [NumberId] int IDENTITY(1,1) NOT NULL,
    [BookId] bigint  NOT NULL,
    [Number] nvarchar(30)  NOT NULL,
    [NumberTypeId] int  NULL
);
GO

-- Creating table 'CB_Email'
CREATE TABLE [dbo].[CB_Email] (
    [EmailId] int IDENTITY(1,1) NOT NULL,
    [BookId] bigint  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [EmailTypeId] int  NULL
);
GO

-- Creating table 'CB_Address'
CREATE TABLE [dbo].[CB_Address] (
    [AddressId] int IDENTITY(1,1) NOT NULL,
    [BookId] bigint  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [AddressTypeId] int  NULL
);
GO

-- Creating table 'CB_Group'
CREATE TABLE [dbo].[CB_Group] (
    [GroupId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [Group_TypeName] nvarchar(100)  NOT NULL,
    [Group_TypeCode] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'CB_NumberType'
CREATE TABLE [dbo].[CB_NumberType] (
    [NumberTypeId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [Number_TypeName] nvarchar(100)  NOT NULL,
    [Number_TypeCode] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'CB_Book_GroupTypes'
CREATE TABLE [dbo].[CB_Book_GroupTypes] (
    [GroupRelationId] int IDENTITY(1,1) NOT NULL,
    [BookId] bigint  NOT NULL,
    [GroupId] int  NULL
);
GO

-- Creating table 'CB_AddressType'
CREATE TABLE [dbo].[CB_AddressType] (
    [AddressTypeId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [Address_TypeName] nvarchar(100)  NOT NULL,
    [Address_TypeCode] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'CB_EmailType'
CREATE TABLE [dbo].[CB_EmailType] (
    [EmailTypeId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [Email_TypeName] nvarchar(100)  NOT NULL,
    [Email_TypeCode] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'CB_Suffix'
CREATE TABLE [dbo].[CB_Suffix] (
    [SuffixId] int IDENTITY(1,1) NOT NULL,
    [Suffix] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CB_IM'
CREATE TABLE [dbo].[CB_IM] (
    [IMId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [BookId] bigint  NOT NULL,
    [IMTypeId] int  NULL
);
GO

-- Creating table 'CB_IMType'
CREATE TABLE [dbo].[CB_IMType] (
    [IMTypeId] int IDENTITY(1,1) NOT NULL,
    [IM_TypeName] nvarchar(30)  NOT NULL,
    [IMLogoPath] nvarchar(2000)  NULL,
    [IM_TypeCode] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'CB_Website'
CREATE TABLE [dbo].[CB_Website] (
    [WebsiteId] int IDENTITY(1,1) NOT NULL,
    [Website] nvarchar(1000)  NULL,
    [BookId] bigint  NOT NULL
);
GO

-- Creating table 'CB_Relationship'
CREATE TABLE [dbo].[CB_Relationship] (
    [RelationshipId] int IDENTITY(1,1) NOT NULL,
    [BookId] bigint  NOT NULL,
    [RelationshipTypeId] int  NULL
);
GO

-- Creating table 'CB_RelationshipType'
CREATE TABLE [dbo].[CB_RelationshipType] (
    [RelationshipTypeId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [Relationship_TypeName] nvarchar(max)  NOT NULL,
    [Relationship_TypeCode] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'CB_SpecialDates'
CREATE TABLE [dbo].[CB_SpecialDates] (
    [SpecialDateId] int IDENTITY(1,1) NOT NULL,
    [BookId] bigint  NOT NULL,
    [Dates] datetime  NOT NULL,
    [SpecialDateTpId] int  NOT NULL
);
GO

-- Creating table 'CB_SpecialDateType'
CREATE TABLE [dbo].[CB_SpecialDateType] (
    [SpecialDateTpId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [Date_TypeName] nvarchar(100)  NOT NULL,
    [Date_TypeCode] nvarchar(1)  NOT NULL
);
GO

-- Creating table 'CB_InternetCall'
CREATE TABLE [dbo].[CB_InternetCall] (
    [InternetCallId] int IDENTITY(1,1) NOT NULL,
    [InternetCallNumber] nvarchar(100)  NOT NULL,
    [BookId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BookId] in table 'CB_ContactBook'
ALTER TABLE [dbo].[CB_ContactBook]
ADD CONSTRAINT [PK_CB_ContactBook]
    PRIMARY KEY CLUSTERED ([BookId] ASC);
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

-- Creating primary key on [GroupId] in table 'CB_Group'
ALTER TABLE [dbo].[CB_Group]
ADD CONSTRAINT [PK_CB_Group]
    PRIMARY KEY CLUSTERED ([GroupId] ASC);
GO

-- Creating primary key on [NumberTypeId] in table 'CB_NumberType'
ALTER TABLE [dbo].[CB_NumberType]
ADD CONSTRAINT [PK_CB_NumberType]
    PRIMARY KEY CLUSTERED ([NumberTypeId] ASC);
GO

-- Creating primary key on [GroupRelationId] in table 'CB_Book_GroupTypes'
ALTER TABLE [dbo].[CB_Book_GroupTypes]
ADD CONSTRAINT [PK_CB_Book_GroupTypes]
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

-- Creating primary key on [SuffixId] in table 'CB_Suffix'
ALTER TABLE [dbo].[CB_Suffix]
ADD CONSTRAINT [PK_CB_Suffix]
    PRIMARY KEY CLUSTERED ([SuffixId] ASC);
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BookId] in table 'CB_Number'
ALTER TABLE [dbo].[CB_Number]
ADD CONSTRAINT [FK_ContactBookContactNumber]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactNumber'
CREATE INDEX [IX_FK_ContactBookContactNumber]
ON [dbo].[CB_Number]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_Email'
ALTER TABLE [dbo].[CB_Email]
ADD CONSTRAINT [FK_ContactBookContactEmail]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactEmail'
CREATE INDEX [IX_FK_ContactBookContactEmail]
ON [dbo].[CB_Email]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_Book_GroupTypes'
ALTER TABLE [dbo].[CB_Book_GroupTypes]
ADD CONSTRAINT [FK_ContactBookContactBookAndGroup]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactBookAndGroup'
CREATE INDEX [IX_FK_ContactBookContactBookAndGroup]
ON [dbo].[CB_Book_GroupTypes]
    ([BookId]);
GO

-- Creating foreign key on [GroupId] in table 'CB_Book_GroupTypes'
ALTER TABLE [dbo].[CB_Book_GroupTypes]
ADD CONSTRAINT [FK_ContactGroupContactBookAndGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[CB_Group]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactGroupContactBookAndGroup'
CREATE INDEX [IX_FK_ContactGroupContactBookAndGroup]
ON [dbo].[CB_Book_GroupTypes]
    ([GroupId]);
GO

-- Creating foreign key on [BookId] in table 'CB_Address'
ALTER TABLE [dbo].[CB_Address]
ADD CONSTRAINT [FK_ContactBookContactAddress]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactAddress'
CREATE INDEX [IX_FK_ContactBookContactAddress]
ON [dbo].[CB_Address]
    ([BookId]);
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

-- Creating foreign key on [SuffixId] in table 'CB_ContactBook'
ALTER TABLE [dbo].[CB_ContactBook]
ADD CONSTRAINT [FK_Default_SuffixContactBook]
    FOREIGN KEY ([SuffixId])
    REFERENCES [dbo].[CB_Suffix]
        ([SuffixId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Default_SuffixContactBook'
CREATE INDEX [IX_FK_Default_SuffixContactBook]
ON [dbo].[CB_ContactBook]
    ([SuffixId]);
GO

-- Creating foreign key on [BookId] in table 'CB_IM'
ALTER TABLE [dbo].[CB_IM]
ADD CONSTRAINT [FK_ContactBookIM]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookIM'
CREATE INDEX [IX_FK_ContactBookIM]
ON [dbo].[CB_IM]
    ([BookId]);
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

-- Creating foreign key on [BookId] in table 'CB_Website'
ALTER TABLE [dbo].[CB_Website]
ADD CONSTRAINT [FK_ContactBookContactWebsite]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactWebsite'
CREATE INDEX [IX_FK_ContactBookContactWebsite]
ON [dbo].[CB_Website]
    ([BookId]);
GO

-- Creating foreign key on [BookId] in table 'CB_Relationship'
ALTER TABLE [dbo].[CB_Relationship]
ADD CONSTRAINT [FK_ContactBookContactRelationship]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactRelationship'
CREATE INDEX [IX_FK_ContactBookContactRelationship]
ON [dbo].[CB_Relationship]
    ([BookId]);
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

-- Creating foreign key on [BookId] in table 'CB_SpecialDates'
ALTER TABLE [dbo].[CB_SpecialDates]
ADD CONSTRAINT [FK_ContactBookContactSpecialDates]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactSpecialDates'
CREATE INDEX [IX_FK_ContactBookContactSpecialDates]
ON [dbo].[CB_SpecialDates]
    ([BookId]);
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

-- Creating foreign key on [BookId] in table 'CB_InternetCall'
ALTER TABLE [dbo].[CB_InternetCall]
ADD CONSTRAINT [FK_ContactBookContactInternetCall]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[CB_ContactBook]
        ([BookId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactBookContactInternetCall'
CREATE INDEX [IX_FK_ContactBookContactInternetCall]
ON [dbo].[CB_InternetCall]
    ([BookId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------