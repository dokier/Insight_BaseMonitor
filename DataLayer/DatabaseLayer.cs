using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataLayer.HelperClasses;
using Insight_BL;
using DataLayer.Properties;

namespace DataLayer
{
    public class DatabaseLayer
    {

        public string ConnString;

        public string CreateConnectionString(string InstanceName)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = InstanceName,
                InitialCatalog = "master",
                IntegratedSecurity = false,
                //MultipleActiveResultSets = false,
                //PersistSecurityInfo = true,
                UserID = "SQL_USER",
                Password = "PASSWORD"
            };

            return builder.ConnectionString;
        }

        public Object ExecuteQuery(string Query)
        {
            try
            {
                using (SqlConnection objsqlconn = new SqlConnection(ConnString))
                {
                    string setOptions = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED SET ARITHABORT ON ";
                    objsqlconn.Open();
                    DataSet ds = new DataSet();
                    SqlCommand objcmd = new SqlCommand(setOptions + Query, objsqlconn);
                    objcmd.CommandTimeout = 60;
                    SqlDataAdapter objAdp = new SqlDataAdapter(objcmd);
                    objAdp.Fill(ds);
                    return ds;
                }
            }
            catch(SqlException)
            {
                throw;
            }
        }

        public ServerProperty GetServerProperties()
        {
            ServerProperty SP = new ServerProperty();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            string query = Resources.ServerProperties;

            string query2 = Resources.SeverProperties_Port;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                ds2 = (DataSet)ExecuteQuery(query2);
                SP.ComputerName = (String)ds.Tables["Table"].Rows[0]["ComputerName"];
                SP.InstanceName = (String)ds.Tables["Table"].Rows[0]["InstanceName"];
                SP.Edition = (String)ds.Tables["Table"].Rows[0]["Edition"];
                SP.ProductVersion = (String)ds.Tables["Table"].Rows[0]["ProductVersion"];
                SP.ProductLevel = (String)ds.Tables["Table"].Rows[0]["ProductLevel"];
                SP.AuthMode = Convert.ToBoolean(ds.Tables["Table"].Rows[0]["AuthMode"]);
                SP.SQLInstallDate = (DateTime)ds.Tables["Table"].Rows[0]["SQLInstallDate"];
                SP.LocalTcpPort = (Int32)ds2.Tables["Table"].Rows[0]["local_tcp_port"];
                SP.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine ("Exception catched: " + ex.Message);
                SP.Success = false;
            }

            return SP;
        }

        public SysConfig GetSysConfiguration()
        {
            SysConfig SC = new SysConfig();
            DataSet ds = new DataSet();
            string query = Resources.SystemConfiguration;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                SC.MaxServerMemory = (Int32)ds.Tables["Table"].Rows[0]["max server memory (MB)"];
                SC.MinServerMemory = (Int32)ds.Tables["Table"].Rows[0]["min server memory (MB)"];
               //SC.BackupCompression = Convert.ToBoolean(ds.Tables["Table"].Rows[0]["backup compression default"]);
                //SC.BackupCompression = ds.Tables["Table"].Rows[0]["max degree of parallelism"] == DBNull.Value ? null : Convert.ToBoolean(ds.Tables["Table"].Rows[0]["backup compression default"]);
                SC.BackupCompression = ds.Tables["Table"].Rows[0]["backup compression default"] == DBNull.Value ? false : Convert.ToBoolean(ds.Tables["Table"].Rows[0]["backup compression default"]);
                SC.MaxDop = Convert.ToInt16(ds.Tables["Table"].Rows[0]["max degree of parallelism"]);
                SC.Xp_Cmdshell = Convert.ToBoolean(ds.Tables["Table"].Rows[0]["xp_cmdshell"]);
                SC.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                SC.Success = false;
            }

            return SC;
        }

        public SysOsInfo GetSysOSInformation()
        {
            SysOsInfo SI = new SysOsInfo();
            DataSet ds = new DataSet();
            string query = Resources.SysOSInformation;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                SI.CpuCount = (Int32)ds.Tables["Table"].Rows[0]["cpu_count"];
                SI.ServerMemory = (Int64)ds.Tables["Table"].Rows[0]["Memory (MB)"];
                // SI.SQLStartTime = (DateTime)ds.Tables["Table"].Rows[0]["sqlserver_start_time"];
                SI.SQLStartTime = ds.Tables["Table"].Rows[0]["sqlserver_start_time"] == DBNull.Value ? null : (DateTime?)ds.Tables["Table"].Rows[0]?["sqlserver_start_time"];
                //SI.MachineType = (String)ds.Tables["Table"].Rows[0]["virtual_machine_type_desc"];
                SI.MachineType = ds.Tables["Table"].Rows[0]["virtual_machine_type_desc"] == DBNull.Value ? null : (String)ds.Tables?["Table"].Rows[0]["virtual_machine_type_desc"];
                SI.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                SI.Success = false;
            }

            return SI;
        }

        public PowerPlan GetPowerPlan()
        {
            PowerPlan PP = new PowerPlan();
            DataSet ds = new DataSet();
            string query = Resources.PowerPlan;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                PP.powerPlan = (String)ds.Tables["Table"].Rows[0]["powerplan"];
                PP.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                PP.Success = false;
            }

            return PP;
        }
        public List<DatabaseOption> GetDatabaseOptions()
        {
            var databaseList = new List<DatabaseOption>();
            DataSet ds = new DataSet();
            string query = Resources.DatabaseOptions;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        DatabaseOption DO = new DatabaseOption();
                        DO.Name= (String)r["name"];
                        DO.Compatibility = Convert.ToInt16(r["compatibility_level"]);
                        DO.StateDesc = (String)r["state_desc"];
                        DO.UserAccessDesc = (String)r["user_access_desc"];
                        DO.RecoveryModel = (String)r["recovery_model_desc"];
                       // DO.Collation = (String)r["collation_name"];
                        DO.Collation = r["collation_name"] == DBNull.Value ? null : (String)r?["collation_name"];
                        DO.PageVerify = (String)r["page_verify_option_desc"];
                        DO.ReadOnly = Convert.ToBoolean(r["is_read_only"]);
                        DO.AutoClose = Convert.ToBoolean(r["is_auto_close_on"]);
                        DO.AutoShrink = Convert.ToBoolean(r["is_auto_shrink_on"]);
                        DO.AutoCreateStats = Convert.ToBoolean(r["is_auto_create_stats_on"]);
                        DO.AutoUpdateStats = Convert.ToBoolean(r["is_auto_update_stats_on"]);
                        DO.FullText = Convert.ToBoolean(r["is_fulltext_enabled"]);
                        DO.DbChaining = Convert.ToBoolean(r["is_db_chaining_on"]);
                        DO.Trustworthy = Convert.ToBoolean(r["is_trustworthy_on"]);
                        DO.Owner = (String)r["Owner"];

                        databaseList.Add(DO);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return databaseList;
        }
        public List<Backup> GetBackups()
        {
            
            var backupList = new List<Backup>();
            DataSet ds = new DataSet();
            string query = Resources.DatabaseBackups;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        Backup B = new Backup();
                        B.DBName = (String)r["DBname"];
                        B.TapeFull = r["TapeFull"] == DBNull.Value ? null : (DateTime?)r["TapeFull"];
                        B.TapeDiff = r["TapeDiff"] == DBNull.Value ? null : (DateTime?)r["TapeDiff"];
                        B.DiskFull = r["DiskFull"] == DBNull.Value ? null : (DateTime?)r["DiskFull"];
                        B.DiskDiff = r["DiskDiff"] == DBNull.Value ? null : (DateTime?)r["DiskDiff"];
                        B.DiskTlog = r["DiskTlog"] == DBNull.Value ? null : (DateTime?)r["DiskTlog"];
                        B.RecoveryModel = (String)r["Recovery"];
                        B.StateDesc = (String)r["Status"];
                        B.Updateability = (String)r["State"];
                        backupList.Add(B);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return backupList;
        }
        public List<Drive> GetDrives()
        {
            var driveList = new List<Drive>();
            DataSet ds = new DataSet();
            string query = Resources.DriveSpace;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        Drive D = new Drive();
                        D.DriveName = (String)r["drivename"];
                        D.Capacity_GB = Convert.ToInt32(r["capacityGB"]);
                        D.Used_GB = Convert.ToInt32(r["usedspaceGB"]);
                        D.Free_GB = Convert.ToInt32(r["freespaceGB"]);
                        D.PercentFree = Convert.ToInt32(r["percentfree"]);
                        driveList.Add(D);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return driveList;
        }
        public List<DatabaseFile> GetDatabaseFiles()
        {

            var databasefileList = new List<DatabaseFile>();
            DataSet ds = new DataSet();
            string query = Resources.DatabaseFiles;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        DatabaseFile DBF = new DatabaseFile();
                        DBF.DBName = (String)r["DBname"];
                        DBF.LogicalName = (String)r["LogicalName"];
                        DBF.Type = (String)r["File Type"];
                        DBF.Total_Space_MB = Convert.ToInt32(r["Total Space(MB)"]);
                        DBF.Used_Space_MB = Convert.ToDecimal(r["Used Space MB"]);
                        DBF.Free_Space_MB = Convert.ToDecimal(r["Free Space(MB)"]);
                        DBF.PercentUsed = Convert.ToDecimal(r["Used%"]);
                        DBF.PhysicalName = (String)r["PhysicalName"];
                        DBF.FileGroup = r["FileGroup"] == DBNull.Value ? null : (String)r?["FileGroup"];
                        DBF.FileGrowth = (String)r["File Growth"];
                        DBF.AutoGrowth = (String)r["Auto Grow"];
                        databasefileList.Add(DBF);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return databasefileList;
        }
        public List<DatabaseSize> GetDatabaseSize()
        {

            var databasesizeList = new List<DatabaseSize>();
            DataSet ds = new DataSet();
            string query = Resources.DatabaseSize;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        DatabaseSize DBS = new DatabaseSize();
                        DBS.Name = (String)r["DBname"];
                        DBS.Size = Convert.ToDecimal(r["DBSize"]);
                        DBS.Used_Space_MB = Convert.ToDecimal(r["UsedMB"]);
                        DBS.Free_Space_MB = Convert.ToDecimal(r["FreeMB"]);
                        DBS.PercentFree = Convert.ToDecimal(r["Free"]);
                        DBS.Updateability = (String)r["ReadOnly"];
                        DBS.StateDesc = (String)r["Status"];
                        databasesizeList.Add(DBS);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return databasesizeList;
        }
        public List<UserMembership> GetUserMembership()
        {

            var usersList = new List<UserMembership>();
            DataSet ds = new DataSet();
            string query = Resources.UserMembership;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        UserMembership UM = new UserMembership();
                        UM.DBName = (String)r["DBname"];
                        UM.UserName = (String)r["UserName"];
                        UM.GroupName = (String)r["GroupName"];
                        UM.LoginName = (String)r["LoginName"];
                        UM.Def_DBName = r["DefDBName"] == DBNull.Value ? null : (String)r?["DefDBName"];
                        UM.Def_SchemaName = r["DefSchemaName"] == DBNull.Value ? null : (String)r?["DefSchemaName"];
                        usersList.Add(UM);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return usersList;
        }
        public List<LoginMembership> GetLoginMembership()
        {

            var loginsList = new List<LoginMembership>();
            DataSet ds = new DataSet();
            string query = Resources.LoginMembership;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        LoginMembership LM = new LoginMembership();
                        LM.LoginName = (String)r["LoginName"];
                        LM.Type = (String)r["Type"];
                        LM.ServerRole = (String)r["ServerRole"];
                        LM.Disabled = Convert.ToBoolean(r["is_disabled"]);
                        LM.Def_DBName = (String)r["default_database_name"];
                        loginsList.Add(LM);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return loginsList;
        }
        public List<LogSize> GetLogSize()
        {
            var logsizeList = new List<LogSize>();
            DataSet ds = new DataSet();
            string query = Resources.LogSize;
            try
            {
                ds = (DataSet)ExecuteQuery(query);
                foreach (DataTable t in ds.Tables)
                {
                    foreach (DataRow r in t.Rows)
                    {
                        LogSize LS = new LogSize();
                        LS.DBName = (String)r["Database Name"];
                        LS.LogSize_MB = Convert.ToDecimal(r["Log Size (MB)"]);
                        LS.PercentUsed = Convert.ToDecimal(r["Log Space Used (%)"]);
                        logsizeList.Add(LS);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception catched: " + ex.Message);
                throw;
                //Console.WriteLine("Exception catched: " + ex.Message);
                //DO2.Success = false;
            }

            return logsizeList;
        }
        public bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    //Console.WriteLine("Could not connect to server...");
                    return false;
                }
            }
        }
    }
}
