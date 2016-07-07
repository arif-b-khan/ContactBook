CREATE TABLE [dbo].[CB_AddressType] (
    [AddressTypeId]    INT            IDENTITY (1, 1) NOT NULL,
    [Address_TypeName] NVARCHAR (100) NOT NULL,
    [BookId]           BIGINT         NULL,
    CONSTRAINT [PK_CB_AddressType] PRIMARY KEY CLUSTERED ([AddressTypeId] ASC),
    CONSTRAINT [FK_CB_ContactBookCB_AddressType] FOREIGN KEY ([BookId]) REFERENCES [dbo].[CB_ContactBook] ([BookId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CB_ContactBookCB_AddressType]
    ON [dbo].[CB_AddressType]([BookId] ASC);

