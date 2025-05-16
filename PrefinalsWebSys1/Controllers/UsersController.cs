using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrefinalsWebSys1.ViewModels;
using System.Security.Claims;
using PrefinalsWebSys1.Models;

namespace PrefinalsWebSys1.Controllers
{
    public class UsersController : Controller
    {
        //[Authorize]
        public IActionResult Index()
        {
            var resp = new UsersPageViewModel();

            using (var db = new SmileBookDBContext())
            {
                resp.Users = (from rw in db.UserAccounts
                        //where !rw.IsDeleted
                        select rw).ToList();
            }


            return View(resp);
        }

        [Authorize]
        public IActionResult UserForm()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// For AJAX-Based Login
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<LoginStatusViewModel> Autheticate(LoginViewModel req)
        {
            LoginStatusViewModel resp = new LoginStatusViewModel();

            //Prepare Session Creation
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, req.Username),
                //new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true, // Set to false for session-only
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1) // 1 Day Session
                });

            return resp;
        }

        /// <summary>
        /// For Standard HTML Login
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AutheticateLogin(LoginViewModel req)
        {
            LoginStatusViewModel resp = new LoginStatusViewModel();

            if (req != null)
            {
                if (!string.IsNullOrWhiteSpace(req.Username) && !string.IsNullOrWhiteSpace(req.Password))
                {
                    //Database Authenticate here



                    //Prepare Session Creation
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, req.Username),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true, // Set to false for session-only
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1) // 1 Day Session
                        });

                    return RedirectToAction("Index", "Home", resp);
                }
                else
                {
                    resp.Message = "Empty Username or Password";
                    resp.Status = "ERROR";
                }
            }
            else
            {
                resp.Message = "Empty Username or Password";
                resp.Status = "ERROR";
            }

            return RedirectToAction("Login", "Users", resp);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
