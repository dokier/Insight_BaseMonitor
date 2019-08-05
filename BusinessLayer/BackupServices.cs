using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
    public class BackupServices
    {
        static IBackupRepository repository;

        static BackupServices()
        {
            repository = new BackupRepository();
        }

        public static List<Backup> GetAll()
        {
            return repository.GetAll();
        }
        public static Backup GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public static Backup Insert(Backup obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(Backup obj)
        {
            repository.Update(obj);
        }

        public static void Delete(Backup obj)
        {
            repository.Delete(obj);
        }
    }
}
