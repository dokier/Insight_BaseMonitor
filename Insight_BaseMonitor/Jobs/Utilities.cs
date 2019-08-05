using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.IO;
using Microsoft.Management.Infrastructure;



namespace Insight_BaseMonitor.Jobs
{
    class Utilities
    {
        public bool VerifyRemoteMachineStatus(string machineName)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(machineName);
                    if (reply.Status == IPStatus.Success)
                    { return true; }
                }
            }
            catch (Exception ex)
            {
                // return false for any exception encountered
                // we'll probably want to just shut down anyway
            }
            return false;
        }

        public bool isClustered(string cluster)
        {
            RegistryKey environmentKey;
            string remoteName;
            if (cluster.Length == 0)
            {
                Console.WriteLine("Error: The name of the remote " +
                    "computer must be specified when the program is " +
                    "invoked.");
                return false;
            }

            else
            {
                remoteName = cluster;
            }


                // Open HKEY_CURRENT_USER\Environment 
                // on a remote computer.
                //  new System.Security.Permissions.RegistryPermission(
                //System.Security.Permissions.PermissionState.Unrestricted).Assert();
                using (environmentKey = RegistryKey.OpenRemoteBaseKey(
                      RegistryHive.LocalMachine, remoteName, RegistryView.Registry64).OpenSubKey(
                      @"Cluster"))
                {
                    if (environmentKey == null)
                    {
                        //environmentKey.Close();
                        return false;
                    }
                    else 
                    {
                        environmentKey.Close();
                        return true;
                    }

                }
                /*catch (IOException e)
                    {
                        Console.WriteLine("{0}: {1}",
                            e.GetType().Name, e.Message);
                        return false;
                    }*/


                /* Console.WriteLine("\nThere are {0} values for {1}.",
                environmentKey.ValueCount.ToString(),
                environmentKey.Name);
                 foreach (string valueName in environmentKey.GetValueNames())
                 {
                     Console.WriteLine("{0,-20}: {1}", valueName,
                         environmentKey.GetValue(valueName).ToString());
                 }*/

                // Close the registry key.
            
        }


        public void isClustered2(string cluster)
        {

            string Namespace = @"root\cimv2";
            string OSQuery = "SELECT * FROM Win32_OperatingSystem";
            CimSession mySession = CimSession.Create(cluster);
            IEnumerable<CimInstance> queryInstance = mySession.QueryInstances(Namespace, "WQL", OSQuery);
        }

        public static List<Insight_BL.Instance> getInstances()
        {
            List<Insight_BL.Instance> Instances = null;
            using (var context = new Insight_BL.InsightEntities())
            {
                Instances = (from i in context.Instances
                                select i).ToList();

                return Instances;
            }
        }

    }
    
}
