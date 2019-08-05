using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IDatabaseFileRepository
    {
        List<DatabaseFile> GetAll();

        DatabaseFile GetById(int Id);

        DatabaseFile Insert(DatabaseFile obj);

        void Update(DatabaseFile obj);

        void Delete(DatabaseFile obj);
    }
}
