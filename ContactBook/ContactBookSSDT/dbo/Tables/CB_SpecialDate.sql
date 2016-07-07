CREATE TABLE [dbo].[CB_SpecialDate] (
    [SpecialDateId]   INT      IDENTITY (1, 1) NOT NULL,
    [ContactId]       BIGINT   NOT NULL,
    [Dates]           DATETIME NOT NULL,
    [SpecialDateTpId] INT      NOT NULL,
    CONSTRAINT [PK_CB_SpecialDate] PRIMARY KEY CLUSTERED ([SpecialDateId] ASC),
    CONSTRAINT [FK_ContactBookContactSpecialDates] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[CB_Contact] ([ContactId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Custom_SpecialDateTypeContactSpecialDates] FOREIGN KEY ([SpecialDateTpId]) REFERENCES [dbo].[CB_SpecialDateType] ([SpecialDateTpId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ContactBookContactSpecialDates]
    ON [dbo].[CB_SpecialDate]([ContactId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Custom_SpecialDateTypeContactSpecialDates]
    ON [dbo].[CB_SpecialDate]([SpecialDateTpId] ASC);

