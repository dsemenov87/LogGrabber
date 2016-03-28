using System.Linq;
using Microsoft.AspNet.Mvc;
using LogGrabber.DAL;
using Microsoft.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LogGrabber.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [FromServices]
        public LogGrabberDbContext Context { get; set; }
        
        // GET: /<controller>/{int}
        public IActionResult Index(Status id = Status.Critical)
        {
            ViewBag.CurrentStatus = id;

            var user = HttpContext.User as ClaimsPrincipal;
            if (user == null)
            {
                return View(Enumerable.Empty<LogItem>());
            }

            string userName = ViewBag.CurrentUserName = user.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();

            var model = Context.Logs
                .Where(l => l.User.Name == userName && l.Status == id)
                .Include(l => l.Error)
                .Include(l => l.Application)
                .ToList();

            return View(model);
        }
    }
}
