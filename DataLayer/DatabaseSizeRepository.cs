using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class DatabaseSizeRepository : IDatabaseSizeRepository
    {
        public DatabaseSize Insert(DatabaseSize obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.DatabaseSize.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }
    }
}
