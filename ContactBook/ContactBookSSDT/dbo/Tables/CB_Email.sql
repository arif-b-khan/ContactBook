CREATE TABLE [dbo].[CB_Email] (
    [EmailId]     INT            IDENTITY (1, 1) NOT NULL,
    [ContactId]   BIGINT         NOT NULL,
    [Email]       NVARCHAR (MAX) NOT NULL,
    [EmailTypeId] INT            NULL,
    CONSTRAINT [PK_CB_Email] PRIMARY KEY CLUSTERED ([EmailId] ASC),
    CONSTRAINT [FK_ContactBookContactEmail] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_EmailTypeContactEmail] FOREIGN KEY ([EmailTypeId]) REFERENCES [dbo].[CB_EmailType] ([EmailTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactEmail]
    ON [dbo].[CB_Email]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_EmailTypeContactEmail]
    ON [dbo].[CB_Email]([EmailTypeId] ASC);

