use master

                            SET ANSI_WARNINGS OFF
                            SET NOCOUNT ON

                            CREATE TABLE #DBFileStats
                            (
                            [DBName] VARCHAR(128),
                            [DBID] INT,
                            [LogicalName] SYSNAME,
                            [FileID] INT,
                            [FileName] VARCHAR(255),
                            [FileGroup] VARCHAR(100),
                            [Size] VARCHAR(25),
                            [MaxSize] VARCHAR(25),
                            [Growth] VARCHAR(25),
                            [Usage] VARCHAR(25),
                            [TotalMB] DECIMAL(19,2),
                            [UsedMB] DECIMAL(19,2),
                            [FreeMB] AS CAST([TotalMB]-[UsedMB] AS DECIMAL(19,2)),
                            [Free%] AS CAST(([TotalMB]-[UsedMB])/[TotalMB]*100.00 AS DECIMAL(5,2)),
                            [ReadOnly] varchar (20),
                            [Status] varchar (20)
                            )

                            IF exists(SELECT * FROM tempdb..sysobjects WHERE [id] = OBJECT_ID('tempdb..#Stats'))
                            DROP TABLE #Stats

                            CREATE TABLE #Stats
                            (
							[DBId] INT,
                            [FileId] INT,
                            [FileGroup] INT,
                            [TotalExtents] DECIMAL(19,4),
                            [UsedExtents] DECIMAL(19,4),
                            [TotalMB] AS [TotalExtents]*64/1024,
                            [UsedMB] AS [UsedExtents]*64/1024,
                            [Name] SYSNAME,
                            [FileName] VARCHAR(255)
                            )

                            IF exists(SELECT * FROM tempdb..sysobjects WHERE [id] = OBJECT_ID('tempdb..#DBLogSpace'))
                            DROP TABLE #DBLogSpace

                            CREATE TABLE #DBLogSpace
                            (
                            [DBName] SYSNAME,
                            [LogSizeMB] DECIMAL(19,4),
                            [LogSpaceUsed%] DECIMAL(19,4),
                            [Status] INT
                            )

                            --Gather Stats for all databases
                            EXECUTE master.sys.sp_MSforeachdb
                            @command1 = 'use [?];INSERT into #Stats([FileId], [FileGroup], [TotalExtents], [UsedExtents], [Name], [FileName]) exec (''dbcc showfilestats with no_infomsgs'')',
                            @command2 = 'use [?];INSERT into #DBFileStats([LogicalName], [FileID], [FileName], [FileGroup], [Size], [MaxSize], [Growth], [Usage]) exec sp_helpfile;UPDATE #DBFileStats SET [DBName] = ''?'' WHERE DBName is Null;UPDATE #DBFileStats SET [DBID] = DB_ID(''?'') WHERE [DBID] is null; UPDATE #Stats SET [DBId] = DB_ID(''?'') WHERE [DBId] is null'

                            --Gather log usage stats
                            INSERT #DBLogSpace EXEC ('dbcc sqlperf(logspace) with no_infomsgs')

                            --Merge file stats with database info
                            UPDATE #DBFileStats SET TotalMB = fs.TotalMB, UsedMB = fs.UsedMB
                            FROM #DBFileStats hf INNER join #Stats fs 
							ON hf.DBID = fs.DBId
							where hf.[FileID] = fs.[FileId]
                            and hf.[TotalMB] is null

                            UPDATE #DBFileStats SET TotalMB = ls.LogSizeMB, UsedMB = ls.LogSizeMB*ls.[LogSpaceUsed%]/100
                            FROM #DBLogSpace ls INNER join #DBFileStats hf ON hf.[DBName] = ls.[DBName]
                            WHERE hf.[FileGroup] is null

                            --SELECT [DBName], [LogicalName], [TotalMB], [FreeMB], [Free%], [Growth], [Usage] 
                            --FROM #DBFileStats
                            --ORDER BY [Free%],[DBName] ASC, [USAGE] ASC, [LogicalName] ASC


                            Select name, convert(varchar(10),DATABASEPROPERTYEX(name, 'Updateability')) as [ReadOnly],
                            Convert(varchar(10),DATABASEPROPERTYEX(name, 'Status')) as [Status]
                                   --Convert(varchar(10),DATABASEPROPERTYEX(name, 'Status')) as [Status]
                            Into #tStatus
                            FROM   master.dbo.sysdatabases
                            ORDER BY 1 

                            Update  #DBFileStats
                            Set [ReadOnly] = #tStatus.[ReadOnly],
                            [Status] = #tStatus.[Status]
                            from #tStatus
                            where #DBFileStats.DBName =  #tStatus.name


                            Select DBName,Sum(TotalMB) as DBSize, SUM(UsedMB) as UsedMB, SUM(TotalMB)-SUM(UsedMB) as FreeMB,
                            --[Free%] AS CAST(([TotalMB]-[UsedMB])/[TotalMB]*100.00 AS DECIMAL(4,2)) 
                            [Free] =  CAST((Sum(TotalMB)-Sum(UsedMB))/Sum(TotalMB)*100.00 AS DECIMAL(5,2)), ReadOnly, Status
                            from #DBFileStats
                            where [Usage] = 'data only'
                            --and DBName not in ('model','master','tempdb','msdb')
                            Group By DBID, DBName, ReadOnly, Status
                            Order by 1

                            --Clean up
                            DROP TABLE #Stats
                            DROP TABLE #DBLogSpace
                            DROP TABLE #DBFileStats
                            Drop Table #tStatus