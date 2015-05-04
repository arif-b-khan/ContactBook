USE [ContactBook]
GO

SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
SET ANSI_PADDING ON
GO
IF OBJECT_ID(N'[dbo].[CB_Log]', 'U') IS NOT NULL
BEGIN
DROP TABLE CB_Log
END
GO
CREATE TABLE [dbo].[CB_Log](
	[LoggingId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Date] [datetime] NULL,
	[Application] [varchar](200) NULL,
	[Level] [varchar](100) NULL,
	[Logger] [varchar](8000) NULL,
	[Message] [varchar](8000) NULL,
	[MachineName] [varchar](8000) NULL,
	[UserName] [varchar](8000) NULL,
	[CallSite] [varchar](8000) NULL,
	[Thread] [varchar](100) NULL,
	[Exception] [varchar](8000) NULL,
	[Stacktrace] [varchar](8000) NULL,
 CONSTRAINT [PK_logging] PRIMARY KEY CLUSTERED 
(
	[LoggingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
 
GO
 
SET ANSI_PADDING OFF
GO
 
ALTER TABLE [dbo].[CB_Log] ADD  CONSTRAINT [DF_Logging_LoggingId]  DEFAULT (newid()) FOR [LoggingId]
GO
 
ALTER TABLE [dbo].[CB_Log] ADD  CONSTRAINT [DF_Logging_date]  DEFAULT (getdate()) FOR [Date]
GO