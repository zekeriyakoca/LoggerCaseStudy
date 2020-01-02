using LoggerCaseStudy.Infrastructure.EntityFramework;
using LoggerCaseStudy.Infrastructure.Repositories;
using LoggerCaseStudy.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LoggerCaseStudy.Test.Repositories
{
    

    public class LogRepositoryShould
    {
        public LogRepositoryShould()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                             .UseInMemoryDatabase(databaseName: "TestDBContext")
                             .Options;

            var context = new DataContext(options);

            this.logRepository = new LogRepository(context);
        }

        public LogRepository logRepository { get; }

        [Fact]
        public async Task FetchLogsFromDB()
        {
            var logList = await logRepository.FetchAll();
            Assert.True(logList != null);

        }

        [Fact]
        public async Task AddLogToDB()
        {
            var logCountBefore = (await logRepository.FetchAll())?.Count;
            var result = await logRepository.AddAsync(new Domain.Entities.Log()
            {
                Body = "Test Body",
                Message = "Test"
            });
            var logCountAfter = (await logRepository.FetchAll())?.Count;

            Assert.True(result);
            Assert.True(logCountAfter == logCountBefore + 1);

        }
    }
}
