using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class DatabaseFileRepository : IDatabaseFileRepository
    {
        public void Delete(DatabaseFile obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.DatabaseFiles.Attach(obj);
                db.DatabaseFiles.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<DatabaseFile> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.DatabaseFiles.ToList();
            }
        }

        public DatabaseFile GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.DatabaseFiles.Find(Id);
            }
        }

        public DatabaseFile Insert(DatabaseFile obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.DatabaseFiles.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(DatabaseFile obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.DatabaseFiles.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
