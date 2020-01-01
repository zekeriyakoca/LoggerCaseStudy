using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Infrastructure.EntityFramework;
using LoggerCaseStudy.Infrastructure.Repositories;
using LoggerCaseStudy.Interfaces;
using LoggerCaseStudy.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LoggerCaseStudy.Test
{

    public class FileLoggerShould
    {
        public FileLoggerShould()
        {

            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment
                .Setup(m => m.ContentRootPath)
                .Returns("");

            this.fileLogger = new FileLogger(mockEnvironment.Object);
        }

        public FileLogger fileLogger { get; }
        public DBLogger dbLogger { get; }

        [Fact]
        public async Task LogToFile()
        {
            var result = await fileLogger.AddAsync(new Domain.Entities.Log()
            {
                Body = "Test Body",
                Message = "Test"
            });

            Assert.True(result);

        }
    }
}
