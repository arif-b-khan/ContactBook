CREATE TABLE [dbo].[EmailTypes] (
    [EmailTypeId]    INT            IDENTITY (1, 1) NOT NULL,
    [Email_TypeName] NVARCHAR (100) NOT NULL,
    [BookId]         BIGINT         NULL,
    CONSTRAINT [PK_CB_EmailType] PRIMARY KEY CLUSTERED ([EmailTypeId] ASC),
    CONSTRAINT [FK_CB_ContactBookCB_EmailType] FOREIGN KEY ([BookId]) REFERENCES [dbo].[ContactBooks] ([BookId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CB_ContactBookCB_EmailType]
    ON [dbo].[EmailTypes]([BookId] ASC);

