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

                            DECLARE @output table (powerplan varchar(255))
                            DECLARE @sql varchar(400) 

                            SET @sql = 'powershell.exe -c ""(Get-WmiObject -Class Win32_PowerPlan -Namespace root\cimv2\power -Filter ""IsActive=''true''"").ElementName""'

                            SET NOCOUNT ON
                            insert into @output
                            EXEC xp_cmdshell @sql 

                            IF(@@ROWCOUNT > 2)
							Select 'FAILED' as powerplan
							ELSE                            
                            select powerplan from @output
                            where powerplan is not null

                            --Turn off 'xp_cmdshell'
                            IF (@isCmdShellEnabled = 0)
                            BEGIN
	                            EXEC sp_configure 'xp_cmdshell', 0;
	                            RECONFIGURE WITH OVERRIDE;
                            END;

                            --Turn off 'show advanced options'
                            IF (@isShowAdvanced = 0)
                            BEGIN
	                            EXEC sp_configure 'show advanced options', 0;
	                            RECONFIGURE WITH OVERRIDE;
                            END;