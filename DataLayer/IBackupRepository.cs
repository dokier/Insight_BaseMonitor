using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface IBackupRepository
    {
        List<Backup> GetAll();

        Backup GetById(int Id);

        Backup Insert(Backup obj);

        void Update(Backup obj);

        void Delete(Backup obj);
    }
}
