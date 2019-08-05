using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class InstanceDetailRepository : IInstanceDetailRepository
    {
        public void Delete(InstanceDetail obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.InstanceDetails.Attach(obj);
                db.InstanceDetails.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<InstanceDetail> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.InstanceDetails.ToList();
            }
        }

        public InstanceDetail GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.InstanceDetails.Find(Id);
            }
        }

        public InstanceDetail Insert(InstanceDetail obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.InstanceDetails.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(InstanceDetail obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.InstanceDetails.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
