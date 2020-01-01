using LoggerCaseStudy.Interfaces;
using LoggerCaseStudy.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("LoggerCaseStudy.Test.Startup", "AssemblyName")]
namespace LoggerCaseStudy.Test
{
    public class Startup : DependencyInjectionTestFramework
    {
        public Startup(IMessageSink messageSink) : base(messageSink) { }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DBLogger>();
            services.AddTransient<FileLogger>();
        }
    }
}
