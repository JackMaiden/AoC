using System.Reflection;
using Challenges;
using Challenges.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.IO.File;

namespace AdventOfCodeSharp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdventOfCodeController : ControllerBase
    {
        private readonly IHostEnvironment _environment;
        private readonly RunChallenges _runChallenges;

        public AdventOfCodeController(IHostEnvironment environment)
        {
            _environment = environment;
            _runChallenges = new RunChallenges(_environment.ContentRootPath);
        }

        [HttpGet]
        [Route("Today")]
        public async Task<Result> DoAoCTask()
        {
            var results = await _runChallenges.RunAoCTask(DateTime.Today);
            return results.First();
        }

        [HttpPost]
        [Route("Year")]
        public async Task<IEnumerable<Result>> DoAoCTask(int? year)
        {
            year = year ?? DateTime.Today.Year;
            return await _runChallenges.RunAoCTask(new DateTime((int)year, 12, 01), new DateTime((int)year, 12, 25));
        }

        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Result>> DoAoCTaskTest()
        {
            return await _runChallenges.RunAoCTask(new DateTime(2021,12,01), new DateTime(2030, 12, 25));
        }

    }
}