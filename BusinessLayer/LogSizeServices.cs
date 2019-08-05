using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;
using DataLayer;

namespace BusinessLayer
{
    public class LogSizeServices
    {
        static ILogSizeRepository repository;

        static LogSizeServices()
        {
            repository = new LogSizeRepository();
        }
        public static LogSize Insert(LogSize obj)
        {
            return repository.Insert(obj);
        }
    }
}
