CREATE TABLE [dbo].[CB_Log] (
    [LoggingId]   UNIQUEIDENTIFIER CONSTRAINT [DF_Logging_LoggingId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Date]        DATETIME         CONSTRAINT [DF_Logging_date] DEFAULT (getdate()) NULL,
    [Application] VARCHAR (200)    NULL,
    [Level]       VARCHAR (100)    NULL,
    [Logger]      VARCHAR (8000)   NULL,
    [Message]     VARCHAR (8000)   NULL,
    [MachineName] VARCHAR (8000)   NULL,
    [UserName]    VARCHAR (8000)   NULL,
    [CallSite]    VARCHAR (8000)   NULL,
    [Thread]      VARCHAR (100)    NULL,
    [Exception]   VARCHAR (8000)   NULL,
    [Stacktrace]  VARCHAR (8000)   NULL,
    CONSTRAINT [PK_logging] PRIMARY KEY CLUSTERED ([LoggingId] ASC)
);

