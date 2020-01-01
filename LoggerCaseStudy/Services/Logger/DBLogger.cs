using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Domain.Interfaces;
using LoggerCaseStudy.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Services
{
    public class DBLogger : ILoggerWorker
    {
        public DBLogger(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public ILogRepository logRepository { get; }

        public async Task<bool> AddAsync(Log log)
        {
            try
            {
                return await logRepository.AddAsync(log);
            }
            catch
            {
                return false;
            }

        }
    }
}
