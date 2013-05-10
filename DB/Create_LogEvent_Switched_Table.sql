USE [Chinook.Logs]
GO

/****** Object:  Table [dbo].[LogEvent]    Script Date: 3/6/2013 9:52:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LogEvent_Switched](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NOT NULL,
	[EventLevel] [nvarchar](50) NOT NULL,
	[LoggerName] [nvarchar](500) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Url] [nvarchar](1024) NULL,
	[MachineName] [nvarchar](100) NOT NULL,
	[SessionId] [nvarchar](100) NULL,
	[ThreadId] [int] NULL,
	[Referrer] [nvarchar](1024) NULL,
	[UserAgent] [nvarchar](500) NULL,
	[Code] [nvarchar](10) NULL,
	[LogMessage] [nvarchar](max) NOT NULL,
	[PartitionKey] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC,
	[PartitionKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

SET ANSI_PADDING OFF
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_LogEvent_EventLevel]    Script Date: 3/6/2013 9:52:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_LogEvent_EventLevel] ON [dbo].[LogEvent]
(
	[EventLevel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80)
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_LogEvent_URL]    Script Date: 3/6/2013 9:52:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_LogEvent_URL] ON [dbo].[LogEvent]
(
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80)
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_LogEvent_UserName]    Script Date: 3/6/2013 9:52:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_LogEvent_UserName] ON [dbo].[LogEvent]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80)
GO


