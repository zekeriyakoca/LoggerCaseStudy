using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerCaseStudy.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Body { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    }
}
