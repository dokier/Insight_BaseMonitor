using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
    public class DriveServices
    {
        static IDriveRepository repository;

        static DriveServices()
        {
            repository = new DriveRepository();
        }

        public static List<Drive> GetAll()
        {
            return repository.GetAll();
        }
        public static Drive GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public static Drive Insert(Drive obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(Drive obj)
        {
            repository.Update(obj);
        }

        public static void Delete(Drive obj)
        {
            repository.Delete(obj);
        }
    }
}
