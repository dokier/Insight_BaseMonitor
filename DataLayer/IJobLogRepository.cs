using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IJobLogRepository
    {
        List<JobLog> GetAll();

        JobLog GetById(int Id);

        JobLog Insert(JobLog obj);

        void Update(JobLog obj);

        void Delete(JobLog obj);
    }
}
