CREATE TABLE [dbo].[CB_Number] (
    [NumberId]     INT           IDENTITY (1, 1) NOT NULL,
    [ContactId]    BIGINT        NOT NULL,
    [Number]       NVARCHAR (30) NOT NULL,
    [NumberTypeId] INT           NULL,
    CONSTRAINT [PK_CB_Number] PRIMARY KEY CLUSTERED ([NumberId] ASC),
    CONSTRAINT [FK_ContactBookContactNumber] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_NumberTypeContactNumber] FOREIGN KEY ([NumberTypeId]) REFERENCES [dbo].[CB_NumberType] ([NumberTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactNumber]
    ON [dbo].[CB_Number]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_NumberTypeContactNumber]
    ON [dbo].[CB_Number]([NumberTypeId] ASC);

