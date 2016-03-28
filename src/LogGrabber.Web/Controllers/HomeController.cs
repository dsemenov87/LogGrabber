using System.Linq;
using Microsoft.AspNet.Mvc;
using LogGrabber.DAL;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LogGrabber.Web.Controllers
{
    public class HomeController : Controller
    {
        [FromServices]
        public LogGrabberDbContext Context { get; set; }
        
        // GET: /<controller>/{int}
        public IActionResult Index(Status id = Status.Critical)
        {
            ViewBag.CurrentStatus = id;

            var model = Context.Logs
                .Where(i => i.Status == id)
                .Include(l => l.Error)
                .Include(l => l.Application)
                .ToList();

            return View(model);
        }
    }
}
