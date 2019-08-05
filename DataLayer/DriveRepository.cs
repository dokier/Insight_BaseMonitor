using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class DriveRepository : IDriveRepository
    {
        public void Delete(Drive obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Drives.Attach(obj);
                db.Drives.Remove(obj);
                db.SaveChanges();
            }
        }

        public List <Drive> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Drives.ToList();
            }
        }

        public Drive GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Drives.Find(Id);
            }
        }

        public Drive Insert(Drive obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Drives.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(Drive obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Drives.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
