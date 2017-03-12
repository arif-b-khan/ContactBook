CREATE TABLE [dbo].[Secrets] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [SecretKey]   NVARCHAR (200) NOT NULL,
    [SecretValue] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_CB_Secret] PRIMARY KEY CLUSTERED ([Id] ASC)
);

