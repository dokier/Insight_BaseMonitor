using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
    public class JobLogServices
    {
        static IJobLogRepository repository;
        //public static bool logging;

        static JobLogServices()
        {
            repository = new JobLogRepository();
        }

        public static List<JobLog> GetAll()
        {
            return repository.GetAll();
        }

        public static JobLog GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public static JobLog Insert(JobLog obj)
        {
            //if (logging == true)
            //{
                return repository.Insert(obj);
            //}
            //else return obj;
        }

        public static void Update(JobLog obj)
        {
            repository.Update(obj);
        }

        public static void Delete(JobLog obj)
        {
            repository.Delete(obj);
        }
    }
}
