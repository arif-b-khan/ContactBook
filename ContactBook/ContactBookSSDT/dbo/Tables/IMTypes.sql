CREATE TABLE [dbo].[IMTypes] (
    [IMTypeId]    INT             IDENTITY (1, 1) NOT NULL,
    [IM_TypeName] NVARCHAR (30)   NOT NULL,
    [IMLogoPath]  NVARCHAR (2000) NULL,
    [BookId]      BIGINT          NULL,
    CONSTRAINT [PK_CB_IMType] PRIMARY KEY CLUSTERED ([IMTypeId] ASC),
    CONSTRAINT [FK_CB_ContactBookCB_IMType] FOREIGN KEY ([BookId]) REFERENCES [dbo].[ContactBooks] ([BookId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CB_ContactBookCB_IMType]
    ON [dbo].[IMTypes]([BookId] ASC);

