using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
    public static class InstanceDetailServices
    {
        static IInstanceDetailRepository repository;

        static InstanceDetailServices()
        {
            repository = new InstanceDetailRepository();
        }

        public static List<InstanceDetail> GetAll()
        {
            return repository.GetAll();
        }

        public static InstanceDetail GetById(int Id)
        {
            return repository.GetById(Id);
        }
        public static InstanceDetail Insert(InstanceDetail obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(InstanceDetail obj)
        {
            repository.Update(obj);
        }

        public static void Delete(InstanceDetail obj)
        {
            repository.Delete(obj);
        }
    }
}
