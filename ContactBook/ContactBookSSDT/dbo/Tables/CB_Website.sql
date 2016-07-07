CREATE TABLE [dbo].[CB_Website] (
    [WebsiteId] INT             IDENTITY (1, 1) NOT NULL,
    [Website]   NVARCHAR (1000) NULL,
	[Description] NVARCHAR (1000) NULL,
    [ContactId] BIGINT          NOT NULL,
    CONSTRAINT [PK_CB_Website] PRIMARY KEY CLUSTERED ([WebsiteId] ASC),
    CONSTRAINT [FK_ContactBookContactWebsite] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactWebsite]
    ON [dbo].[CB_Website]([ContactId] ASC);

