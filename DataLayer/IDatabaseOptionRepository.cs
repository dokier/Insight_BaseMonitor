using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IDatabaseOptionRepository
    {
        List<DatabaseOption> GetAll();

        DatabaseOption GetById(int Id);

        DatabaseOption Insert(DatabaseOption obj);

        void Update(DatabaseOption obj);

        void Delete(DatabaseOption obj);
    }
}
