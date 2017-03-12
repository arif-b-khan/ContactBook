CREATE TABLE [dbo].[Relationships] (
    [RelationshipId]     INT    IDENTITY (1, 1) NOT NULL,
    [ContactId]          BIGINT NOT NULL,
    [RelationshipTypeId] INT    NULL,
    CONSTRAINT [PK_CB_Relationship] PRIMARY KEY CLUSTERED ([RelationshipId] ASC),
    CONSTRAINT [FK_ContactBookContactRelationship] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_RelationshipTypeContactRelationship] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [dbo].[RelationshipTypes] ([RelationshipTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactRelationship]
    ON [dbo].[Relationships]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_RelationshipTypeContactRelationship]
    ON [dbo].[Relationships]([RelationshipTypeId] ASC);

