using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using LogGrabber.Web.ViewModels;
using LogGrabber.DAL;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Authentication.Cookies;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LogGrabber.Web.Controllers
{
    public class AccountController : Controller
    {
        [FromServices]
        public LogGrabberDbContext Context { get; set; }
        public object IdentityCookieOptions { get; private set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var user = await Context.Users.FirstOrDefaultAsync(
            //    u => u.Name == model.UserName && u.Password == model.Password);

            //if (user == null)
            //{
            //    Context.Users.Add(new User
            //    {
            //        Name = model.UserName,
            //        Password = model.Password
            //    });
            //    await Context.SaveChangesAsync();
            //}

            //var principal = HttpContext.User as ClaimsPrincipal;
            //if (principal == null)
            //{
            //    return HttpUnauthorized();
            //}
            //var identity = new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Name, model.UserName),
            //    new Claim("Password", model.Password),
            //});
            //principal.AddIdentity(identity);

            //var claims = new[] {
            //    new Claim(ClaimTypes.Name, model.UserName, ClaimValueTypes.String),
            //    new Claim("Name", model.UserName, ClaimValueTypes.String)
            //};

            //var identity = new ClaimsIdentity(claims,
            //    CookieAuthenticationDefaults.AuthenticationScheme);

            var userIdentity = new ClaimsIdentity("Password");
            userIdentity.AddClaims(new[]
            {
                new Claim(ClaimTypes.Name, model.UserName)
            });
            var principal = new ClaimsPrincipal(userIdentity);

            //var principal = new ClaimsPrincipal(
            //    new ClaimsIdentity(new[]
            //    {
            //        new Claim(ClaimTypes.Name, model.UserName)
            //    },
            //    "AuthenticationScheme"));
            //principal.AddIdentity(identity);

            await HttpContext.Authentication.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties() { IsPersistent = true });

            return Redirect("~/");
        }
    }
}
