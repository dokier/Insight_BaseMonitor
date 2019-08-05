USE [master]
                            DECLARE @isCmdShellEnabled BIT;
                            DECLARE @isShowAdvanced BIT;

                            SELECT @isCmdShellEnabled = CAST(value AS BIT) FROM sys.configurations
                            WHERE name = 'xp_cmdshell';

                            SELECT @isShowAdvanced = CAST(value AS BIT) FROM sys.configurations
                            WHERE name = 'show advanced options';

                            IF (@isShowAdvanced = 0)
                            BEGIN
	                            EXEC sp_configure 'show advanced options', 1;
	                            RECONFIGURE WITH OVERRIDE;
                            END;

                            IF (@isCmdShellEnabled = 0)
                            BEGIN
	                            EXEC sp_configure 'xp_cmdshell', 1;
	                            RECONFIGURE WITH OVERRIDE;
                            END;

                            DECLARE @sql varchar(400) 
                            --by default it will take the current server name, we can the set the server name as well 
                            --SET @svrName = @@SERVERNAME 
                            SET @sql = 'powershell.exe -c "Get-WmiObject -Class Win32_Volume -Filter ''DriveType = 3'' | select name , capacity , freespace | foreach{$_.name+''|''+$_.capacity/1048576+''%''+$_.freespace/1048576+''*''}"'

                            --creating a temporary table
                            CREATE TABLE #output 
                            (line varchar(255)) 
                            --inserting disk name, total space and free space value in to temporary table
                            insert #output 
                            EXEC xp_cmdshell @sql

                            --script to retrieve the values in GB from PS Script output
                            select rtrim(ltrim(SUBSTRING(line, 1, CHARINDEX('|', line) - 1))) as drivename
                            ,round(cast(rtrim(ltrim(SUBSTRING(line, CHARINDEX('|', line) + 1,
                            (CHARINDEX('%', line) - 1) - CHARINDEX('|', line)))) as Float) / 1024, 0) as 'capacityGB'
                            ,round(cast(rtrim(ltrim(SUBSTRING(line, CHARINDEX('%', line) + 1,
                            (CHARINDEX('*', line) - 1) - CHARINDEX('%', line)))) as Float) / 1024, 0) as 'freespaceGB', 
                            round(cast(rtrim(ltrim(SUBSTRING(line, CHARINDEX('|', line) + 1,
                            (CHARINDEX('%', line) - 1) - CHARINDEX('|', line)))) as Float) / 1024, 0) -
                            round(cast(rtrim(ltrim(SUBSTRING(line, CHARINDEX('%', line) + 1,
                            (CHARINDEX('*', line) - 1) - CHARINDEX('%', line)))) as Float) / 1024, 0) as 'usedspaceGB',
                            round(100 * (round(cast(rtrim(ltrim(SUBSTRING(line, CHARINDEX('%', line) + 1,
                            (CHARINDEX('*', line) - 1) - CHARINDEX('%', line)))) as Float) / 1024, 0)) /
                            (round(cast(rtrim(ltrim(SUBSTRING(line, CHARINDEX('|', line) + 1,
                            (CHARINDEX('%', line) - 1) - CHARINDEX('|', line)))) as Float) / 1024, 0)), 0) as percentfree
                            from #output 
                            --select* from #output 
                            where line like '[A-Z][:]%'
                            order by drivename
                            --script to drop the temporary table
                            drop table #output

                             --Turn off 'xp_cmdshell'
                             IF(@isCmdShellEnabled = 0)
                            BEGIN
                            EXEC sp_configure 'xp_cmdshell', 0;
                                        RECONFIGURE WITH OVERRIDE;
                                        END;

                                        --Turn off 'show advanced options'
                            IF(@isShowAdvanced = 0)
                            BEGIN
                            EXEC sp_configure 'show advanced options', 0;
                                        RECONFIGURE WITH OVERRIDE;
                                        END;