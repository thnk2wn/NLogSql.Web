USE [Chinook.Logs]
GO

/****** Object:  StoredProcedure [dbo].[sLogEvent_Insert]    Script Date: 3/6/2013 9:58:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sLogEvent_Insert]
	@time_stamp datetime,
	@level nvarchar(50),
	@logger nvarchar(500),
	@userName nvarchar(50),
	@url nvarchar(1024),
	@machineName nvarchar(100),
	@sessionId nvarchar(100),
	@threadId int,
	@referrer nvarchar(1024),
	@userAgent nvarchar(500),
	@code nvarchar(10),
	@message nvarchar(max)
AS
BEGIN

	SET NOCOUNT ON;
	Declare @currentDate Datetime
	declare @partitionKey tinyint
	
	set		@currentDate = getdate()
	set		@partitionKey = DATEPART(weekday, @currentDate)

	
	INSERT INTO [dbo].[LogEvent]
           ([LogDate]
           ,[EventLevel]
           ,[LoggerName]
           ,[UserName]
           ,[Url]
           ,[MachineName]
           ,[SessionId]
           ,[ThreadId]
           ,[Referrer]
           ,[UserAgent]
		   ,[Code]
           ,[LogMessage]
		   ,[PartitionKey])
     VALUES
           (@time_stamp
           ,@level
           ,@logger
           ,@userName
           ,@url
           ,@machineName
           ,@sessionId
           ,@threadId
           ,@referrer
           ,@userAgent
		   ,@code
           ,@message
		   ,@partitionKey);
END




GO

