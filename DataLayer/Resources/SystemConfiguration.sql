SELECT MAX([max server memory (MB)]) [max server memory (MB)],
                            MAX([min server memory (MB)]) [min server memory (MB)],
                            MAX([backup compression default]) [backup compression default],
                            MAX([max degree of parallelism]) [max degree of parallelism], 
                            MAX([xp_cmdshell]) [xp_cmdshell]
                            FROM (
	                            select * from sys.configurations
	                            where name in (
                            'max server memory (MB)',
                            'min server memory (MB)',
                            'backup compression default',
                            'max degree of parallelism',
                            'xp_cmdshell')
                            ) X
                            PIVOT
                            (
                            MAX(value) FOR [name] IN ([max server memory (MB)],[min server memory (MB)],[backup compression default],[max degree of parallelism],[xp_cmdshell])
                            ) pvt