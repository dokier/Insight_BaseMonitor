using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;
using System.Data.SqlClient;
using System.Data;
using DataLayer.HelperClasses;


namespace BusinessLayer
{
    public class BusinessLayer
    {
        public DatabaseLayer objDataLayer = new DatabaseLayer();

        public ServerProperty GetServerProperties()
        {
            return objDataLayer.GetServerProperties();
        }

        public SysConfig GetSysConfiguration()
        {
            return objDataLayer.GetSysConfiguration();
        }
        public SysOsInfo GetSysOsInformation()
        {
            return objDataLayer.GetSysOSInformation();
        }
        public PowerPlan GetPowerPlan()
        {
            return objDataLayer.GetPowerPlan();
        }
        public List<DatabaseOption> GetDatabaseOptions()
        {
            return objDataLayer.GetDatabaseOptions();
        }
        public List<Backup> GetBackups()
        {
            return objDataLayer.GetBackups();
        }
        public List<Drive> GetDrives()
        {
            return objDataLayer.GetDrives();
        }
        public List<DatabaseFile> GetDatabaseFiles()
        {
            return objDataLayer.GetDatabaseFiles();
        }

        public List<DatabaseSize> GetDatabaseSize()
        {
            return objDataLayer.GetDatabaseSize();
        }
        public List<UserMembership> GetUserMembership()
        {
            return objDataLayer.GetUserMembership();
        }
        public List<LoginMembership> GetLoginMembership()
        {
            return objDataLayer.GetLoginMembership();
        }
        public List<LogSize> GetLogSize()
        {
            return objDataLayer.GetLogSize();
        }
        public bool IsServerConnected(string connectionString)
        {
            return objDataLayer.IsServerConnected(connectionString);
        }
        public void SetConnString(string InstanceName)
        {
            objDataLayer.ConnString = objDataLayer.CreateConnectionString(InstanceName);
        }

    }
}
