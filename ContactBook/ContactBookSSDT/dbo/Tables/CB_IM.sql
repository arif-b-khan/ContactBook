CREATE TABLE [dbo].[CB_IM] (
    [IMId]      INT            IDENTITY (1, 1) NOT NULL,
    [Username]  NVARCHAR (MAX) NOT NULL,
    [ContactId] BIGINT         NOT NULL,
    [IMTypeId]  INT            NOT NULL,
    CONSTRAINT [PK_CB_IM] PRIMARY KEY CLUSTERED ([IMId] ASC),
    CONSTRAINT [FK_ContactBookIM] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_IMTypeIM] FOREIGN KEY ([IMTypeId]) REFERENCES [dbo].[CB_IMType] ([IMTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookIM]
    ON [dbo].[CB_IM]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_IMTypeIM]
    ON [dbo].[CB_IM]([IMTypeId] ASC);

