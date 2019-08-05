using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class JobRepository : IJobRepository
    {
        public void Delete(Job obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Jobs.Attach(obj);
                db.Jobs.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<Job> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Jobs.ToList();
            }
        }
        public List<Job> GetAllActive()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Jobs.Where(x => x.Enabled == true).ToList();
            }
        }

        public Job GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.Jobs.Find(Id);
            }
        }

        public Job Insert(Job obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Jobs.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(Job obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.Jobs.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
