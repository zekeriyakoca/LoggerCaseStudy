using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Interfaces;
using LoggerCaseStudy.Util.Util;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Services
{
    public class FileLogger : ILoggerWorker
    {
        public FileLogger(IHostingEnvironment env)
        {
            this.env = env;
        }

        public IHostingEnvironment env { get; }

        public async Task<bool> AddAsync(Log log)
        {
            try
            {
                var filePath = $"{env.ContentRootPath}/Logs/{DateTime.UtcNow.ToShortDateString().Replace('/', '-')}-logs.txt";
                if (!FileOperations.WaitForFile(filePath))
                    return false;
                FileOperations.WriteToJsonFile(filePath, log, true); // Logs unable to written to DB
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
