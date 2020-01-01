using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggerCaseStudy.Domain.Interfaces;
using LoggerCaseStudy.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace LoggerCaseStudy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger logger;

        public HomeController(ILogger logger, ILogRepository logRepository)
        {
            this.logger = logger;
            this.logRepository = logRepository;
        }

        public ILogRepository logRepository { get; }

        [HttpGet]
        public IActionResult Get()
        {
            logger.Add("home/get method called", "");
            return Ok("success");
        }

    }
}
