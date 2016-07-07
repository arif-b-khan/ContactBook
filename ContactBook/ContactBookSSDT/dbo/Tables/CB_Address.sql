CREATE TABLE [dbo].[CB_Address] (
    [AddressId]     INT            IDENTITY (1, 1) NOT NULL,
    [ContactId]     BIGINT         NOT NULL,
    [Address]       NVARCHAR (MAX) NOT NULL,
    [AddressTypeId] INT            NULL,
    CONSTRAINT [PK_CB_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_ContactBookContactAddress] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_AddressTypeContactAddress] FOREIGN KEY ([AddressTypeId]) REFERENCES [dbo].[CB_AddressType] ([AddressTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactAddress]
    ON [dbo].[CB_Address]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_AddressTypeContactAddress]
    ON [dbo].[CB_Address]([AddressTypeId] ASC);

