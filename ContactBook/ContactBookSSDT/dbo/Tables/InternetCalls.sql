CREATE TABLE [dbo].[InternetCalls] (
    [InternetCallId]     INT            IDENTITY (1, 1) NOT NULL,
    [InternetCallNumber] NVARCHAR (100) NOT NULL,
    [ContactId]          BIGINT         NOT NULL,
    CONSTRAINT [PK_CB_InternetCall] PRIMARY KEY CLUSTERED ([InternetCallId] ASC),
    CONSTRAINT [FK_ContactBookContactInternetCall] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactInternetCall]
    ON [dbo].[InternetCalls]([ContactId] ASC);

