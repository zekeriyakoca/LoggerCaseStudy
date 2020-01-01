using LoggerCaseStudy.Domain.Entities;
using LoggerCaseStudy.Domain.Interfaces;
using LoggerCaseStudy.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerCaseStudy.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly DataContext context;
        private DbSet<Log> dbSet;

        public LogRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<Log>();
        }
        public async Task<bool> AddAsync(Log log)
        {

            this.dbSet.Add(log);
            return SaveAll();
        }


        public async Task<IList<Log>> FetchAll()
        {
            return await this.dbSet.Where(_ => true).ToListAsync();
        }


        private bool SaveAll()
        {
            var result = this.context.SaveChanges() > 0;
            return result;
        }


    }
}
