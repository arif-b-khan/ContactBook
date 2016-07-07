CREATE TABLE [dbo].[CB_GroupType] (
    [GroupId]        INT            IDENTITY (1, 1) NOT NULL,
    [Group_TypeName] NVARCHAR (100) NOT NULL,
    [BookId]         BIGINT         NULL,
    CONSTRAINT [PK_CB_GroupType] PRIMARY KEY CLUSTERED ([GroupId] ASC),
    CONSTRAINT [FK_CB_ContactBookCB_Group] FOREIGN KEY ([BookId]) REFERENCES [dbo].[CB_ContactBook] ([BookId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CB_ContactBookCB_Group]
    ON [dbo].[CB_GroupType]([BookId] ASC);

