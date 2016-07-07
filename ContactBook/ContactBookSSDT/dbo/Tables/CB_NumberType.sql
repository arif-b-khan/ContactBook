CREATE TABLE [dbo].[CB_NumberType] (
    [NumberTypeId]    INT            IDENTITY (1, 1) NOT NULL,
    [Number_TypeName] NVARCHAR (100) NOT NULL,
    [BookId]          BIGINT         NULL,
    CONSTRAINT [PK_CB_NumberType] PRIMARY KEY CLUSTERED ([NumberTypeId] ASC),
    CONSTRAINT [FK_CB_ContactBookCB_NumberType] FOREIGN KEY ([BookId]) REFERENCES [dbo].[CB_ContactBook] ([BookId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CB_ContactBookCB_NumberType]
    ON [dbo].[CB_NumberType]([BookId] ASC);

