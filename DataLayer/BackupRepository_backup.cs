using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class BackupRepository : IBackupRepository
    {
        public void Delete(Backup obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Backups.Attach(obj);
                db.Backups.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<Backup> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Backups.ToList();
            }
        }

        public Backup GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Backups.Find(Id);
            }
        }

        public Backup Insert(Backup obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Backups.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(Backup obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Backups.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
