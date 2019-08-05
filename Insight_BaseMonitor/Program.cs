using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using System.Threading;
using Quartz.Impl.Matchers;
using Insight_BaseMonitor.Listeners;
using Insight_BaseMonitor.Jobs;
using Insight_BL;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System.Security;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using DataLayer.HelperClasses;

namespace Insight_BaseMonitor
{
    class Program
    {
        private static IScheduler _scheduler;

        static void Main(string[] args)
        {

            var RunDate = DateTime.Now;
            RunDate = RunDate.AddTicks(-(RunDate.Ticks % 10000000)); ;

            var Instances = InstanceServices.GetAllActive();
            var Jobs = JobServices.GetById(1);
            Jobs.RunDate = RunDate;
            Jobs.RunCount = Jobs.RunCount + 1;
            JobServices.Update(Jobs);
            //JobLogServices.logging = true;

            var LogBook = new JobLog { JobId = Jobs.Id, RunDate = RunDate, RunCount = (Int32)Jobs.RunCount};

            LogBook.Type = "INF"; LogBook.Message = "***** START OF LOG FILE *****";
            JobLogServices.Insert(LogBook);
            //Console.WriteLine("***** START OF LOG FILE *****");

            if (Jobs.Enabled == true)
            {
                LogBook.Type = "INF"; LogBook.Message = ("JOB: " + Jobs.Name + " IS ENABLED"); LogBook.RunDate = DateTime.Now;
                JobLogServices.Insert(LogBook);
                #region SchedulerSettings         
                // construct a scheduler factory
                //ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

                //get a scheduler
                // _scheduler = schedulerFactory.GetScheduler();


                //// add scheduler listener
                ////_scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());

                //// add global job listener
                ////_scheduler.ListenerManager.AddJobListener(new JobListener(), GroupMatcher<JobKey>.AnyGroup());

                //// add global trigger listener
                ////_scheduler.ListenerManager.AddTriggerListener(new TriggerListener(), GroupMatcher<TriggerKey>.AnyGroup());

                //_scheduler.Start();
                // Console.WriteLine("Starting Scheduler");
                #endregion

                #region CallQuartzJob
                /*foreach (string value in servers)
                {
                  //  if (_utility.VerifyRemoteMachineStatus(value) == true)
                    //{

                        // foreach (string value in servers)
                        //{

                        Jobs.getServerName srvName = new Jobs.getServerName(); //This Constructor needs to be parameterless
                        AddSimpleJob(srvName, value);

                        Jobs.getCpuData cpuData = new Jobs.getCpuData();
                        AddSimpleJob(cpuData, value);

                    //}
                    //else
                    //{
                      //  Console.WriteLine("Server: " + value + " is offline");
                    //}

                }*/
                #endregion
                LogBook.Type = "INF"; LogBook.Message = ("INITIATING SQL CONNECTIVITY TEST"); LogBook.RunDate = DateTime.Now;
                JobLogServices.Insert(LogBook);
                //Console.WriteLine("INFORMATIVE: INITIATING SQL CONNECTIVITY TEST");
                foreach (var i in Instances)
                {

                    var objLogic = new BusinessLayer.BusinessLayer();
                    objLogic.SetConnString(i.Name);

                    if (objLogic.IsServerConnected(objLogic.objDataLayer.ConnString) == true)
                    {
                        //Console.WriteLine("INFORMATIVE: SQL INSTANCE :" + i.Name + " IS UP");
                        LogBook.Type = "INF"; LogBook.Message = ("SQL INSTANCE :" + i.Name + " IS UP"); LogBook.RunDate = DateTime.Now;
                        JobLogServices.Insert(LogBook);
                        var helper_ServerProperty = new ServerProperty();
                        var helper_SysConfig = new SysConfig();
                        var helper_SysOsInfo = new SysOsInfo();
                        var helper_PowerPlan = new PowerPlan();

                        var InstanceDetail_obj = new InstanceDetail();

                        Console.WriteLine("INFORMATIVE: Getting Information from Instance: " + i.Name);

                        helper_ServerProperty = objLogic.GetServerProperties();
                        if (helper_ServerProperty.Success == true)
                        {
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET SERVER PROPERTIES"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                            helper_SysConfig = objLogic.GetSysConfiguration();
                            if (helper_SysConfig.Success == true)
                            {
                                LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET SYSTEM CONFIG"); LogBook.RunDate = DateTime.Now;
                                JobLogServices.Insert(LogBook);
                                helper_SysOsInfo = objLogic.GetSysOsInformation();
                                if (helper_SysOsInfo.Success == true)
                                {
                                    LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET SYSTEM OS INFORMATION"); LogBook.RunDate = DateTime.Now;
                                    JobLogServices.Insert(LogBook);
                                    helper_PowerPlan = objLogic.GetPowerPlan();
                                    if (helper_PowerPlan.Success == true)
                                    {
                                        LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET POWER PLAN"); LogBook.RunDate = DateTime.Now;
                                        JobLogServices.Insert(LogBook);
                                        InstanceDetail_obj.Edition = helper_ServerProperty.Edition;
                                        InstanceDetail_obj.Version = helper_ServerProperty.ProductVersion;
                                        InstanceDetail_obj.ServicePack = helper_ServerProperty.ProductLevel;
                                        InstanceDetail_obj.AuthMode = helper_ServerProperty.AuthMode;
                                        InstanceDetail_obj.InstallDate = helper_ServerProperty.SQLInstallDate;
                                        InstanceDetail_obj.TcpPort = helper_ServerProperty.LocalTcpPort;

                                        InstanceDetail_obj.MaxServerMemory_MB = helper_SysConfig.MaxServerMemory;
                                        InstanceDetail_obj.MinServerMemory_MB = helper_SysConfig.MinServerMemory;
                                        InstanceDetail_obj.BackupCompression = helper_SysConfig.BackupCompression;
                                        InstanceDetail_obj.MaxDOP = helper_SysConfig.MaxDop;
                                        InstanceDetail_obj.Xp_Cmdshell = helper_SysConfig.Xp_Cmdshell;

                                        InstanceDetail_obj.CPU_Count = helper_SysOsInfo.CpuCount;
                                        InstanceDetail_obj.ServerMemory_MB = helper_SysOsInfo.ServerMemory;
                                        InstanceDetail_obj.LastStartDate = helper_SysOsInfo.SQLStartTime;
                                        InstanceDetail_obj.MachineType = helper_SysOsInfo.MachineType;
                                        InstanceDetail_obj.PowerPlan = helper_PowerPlan.powerPlan;

                                        InstanceDetail_obj.InstanceId = i.Id;
                                        InstanceDetail_obj.RunCount = (Int32)Jobs.RunCount;
                                        InstanceDetail_obj.RunDate = (DateTime)Jobs.RunDate;

                                        try
                                        {
                                            InstanceDetailServices.Insert(InstanceDetail_obj);
                                        }
                                        catch (Exception ex)
                                        {
                                            //Console.WriteLine("ERROR: FAILED TO INSERT OBJECT INTO InstanceDetailed TABLE");
                                            LogBook.Type = "ERR"; LogBook.Message = ("<FAILED> COULD NOT INSERT OBJECT INTO InstanceDetailed TABLE - INSTANCE: " + i.Name + " TABLE - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                                            JobLogServices.Insert(LogBook);
                                        }
                                    }
                                    else
                                    {
                                        LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> GET POWER PLAN - INSTANCE: " + i.Name); LogBook.RunDate = DateTime.Now;
                                        JobLogServices.Insert(LogBook);
                                    }
                                }
                                else
                                {
                                    LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> GET SYSTEM OS INFORMATION - INSTANCE: " + i.Name); LogBook.RunDate = DateTime.Now;
                                    JobLogServices.Insert(LogBook);
                                }
                            }
                            else
                            {
                                LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> GET SYSTEM CONFIG - INSTANCE: " + i.Name); LogBook.RunDate = DateTime.Now;
                                JobLogServices.Insert(LogBook);
                            }
                        }
                        else
                        {
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> GET SERVER PROPERTIES - INSTANCE: " + i.Name); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        //var databaselist = new List<DataLayer.HelperClasses.DatabaseOption>();
                        //databaselist = 
                        try
                        {
                            foreach (DatabaseOption DO in objLogic.GetDatabaseOptions())
                            {
                                DO.InstanceId = i.Id;
                                DO.RunDate = (DateTime)Jobs.RunDate;
                                DO.RunCount = (Int32)Jobs.RunCount;
                                
                                DatabaseOptionServices.Insert(DO);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET DATABASE OPTIONS"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO DatabaseOptions TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        try
                        {
                            foreach (Backup B in objLogic.GetBackups())
                            {
                                B.RunDate = (DateTime)Jobs.RunDate;
                                B.RunCount = (Int32)Jobs.RunCount;
                                B.InstanceId = i.Id;
                                BackupServices.Insert(B);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET BACKUPS"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO Backups TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        try
                        {
                            foreach (Drive D in objLogic.GetDrives())
                            {
                                D.RunDate = (DateTime)Jobs.RunDate;
                                D.RunCount = (Int32)Jobs.RunCount;
                                D.InstanceId = i.Id;
                                DriveServices.Insert(D);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET DRIVE INFORMATION"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO Drives TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        try
                        {
                            foreach (DatabaseFile DBF in objLogic.GetDatabaseFiles())
                            {
                                DBF.RunDate = (DateTime)Jobs.RunDate;
                                DBF.RunCount = (Int32)Jobs.RunCount;
                                DBF.InstanceId = i.Id;
                                DatabaseFileServices.Insert(DBF);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET DATABASE FILE INFORMATION"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO DatabaseFiles TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        try
                        {
                            foreach (DatabaseSize DBS in objLogic.GetDatabaseSize())
                            {
                                DBS.RunDate = (DateTime)Jobs.RunDate;
                                DBS.RunCount = (Int32)Jobs.RunCount;
                                DBS.InstanceId = i.Id;
                                DatabaseSizeServices.Insert(DBS);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET DATABASE SIZE INFORMATION"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO DatabaseSize TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        try
                        {
                            foreach (UserMembership UM in objLogic.GetUserMembership())
                            {
                                UM.RunDate = (DateTime)Jobs.RunDate;
                                UM.RunCount = (Int32)Jobs.RunCount;
                                UM.InstanceId = i.Id;
                                UserMembershipServices.Insert(UM);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET USER MEMBERSHIP INFORMATION"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO UserMembership TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        try
                        {
                            foreach (LoginMembership LM in objLogic.GetLoginMembership())
                            {
                                LM.RunDate = (DateTime)Jobs.RunDate;
                                LM.RunCount = (Int32)Jobs.RunCount;
                                LM.InstanceId = i.Id;
                                LoginMembershipServices.Insert(LM);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET LOGIN MEMBERSHIP INFORMATION"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO LoginMembership TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        try
                        {
                            foreach (LogSize LS in objLogic.GetLogSize())
                            {
                                LS.RunDate = (DateTime)Jobs.RunDate;
                                LS.RunCount = (Int32)Jobs.RunCount;
                                LS.InstanceId = i.Id;
                                LogSizeServices.Insert(LS);
                            }
                            LogBook.Type = "INF"; LogBook.Message = ("<SUCCESS> GET LOG SIZE INFORMATION"); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine("Exception: " + ex.Message);
                            LogBook.Type = "ERR"; LogBook.Message = ("<FAIL> COULD NOT RETRIEVE OR INSERT DATA INTO LogSize TABLE - INSTANCE: " + i.Name + " - EXCEPTION: " + ex.Message); LogBook.RunDate = DateTime.Now;
                            JobLogServices.Insert(LogBook);
                        }
                    }
                    else
                    {
                        //Console.WriteLine("WARNING: COULD NOT CONNECT TO SQL INSTANCE: " + i.Name);
                        LogBook.Type = "WRN"; LogBook.Message = ("COULD NOT CONNECT TO SQL INSTANCE: " + i.Name); LogBook.RunDate = DateTime.Now;
                        JobLogServices.Insert(LogBook);
                    }
                }

                //JobServices.Update(Jobs);
            }
            else
            {
                LogBook.Type = "INF"; LogBook.Message = ("JOB: " + Jobs.Name + " IS DISABLED");
                JobLogServices.Insert(LogBook);
            }
            //_scheduler.Shutdown();

            Console.WriteLine("***** END OF LOG FILE *****");
            LogBook.Type = "INF"; LogBook.Message = "***** END OF LOG FILE *****";
            JobLogServices.Insert(LogBook);
            //Console.ReadLine();

        }

        #region AddSimpleJob (Quartz)
        public static void AddSimpleJob(IMyJob IJobName, string srvName)
        {
            //jobDetail by using JobDetailImpl method
            JobDetailImpl jobDetail = new JobDetailImpl(srvName+": "+IJobName.jobName, IJobName.jobGroup, IJobName.GetType());
            jobDetail.JobDataMap.Add("srvName", srvName);

            //creating Trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(IJobName.timeFrequency).WithRepeatCount(0))
                .Build();

            _scheduler.ScheduleJob(jobDetail, trigger);
        
            //Thread.Sleep(5000);
        }
#endregion
    }
    //[DisallowConcurrentExecution]

}
