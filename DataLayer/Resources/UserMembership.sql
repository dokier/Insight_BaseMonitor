use master
                            CREATE TABLE #temp (
                            DBName Nvarchar(100),
                            UserName NVARCHAR(100),
                            GroupName NVARCHAR(100),
                            LoginName NVARCHAR(100),
                            DefDBName nvarchar(100),
                            DefSchemaName nvarchar(100),
                            UserID char(10)
                            )


                            --create statement to run on each database
                            declare @sql nvarchar(800)
							SET @sql='select  ''?'' as DBName,
							u.name  
							,''GroupName'' = case  when (r.principal_id is null) then ''public''  
							else r.name  
							end  
							,COALESCE (l.name,''***ORPHANED***'') as LoginName
							,l.default_database_name as DefDBname 
							,u.default_schema_name as DefSchemaName
							,u.principal_id  as UserID
							from [?].sys.database_principals u  
							left join ([?].sys.database_role_members m join [?].sys.database_principals r on m.role_principal_id = r.principal_id) on m.member_principal_id = u.principal_id  
							left join [?].sys.server_principals l on u.sid = l.sid  
							where u.type not in(''R'',''C'')
							and u.principal_id > 4'
                            --insert the results from each database to temp table
                            INSERT INTO #temp
                            EXECUTE master.sys.sp_MSforeachdb @sql
                            --return results
                            delete from #temp
                            where DBName in ('master', 'tempdb', 'msdb', 'model')
                            SELECT * FROM #temp
                            order by DBName
                            DROP TABLE #temp