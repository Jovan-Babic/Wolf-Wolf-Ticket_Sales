using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Wolf_Wolf_TicketSales.Models;
using Wolf_Wolf_TicketSales.Services;

namespace Wolf_Wolf_TicketSales.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if(User.IsInRole("ADMIN"))
            {
                return Redirect("/admin");
            }
            if(User.IsInRole("USER"))
            {
                return Redirect("/user");
            }

            return View();
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            try
            {
                var user = await _loginService.AuthenticateuserAsync(username, password);

                if (!string.IsNullOrEmpty(user.Role))
                {
                    var claims = new List<Claim>
                    {
                        new Claim("username", username),
                        new Claim(ClaimTypes.NameIdentifier, username),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    return Redirect(string.IsNullOrEmpty(returnUrl) ? $"/{user.Role}" : returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "User");
                }
            }
            catch(Exception ex)
            {
                if(ex.GetBaseException().Message.Equals("Invalid Password", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["Error"] = "Username or Password is invalid!";
                    return View("index");
                }

                return BadRequest();
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
