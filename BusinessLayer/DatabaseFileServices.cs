using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;
using DataLayer;

namespace BusinessLayer
{
    public class DatabaseFileServices
    {
        static IDatabaseFileRepository repository;

        static DatabaseFileServices()
        {
            repository = new DatabaseFileRepository();
        }

        public static List<DatabaseFile> GetAll()
        {
            return repository.GetAll();
        }
        public static DatabaseFile GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public static DatabaseFile Insert(DatabaseFile obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(DatabaseFile obj)
        {
            repository.Update(obj);
        }

        public static void Delete(DatabaseFile obj)
        {
            repository.Delete(obj);
        }
    }
}
