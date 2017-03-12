CREATE TABLE [dbo].[IMs] (
    [IMId]      INT            IDENTITY (1, 1) NOT NULL,
    [Username]  NVARCHAR (MAX) NOT NULL,
    [ContactId] BIGINT         NOT NULL,
    [IMTypeId]  INT            NOT NULL,
    CONSTRAINT [PK_CB_IM] PRIMARY KEY CLUSTERED ([IMId] ASC),
    CONSTRAINT [FK_ContactBookIM] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_IMTypeIM] FOREIGN KEY ([IMTypeId]) REFERENCES [dbo].[IMTypes] ([IMTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookIM]
    ON [dbo].[IMs]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_IMTypeIM]
    ON [dbo].[IMs]([IMTypeId] ASC);

