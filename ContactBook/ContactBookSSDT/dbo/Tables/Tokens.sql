CREATE TABLE [dbo].[Tokens] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (100)   NULL,
    [Token]      NVARCHAR (500)   NULL,
    [TokenType]  NVARCHAR (MAX)   NULL,
    [Identifier] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CB_Tokens] PRIMARY KEY CLUSTERED ([Id] ASC)
);

