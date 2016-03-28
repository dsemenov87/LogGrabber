using Microsoft.AspNet.Mvc;
using LogGrabber.DAL;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Data.Entity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LogGrabber.Web.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        [FromServices]
        public LogGrabberDbContext Context { get; set; }

        // GET api/logs/5
        [HttpGet("{id:long}", Name = "GetLog")]
        public async Task<IActionResult> GetById(long id)
        {
            return await Context.Logs
                .Where(l => l.Id == id)
                .Select(l => new ObjectResult(id))
                .FirstOrDefaultAsync();
        }

        // POST api/logs
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LogItem item)
        {
            if (item == null)
            {
                return HttpBadRequest();
            }
            item.Error = await EnsureError(item.Error);
            item.Application = await EnsureApplication(item.Application);

            Context.Logs.Add(item);
            await Context.SaveChangesAsync();
            return CreatedAtRoute("GetLog", new {
                controller = "Log",
                id = item.Id
            }, item);
        }

        private async Task<Error> EnsureError(Error item)
        {
            if (item == null) return null;

            var existedError = await Context.Errors
                .FirstOrDefaultAsync(err => err.Name == item.Name);

            return existedError ?? Context.Errors.Add(item).Entity;
        }

        private async Task<Application> EnsureApplication(Application item)
        {
            if (item == null) return null;

            var existedError = await Context.Applications
                .FirstOrDefaultAsync(a => a.Name == item.Name);

            return existedError ?? Context.Applications.Add(item).Entity;
        }

    }
}
