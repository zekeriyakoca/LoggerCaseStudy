using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Interfaces
{
    public interface ILogger
    {
        Task Add(string message, object obj);
        Task Flush();
    }
}
