using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IDriveRepository
    {
        List<Drive> GetAll();

        Drive GetById(int Id);

        Drive Insert(Drive obj);

        void Update(Drive obj);

        void Delete(Drive obj);
    }
}
