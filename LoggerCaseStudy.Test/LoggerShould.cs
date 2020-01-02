using Castle.Core.Logging;
using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Infrastructure.EntityFramework;
using LoggerCaseStudy.Infrastructure.Repositories;
using LoggerCaseStudy.Interfaces;
using LoggerCaseStudy.Services;
using LoggerCaseStudy.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LoggerCaseStudy.Test
{

    public class LoggerShould
    {

        public DBLogger dbLogger { get; }
        public FileLogger fileLogger { get; }
        public Interfaces.ILogger logger { get; }
        public IMemoryCache memoryCache { get; }

        public LoggerShould()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                             .UseInMemoryDatabase(databaseName: "TestDBContext")
                             .Options;

            var context = new DataContext(options);

            var mockLogRepository = new LogRepository(context);

            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment
                .Setup(m => m.ContentRootPath)
                .Returns("");

            this.dbLogger = new DBLogger(mockLogRepository);
            this.fileLogger = new FileLogger(mockEnvironment.Object);
            var loggerWorkerList = new List<ILoggerWorker>() {
                this.dbLogger,
                this.fileLogger
            };
            this.memoryCache = GetMockMemmoryCache();
            this.logger = new Logger(loggerWorkerList, this.memoryCache);
        }

        private IMemoryCache GetMockMemmoryCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<IMemoryCache>();
        }

        [Fact]
        public async Task LogAnEntry()
        {
            memoryCache.TryGetValue(AppConstants.LOG_QUEUE_KEY, out Queue<Log> queue);
            var queueCountBefore = queue?.Count ?? 0;
            await logger.Add("Test Message", "Test Body");
            memoryCache.TryGetValue(AppConstants.LOG_QUEUE_KEY, out queue);
            var queueCountAfter = queue?.Count ?? 0;

            Assert.True(queueCountAfter == queueCountBefore + 1);

        }
    }
}
