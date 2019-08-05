using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IInstanceDetailRepository
    {
        List<InstanceDetail> GetAll();

        InstanceDetail GetById(int Id);

        InstanceDetail Insert(InstanceDetail obj);

        void Update(InstanceDetail obj);

        void Delete(InstanceDetail obj);
    }
}
