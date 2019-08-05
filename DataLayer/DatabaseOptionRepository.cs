using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class DatabaseOptionRepository : IDatabaseOptionRepository
    {
        public void Delete(DatabaseOption obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.DatabaseOptions.Attach(obj);
                db.DatabaseOptions.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<DatabaseOption> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.DatabaseOptions.ToList();
            }
        }

        public DatabaseOption GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.DatabaseOptions.Find(Id);
            }
        }

        public DatabaseOption Insert(DatabaseOption obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.DatabaseOptions.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(DatabaseOption obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.DatabaseOptions.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
