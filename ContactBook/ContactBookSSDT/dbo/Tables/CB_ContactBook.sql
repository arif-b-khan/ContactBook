CREATE TABLE [dbo].[CB_ContactBook] (
    [BookId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [BookName] NVARCHAR (300) NOT NULL,
    [Enabled]  BIT            NOT NULL,
    [Username] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_CB_ContactBook] PRIMARY KEY CLUSTERED ([BookId] ASC)
);

