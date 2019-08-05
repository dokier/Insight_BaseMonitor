using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class JobLogRepository : IJobLogRepository
    {
        public void Delete(JobLog obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.JobLogs.Attach(obj);
                db.JobLogs.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<JobLog> GetAll()
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.JobLogs.ToList();
            }
        }

        public JobLog GetById(int Id)
        {
            using (InsightEntities db = new InsightEntities())
            {
                return db.JobLogs.Find(Id);
            }
        }

        public JobLog Insert(JobLog obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.JobLogs.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(JobLog obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.JobLogs.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
