CREATE TABLE [dbo].[Emails] (
    [EmailId]     INT            IDENTITY (1, 1) NOT NULL,
    [ContactId]   BIGINT         NOT NULL,
    [Email]       NVARCHAR (MAX) NOT NULL,
    [EmailTypeId] INT            NULL,
    CONSTRAINT [PK_CB_Email] PRIMARY KEY CLUSTERED ([EmailId] ASC),
    CONSTRAINT [FK_ContactBookContactEmail] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_EmailTypeContactEmail] FOREIGN KEY ([EmailTypeId]) REFERENCES [dbo].[EmailTypes] ([EmailTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactEmail]
    ON [dbo].[Emails]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_EmailTypeContactEmail]
    ON [dbo].[Emails]([EmailTypeId] ASC);

