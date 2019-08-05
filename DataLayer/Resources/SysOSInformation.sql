declare @sql nvarchar(max)
							declare @version decimal(5,3)
							declare @ProductLevel char(3)
							select @ProductLevel = cast(serverproperty('ProductLevel') as char(3))
							select @version = cast(left(cast(serverproperty('productversion') as varchar), 4) as decimal(5, 3))
                            if @version > 10.500 
                            set @sql = N'select cpu_count, physical_memory_kb / 1024 as ''Memory (MB)'', sqlserver_start_time, virtual_machine_type_desc from sys.dm_os_sys_info'
                            else if (@version = 10.500 and @ProductLevel != 'RTM')
                            set @sql = N'select cpu_count, physical_memory_in_bytes / 1024 / 1024 as ''Memory (MB)'', sqlserver_start_time, virtual_machine_type_desc from sys.dm_os_sys_info'
							else if (@version = 10.500 and @ProductLevel = 'RTM')
                            set @sql = N'select cpu_count, physical_memory_in_bytes / 1024 / 1024 as ''Memory (MB)'', sqlserver_start_time, null as virtual_machine_type_desc from sys.dm_os_sys_info'
							else if @version = 10.000
							set @sql = N'select cpu_count, physical_memory_in_bytes / 1024 / 1024 as ''Memory (MB)'', sqlserver_start_time, null as virtual_machine_type_desc from sys.dm_os_sys_info'
							else 
							set @sql = N'select cpu_count, physical_memory_in_bytes / 1024 / 1024 as ''Memory (MB)'', null as sqlserver_start_time, null as virtual_machine_type_desc from sys.dm_os_sys_info'
                            exec sp_executesql @sql