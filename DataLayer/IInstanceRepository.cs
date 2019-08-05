using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IInstanceRepository
    {
        List<Instance> GetAll();
        List<Instance> GetAllActive();
        Instance GetById(int Id);

        Instance Insert(Instance obj);

        void Update(Instance obj);

        void Delete(Instance obj);

    }
}
