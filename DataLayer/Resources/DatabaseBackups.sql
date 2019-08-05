Use master

                            SET ANSI_WARNINGS OFF
                            SET NOCOUNT ON

                            --Create table for Backups and Space check verification
                            Create table #tDetails (
                            DBName  varchar(100),
                            TapeFull datetime null,
                            TapeDiff datetime null,
                            DiskFull datetime null,
                            DiskDiff datetime null,
                            DiskTLog datetime null,
                            [Recovery] varchar(20) null,
                            [Status] varchar(20) null,
                            [State] varchar(20) null
                            )

                            --Get information of Full Backups to Tape
                            Insert into #tDetails (DBName, TapeFull)
                            select distinct Name=d.name,LastBackup
                            from master.dbo.sysdatabases d
                            left outer  join 
                            (    select A.database_name,LastBackup=max(backup_finish_date) 
                                 from msdb.dbo.backupset A 
                                      left join msdb.dbo.backupmediafamily B 
                                      on A.media_set_id = B.media_set_id 
                                 where A.backup_finish_date <= getdate() 
                                 AND A.type='D'
                                 AND B.device_type  = 7
                                 group by A.database_name 
                            ) b 
                            on d.name = b.database_name
                            left join msdb.dbo.backupset c
                            on LastBackup=backup_finish_date
                            --where DB_ID(d.name) > 4
                            where d.name not like 'tempdb'
                            order by LastBackup, Name

                            --Get information of Differential Backups to Tape
                            select distinct Name=d.name,LastBackup
                            Into #tDiff
                            from master.dbo.sysdatabases d
                            left outer  join 
                            (    select A.database_name,LastBackup=max(backup_finish_date) 
                                 from msdb.dbo.backupset A 
                                      left join msdb.dbo.backupmediafamily B 
                                      on A.media_set_id = B.media_set_id 
                                 where A.backup_finish_date <= getdate() 
                                 AND A.type='I'
                                 AND B.device_type  = 7
                                 group by A.database_name 
                            ) b 
                            on d.name = b.database_name
                            left join msdb.dbo.backupset c
                            on LastBackup=backup_finish_date
                            where d.name not like 'tempdb'
                            order by LastBackup, Name


                            Update #tDetails
                            Set TapeDiff = LastBackup
                            from #tDiff 
                            where #tDetails.DBName =  #tDiff.Name

                            Drop table #tDiff


                            --Get information of Full Backups to Disk
                            select distinct Name=d.name,LastBackup
                            Into #tDiskFull
                            from master.dbo.sysdatabases d
                            left outer  join 
                            (    select A.database_name,LastBackup=max(backup_finish_date) 
                                 from msdb.dbo.backupset A 
                                      left join msdb.dbo.backupmediafamily B 
                                      on A.media_set_id = B.media_set_id 
                                 where A.backup_finish_date <= getdate() 
                                 AND A.type='D'
                                 AND B.device_type  = 2
                                 group by A.database_name 
                            ) b 
                            on d.name = b.database_name
                            left join msdb.dbo.backupset c
                            on LastBackup=backup_finish_date
                            where d.name not like 'tempdb'
                            order by LastBackup, Name

                            Update #tDetails
                            Set DiskFull = #tDiskFull.LastBackup
                            from #tDiskFull 
                            where #tDetails.DBName =  #tDiskFull.Name

                            Drop table #tDiskFull


                            --Get information of Differential Backups to Disk
                            select distinct Name=d.name,LastBackup
                            Into #tDiskDiff
                            from master.dbo.sysdatabases d
                            left outer  join 
                            (    select A.database_name,LastBackup=max(backup_finish_date) 
                                 from msdb.dbo.backupset A 
                                      left join msdb.dbo.backupmediafamily B 
                                      on A.media_set_id = B.media_set_id 
                                 where A.backup_finish_date <= getdate() 
                                 AND A.type='I'
                                 AND B.device_type  = 2
                                 group by A.database_name 
                            ) b 
                            on d.name = b.database_name
                            left join msdb.dbo.backupset c
                            on LastBackup=backup_finish_date
                            where d.name not like 'tempdb'
                            order by LastBackup, Name


                            Update #tDetails
                            Set DiskDiff = #tDiskDiff.LastBackup
                            from #tDiskDiff 
                            where #tDetails.DBName =  #tDiskDiff.Name

                            Drop table #tDiskDiff

                            --Get information of TLog Backups to Disk
                            select distinct Name=d.name,LastBackup
                            Into #tDiskTLog
                            from master.dbo.sysdatabases d
                            left outer  join 
                            (    select A.database_name,LastBackup=max(backup_finish_date) 
                                 from msdb.dbo.backupset A 
                                      left join msdb.dbo.backupmediafamily B 
                                      on A.media_set_id = B.media_set_id 
                                 where A.backup_finish_date <= getdate() 
                                 AND A.type='L'
                                 group by A.database_name 
                            ) b 
                            on d.name = b.database_name
                            left join msdb.dbo.backupset c
                            on LastBackup=backup_finish_date
                            where d.name not like 'tempdb'
                            order by LastBackup, Name

                            Update #tDetails
                            Set DiskTLog = #tDiskTLog.LastBackup
                            from #tDiskTLog 
                            where #tDetails.DBName =  #tDiskTLog.Name

                            Drop table #tDiskTLog


                            --Get information of Recovery and DB Status
                            SELECT name,  
                                   Convert(varchar(20),DATABASEPROPERTYEX(name, 'Recovery')) as RecoveryMode, 
                                   Convert(varchar(10),DATABASEPROPERTYEX(name, 'Status')) as [Status],
                                   Convert(varchar(20),DATABASEPROPERTYEX(name, 'Updateability')) as [State]
                            Into #tStatus
                            FROM   master.dbo.sysdatabases
                            where name not like 'tempdb'
                            ORDER BY 1 


                            Update #tDetails
                            Set [Recovery] = #tStatus.RecoveryMode,
	                            [Status] = #tStatus.[Status],
	                            [State] = #tStatus.[State]
                            from #tStatus
                            where #tDetails.DBName =  #tStatus.name

                            Drop table #tStatus

                            select * From #tDetails 
                            drop table #tDetails