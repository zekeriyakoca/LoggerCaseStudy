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
            // Order of defination affects functionality. Next Logging methods work only if previous one fail. 
            // Successfully logging in one method will interpret logging queue and return.
            services.AddTransient<ILoggerWorker, DBLogger>()
                .AddTransient<ILoggerWorker, FileLogger>();
            
            services.AddTransient<ILogger, Logger>(); 

            services.AddTransient<ILogRepository, LogRepository>();

            services.AddTransient<DbSeeder>();


        }
    }
}
