using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class InstanceRepository : IInstanceRepository
    {
        public void Delete(Instance obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Instances.Attach(obj);
                db.Instances.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<Instance> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Instances.ToList();
            }
        }
        public List<Instance> GetAllActive()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Instances.Where(x => x.Active == true).ToList();
            }
        }

        public Instance GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Instances.Find(Id);
            }
        }

        public Instance Insert(Instance obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Instances.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(Instance obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Instances.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
