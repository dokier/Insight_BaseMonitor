using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
    public class JobServices
    {
        static IJobRepository repository;

        static JobServices()
        {
            repository = new JobRepository();
        }

        public static List<Job> GetAll()
        {
            return repository.GetAll();
        }
        public static List<Job> GetAllActive()
        {
            return repository.GetAllActive();
        }
        public static Job GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public static Job Insert(Job obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(Job obj)
        {
            repository.Update(obj);
        }

        public static void Delete(Job obj)
        {
            repository.Delete(obj);
        }
    }
}
