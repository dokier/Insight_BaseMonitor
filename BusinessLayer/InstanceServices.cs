using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
    public static class InstanceServices
    {
        static IInstanceRepository repository;

        static InstanceServices()
        {
            repository = new InstanceRepository();
        }

        public static List <Instance> GetAll()
        {
            return repository.GetAll();
        }
        public static List<Instance> GetAllActive()
        {
            return repository.GetAllActive();
        }
        public static Instance GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public static Instance Insert(Instance obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(Instance obj)
        {
            repository.Update(obj);
        }

        public static void Delete(Instance obj)
        {
            repository.Delete(obj);
        }
    }
}
