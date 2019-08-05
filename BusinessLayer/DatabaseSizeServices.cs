using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;


namespace BusinessLayer
{
    public class DatabaseSizeServices
    {
        static IDatabaseSizeRepository repository;
        static DatabaseSizeServices()
        {
            repository = new DatabaseSizeRepository();
        }

        public static DatabaseSize Insert(DatabaseSize obj)
        {
            return repository.Insert(obj);
        }
    }
}
