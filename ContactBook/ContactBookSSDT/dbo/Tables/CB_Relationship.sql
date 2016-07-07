CREATE TABLE [dbo].[CB_Relationship] (
    [RelationshipId]     INT    IDENTITY (1, 1) NOT NULL,
    [ContactId]          BIGINT NOT NULL,
    [RelationshipTypeId] INT    NULL,
    CONSTRAINT [PK_CB_Relationship] PRIMARY KEY CLUSTERED ([RelationshipId] ASC),
    CONSTRAINT [FK_ContactBookContactRelationship] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_RelationshipTypeContactRelationship] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [dbo].[CB_RelationshipType] ([RelationshipTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactRelationship]
    ON [dbo].[CB_Relationship]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_RelationshipTypeContactRelationship]
    ON [dbo].[CB_Relationship]([RelationshipTypeId] ASC);

