
using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Infrastructure
{
    public class DbSeeder
    {
        private readonly DataContext context;

        public DbSeeder(DataContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            context.Database.EnsureCreated();
            if (!context.Logs.Any())
            {
                context.Logs.Add(new Log { Body = "", Message = "first log entry" });
                await context.SaveChangesAsync();
            }
        }
    }
}
