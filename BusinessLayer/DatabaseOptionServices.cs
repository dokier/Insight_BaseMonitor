using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
   public static class DatabaseOptionServices
    {
        static IDatabaseOptionRepository repository;

        static DatabaseOptionServices()
        {
            repository = new DatabaseOptionRepository();
        }

        public static List<DatabaseOption> GetAll()
        {
            return repository.GetAll();
        }

        public static DatabaseOption GetById(int Id)
        {
            return repository.GetById(Id);
        }
        public static DatabaseOption Insert(DatabaseOption obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(DatabaseOption obj)
        {
            repository.Update(obj);
        }

        public static void Delete(DatabaseOption obj)
        {
            repository.Delete(obj);
        }
    }
}
