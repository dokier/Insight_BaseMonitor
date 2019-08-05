Use master
                            SET ANSI_WARNINGS OFF
                            SET NOCOUNT ON


                            --Create table for Backups and Space check verification
                            Create table #tDetails (
                            DBName  varchar(100),
                            DBSizeMB float null,
                            DBGrowth varchar(10) null,
                            AvailableSpaceMB float null
                            ) 



                            --Get information of Space Used By DB
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
                            [Growth] VARCHAR(50),
                            [Usage] VARCHAR(25),
                            [TotalMB] DECIMAL(38,2),
                            [UsedMB] DECIMAL(38,2),
                            [FreeMB] AS CAST([TotalMB]-[UsedMB] AS DECIMAL(38,2)),
                            [Free%] AS CAST(([TotalMB]-[UsedMB])/[TotalMB]*100.00 AS DECIMAL(38,2)),
                            [Used%] AS CAST(([UsedMB]*100)/[TotalMB] as DECIMAL(38,2))
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

                            /*SELECT [DBName], [LogicalName], [TotalMB], [FreeMB], [Free%], [Growth], [Usage] 
                            FROM #DBFileStats
                            ORDER BY [Free%],[DBName] ASC, [USAGE] ASC, [LogicalName] ASC */


                            --Get TotalMB per Database
                            Update #tDetails 
                            Set DBSizeMB = #DBFileStats.TotalMB 
                            From #DBFileStats
                            where #tDetails.DBName = #DBFileStats.DBName 


                            --Get Growth per Database
                            Update #tDetails
                            Set DBGrowth = #DBFileStats.Growth 
                            From #DBFileStats 
                            where #tDetails.DBName = #DBFileStats.DBName 

                            --Get Free Space available in Disk per database based in datafiles
                            Select A.name,LEFT(filename,1) as DriveLetter
                            Into #tDataFiles 
                            from sysaltfiles B inner join sys.databases A on B.dbid =  A.database_id


                            select fs.name ,   
                            CASE 
                                                    WHEN (fs.status & 0x100000 = 0 AND CEILING((fs.growth * 8192.0) / (1024.0 * 1024.0)) = 0.00) OR fs.growth = 0 THEN 'None'
                                                    WHEN fs.status & 0x100000 = 0 THEN 'By ' + CONVERT(VARCHAR,CEILING((fs.growth * 8192.0) / (1024.0 * 1024.0))) + ' MB'
                                                    ELSE 'By ' + CONVERT(VARCHAR,growth) + ' percent'
                                            END 
                                            + 
                                            CASE 
                                                    WHEN (fs.status & 0x100000 = 0 AND CEILING((fs.growth * 8192.0) / (1024.0 * 1024.0)) = 0.00) OR fs.growth = 0 THEN ''
                                                    WHEN CAST([maxsize] * 8.0 / 1024 AS DEC(20,2)) <= 0.00 THEN ', unrestricted growth'
                                                    ELSE ', restricted growth to ' + CAST(CAST([maxsize] * 8.0 / 1024 AS DEC(20)) AS VARCHAR) + ' MB'
                                            END AS 'GROWTH'
                                            Into #Growth
                                            FROM master..sysaltfiles fs JOIN master..sysdatabases db ON fs.dbid = db.dbid

                
                            Update #DBFileStats
                            Set #DBFileStats.Growth = g.GROWTH
                            From #Growth g
                            where #DBFileStats.LogicalName = g.name 

                            SELECT DBName as DBName, 
                                   LogicalName as LogicalName,
                                  [Usage] as [File Type],
                                   CEILING([TotalMB]) as [Total Space(MB)], 
                                   UsedMB as [Used Space MB],
                                   [FreeMB] as [Free Space(MB)], 
                                   [Used%],
                                   FileName as [PhysicalName], 
	                               [FileGroup],
                                   Growth as [File Growth],
                                   case when Growth != 'None' then 'Enabled'
			                            else 'Disabled'
	                               end	as [Auto Grow] 
                            FROM #DBFileStats
                            --where DBName not in ('master','model','msdb','tempdb')
                            ORDER BY DBName, [Usage]

		
                            DROP TABLE #Stats
                            DROP TABLE #DBLogSpace
                            DROP TABLE #DBFileStats
                            Drop table #tDetails
                            drop table #tDataFiles 
                            drop table #Growth