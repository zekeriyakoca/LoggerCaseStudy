using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Infrastructure.EntityFramework;
using LoggerCaseStudy.Infrastructure.Repositories;
using LoggerCaseStudy.Interfaces;
using LoggerCaseStudy.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LoggerCaseStudy.Test
{
    public class DBLoggerShould
    {
        public DBLoggerShould()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                             .UseInMemoryDatabase(databaseName: "TestDBContext")
                             .Options;

            var context = new DataContext(options);

            var mockLogRepository = new LogRepository(context);
            this.dbLogger = new DBLogger(mockLogRepository);
        }

        public DBLogger dbLogger { get; }

        [Fact]
        public async Task LogToDB()
        {
            var result = await dbLogger.AddAsync(new Domain.Entities.Log()
            {
                Body = "Test Body",
                Message = "Test"
            });

            Assert.True(result);

        }
    }
}
