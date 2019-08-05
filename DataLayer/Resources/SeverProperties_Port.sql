select net_transport, auth_scheme, local_tcp_port 
from sys.dm_exec_connections
where session_id = @@SPID