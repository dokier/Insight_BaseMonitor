SELECT  
                            SERVERPROPERTY('MachineName') AS ComputerName,
                            SERVERPROPERTY('ServerName') AS InstanceName,  
                            SERVERPROPERTY('Edition') AS Edition,
                            SERVERPROPERTY('ProductVersion') AS ProductVersion,  
                            SERVERPROPERTY('ProductLevel') AS ProductLevel,
                            SERVERPROPERTY('IsIntegratedSecurityOnly') as AuthMode,
                            create_date as SQLInstallDate
                            FROM sys.server_principals
                            WHERE sid = 0x010100000000000512000000