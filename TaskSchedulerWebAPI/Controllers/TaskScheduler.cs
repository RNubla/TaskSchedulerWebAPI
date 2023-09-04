using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskScheduler;
using TaskSchedulerWebAPI.Services;

namespace TaskSchedulerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskScheduler : ControllerBase
    {

        [HttpGet(Name = "GetAllJobs")]
        public IEnumerable<Task>Get()
        {
            TaskSchedulerService ts = new TaskSchedulerService();
            return ts.GetListOfTask();
        }
    }
}
