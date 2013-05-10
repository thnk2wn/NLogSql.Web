USE [Chinook.Logs]
GO
/****** Object:  StoredProcedure [DBA].[WeekdayPartitionCleanup_PartitionSwitching]    Script Date: 3/6/2013 11:19:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [DBA].[WeekdayPartitionCleanup_PartitionSwitching] 

AS
BEGIN
	SET NOCOUNT ON;
	declare @partitionKey int
	declare @SQLCommand nvarchar(1024)
	truncate table [Chinook.Logs].dbo.LogEvent_Switched;

	set @partitionKey = datePart(weekday, getdate()) + 1
	if(@partitionkey >7)
		set @partitionKey = 1

	set @SQLCommand = 'alter table [Chinook.Logs].dbo.LogEvent switch partition  ' + cast(@partitionKey as varchar) + ' to [Chinook.Logs].dbo.LogEvent_Switched;'
	exec sp_executesql @SQLCommand
END



