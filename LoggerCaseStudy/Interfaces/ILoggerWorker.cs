using LoggerCaseStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Interfaces
{
    public interface ILoggerWorker
    {
        Task<bool> AddAsync(Log log);
    }
}
