CREATE TABLE [dbo].[CB_Contact] (
    [ContactId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [Firstname]     NVARCHAR (100) NOT NULL,
    [Lastname]      NVARCHAR (100) NULL,
    [Middlename]    NVARCHAR (MAX) NULL,
    [Suffix]        NVARCHAR (10)  NULL,
    [PhFirstname]   NVARCHAR (100) NULL,
    [PhMiddlename]  NVARCHAR (MAX) NULL,
    [PhSurname]     NVARCHAR (MAX) NULL,
    [CompanyName]   NVARCHAR (MAX) NULL,
    [JobTitle]      NVARCHAR (MAX) NULL,
    [Notes]         NVARCHAR (MAX) NULL,
    [NickName]      NVARCHAR (50)  NULL,
    [BookId]        BIGINT         NOT NULL,
    [ImagePath]     NVARCHAR (MAX) NULL,
    [ThumbnailPath] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CB_Contact] PRIMARY KEY CLUSTERED ([ContactId] ASC),
    CONSTRAINT [FK_CB_ContactBookCB_Contacts] FOREIGN KEY ([BookId]) REFERENCES [dbo].[CB_ContactBook] ([BookId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CB_ContactBookCB_Contacts]
    ON [dbo].[CB_Contact]([BookId] ASC);

