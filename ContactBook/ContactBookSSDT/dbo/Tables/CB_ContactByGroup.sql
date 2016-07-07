CREATE TABLE [dbo].[CB_ContactByGroup] (
    [GroupRelationId] INT    IDENTITY (1, 1) NOT NULL,
    [ContactId]       BIGINT NOT NULL,
    [GroupId]         INT    NOT NULL,
    CONSTRAINT [PK_CB_ContactByGroup] PRIMARY KEY CLUSTERED ([GroupRelationId] ASC),
    CONSTRAINT [FK_ContactBookContactBookAndGroup] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContactGroupContactBookAndGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[CB_GroupType] ([GroupId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactBookAndGroup]
    ON [dbo].[CB_ContactByGroup]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactGroupContactBookAndGroup]
    ON [dbo].[CB_ContactByGroup]([GroupId] ASC);

