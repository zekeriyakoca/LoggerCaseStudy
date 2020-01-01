using LoggerCaseStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task<bool> AddAsync(Log log);
        Task<IList<Log>> FetchAll();
    }
}
