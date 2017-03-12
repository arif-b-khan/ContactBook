CREATE TABLE [dbo].[SpecialDates] (
    [SpecialDateId]   INT      IDENTITY (1, 1) NOT NULL,
    [ContactId]       BIGINT   NOT NULL,
    [Dates]           DATETIME NOT NULL,
    [SpecialDateTpId] INT      NOT NULL,
    CONSTRAINT [PK_CB_SpecialDate] PRIMARY KEY CLUSTERED ([SpecialDateId] ASC),
    CONSTRAINT [FK_ContactBookContactSpecialDates] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_SpecialDateTypeContactSpecialDates] FOREIGN KEY ([SpecialDateTpId]) REFERENCES [dbo].[SpecialDateTypes] ([SpecialDateTpId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactSpecialDates]
    ON [dbo].[SpecialDates]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_SpecialDateTypeContactSpecialDates]
    ON [dbo].[SpecialDates]([SpecialDateTpId] ASC);

