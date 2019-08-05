using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IJobRepository
    {
        List<Job> GetAll();

        List<Job> GetAllActive();

        Job GetById(int Id);

        Job Insert(Job obj);

        void Update(Job obj);

        void Delete(Job obj);
    }
}
