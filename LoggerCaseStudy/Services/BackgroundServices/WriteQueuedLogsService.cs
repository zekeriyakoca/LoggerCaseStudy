using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Interfaces;
using LoggerCaseStudy.Util;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Services.BackgroundServices
{
    public class WriteQueuedLogsService : IHostedService, IDisposable
    {
        private Timer _timer;

        public IConfiguration configuration { get; }
        public IMemoryCache memoryCache { get; }
        public IServiceScopeFactory serviceScopeFactory { get; }
        public ILogger logger { get; }


        public WriteQueuedLogsService(IConfiguration configuration, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
        {
            this.configuration = configuration;
            this.memoryCache = memoryCache;
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            double interval;
            try
            {
                interval = double.Parse(this.configuration["Logging:Interval"]);
            }
            catch
            {
                interval = 20 ; // seconds
            }
            _timer = new Timer(async (o) => await DoWork(o), null, TimeSpan.Zero,
                TimeSpan.FromSeconds(interval));

            return Task.CompletedTask;
        }


        private async Task DoWork(object state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                await logger.Flush();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
