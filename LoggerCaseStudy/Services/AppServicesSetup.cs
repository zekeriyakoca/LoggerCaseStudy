using LoggerCaseStudy.Domain.Interfaces;
using LoggerCaseStudy.Infrastructure;
using LoggerCaseStudy.Infrastructure.Repositories;
using LoggerCaseStudy.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Services
{
    public  class AppServicesSetup
    {
        public void Set(IServiceCollection services)
        {
            services.AddTransient<ILoggerWorker, DBLogger>()
                .AddTransient<ILoggerWorker, FileLogger>();// Order of defination affects functionality.

            services.AddTransient<ILogger, Logger>(); 

            services.AddTransient<ILogRepository, LogRepository>();

            services.AddTransient<DbSeeder>();


        }
    }
}
