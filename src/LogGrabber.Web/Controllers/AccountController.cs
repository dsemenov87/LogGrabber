using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using LogGrabber.Web.ViewModels;
using LogGrabber.DAL;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.Data.Entity;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LogGrabber.Web.Controllers
{
    public class AccountController : Controller
    {
        [FromServices]
        public LogGrabberDbContext Context { get; set; }

        // GET: /<controller>/
        public IActionResult Login()
        {
            HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        // GET: /<controller>/
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await Context.Users.AnyAsync(u => u.Name == model.UserName))
            {
                return new BadRequestResult();
            }

            var user = Context.Users.Add(
                new User {
                    Name = model.UserName,
                    Password = model.Password
                });
            await Context.SaveChangesAsync();

            SignIn(user.Entity);

            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await Context.Users.FirstOrDefaultAsync(
                u => u.Name == model.UserName && u.Password == model.Password);

            if (user == null)
            {
                return Redirect("~/Account/Register");
            }

            SignIn(user);

            return Redirect("~/");
        }

        private async void SignIn(User user)
        {
            var principal = new ClaimsPrincipal(
               new ClaimsIdentity(
                   new[]
                   {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim("Password", user.Password),
                   },
                   CookieAuthenticationDefaults.AuthenticationScheme)
                   );

            await HttpContext.Authentication.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(5)
                });
        }
    }
}
