using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Interfaces;
using LoggerCaseStudy.Util;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Services
{
    public class Logger : ILogger
    {


        public Logger(IEnumerable<ILoggerWorker> loggerWorkers, IMemoryCache memoryCache)
        {
            this.loggerWorkers = loggerWorkers;
            this.memoryCache = memoryCache;
        }

        public IEnumerable<ILoggerWorker> loggerWorkers { get; }
        public IMemoryCache memoryCache { get; }

        // We only add logs to in-memory Cache. Background Service will handle logging to DB or file.
        public async Task Add(string message, object obj)
        {
            memoryCache.TryGetValue(AppConstants.LOG_QUEUE_KEY, out Queue<Log> queue);
            if (queue == null)
                queue = new Queue<Log>();
            if (queue.Count > 10000)
                await Flush();
            queue.Enqueue(new Log { Message = message, Body = JsonConvert.SerializeObject(obj) });
            this.memoryCache.Set(AppConstants.LOG_QUEUE_KEY, queue);
        }

        // Bakcground service will call this function. We also can call this function manually.  
        public async Task Flush()
        {
            memoryCache.TryGetValue(AppConstants.LOG_QUEUE_KEY, out Queue<Log> queue);
            if (queue == null)
                return;
            foreach (var logItem in queue)
            {
                foreach (var item in loggerWorkers)
                {
                    if (await item.AddAsync(logItem))
                        break;
                }
            }
            memoryCache.Remove(AppConstants.LOG_QUEUE_KEY);
        }
    }
}
