using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class LogSizeRepository : ILogSizeRepository
    {
        public LogSize Insert(LogSize obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.LogSize.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }
    }
}
