using BusinessLayer;
using DataLayer.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace vsgutu.Controllers
{
    public class LoginController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        IWebHostEnvironment _appEnvironment;
        public LoginController(DataManager dataManager, IWebHostEnvironment appEnvironment)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(_dataManager);
            _appEnvironment = appEnvironment;
        }


        public IActionResult Index(bool msg = false)
        {
            TempData["notFoundUser"] = msg;
            return View();
        }
        [HttpPost]
        public IActionResult Index(string login, string password)
        {

            if (_serviceManager.Users.CheckUsers(login, PasswordEnc.HashPassword(password)) == true)
            {
                TempData["notFoundUser"] = false;
                var user = _serviceManager.Users.SearchUserByLoginAndPassword(login, PasswordEnc.HashPassword(password));
                if(user.Users.IdRole.Role == "Администратор")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login),
                        new Claim(ClaimTypes.Role,"Administrator")
                    };
                    var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                    var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                    HttpContext.SignInAsync("Cookie", claimPrincipal);

                    string _temp2 = "All";
                    return RedirectToAction("Temp", "Admin", new { user.Users.Id, _temp2 });
                }
                else if (user.Users.IdRole.Role == "Преподователь")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login),
                        new Claim(ClaimTypes.Role,"Teacher")
                    };
                    var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                    var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                    HttpContext.SignInAsync("Cookie", claimPrincipal);
                    string _temp2 = "All";
                    return RedirectToAction("Temp", "Teacher", new { user.Users.Id, _temp2 });
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login),
                        new Claim(ClaimTypes.Role,"Student")
                    };
                    var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                    var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                    HttpContext.SignInAsync("Cookie", claimPrincipal);

                    return RedirectToAction("Temp", "Student", new { user.Users.Id });
                }

            }
            else
            {
                TempData["notFoundUser"] = true;
                bool msg = true;
                return View(msg);

            }


        }

        public IActionResult Logoff()
        {
            HttpContext.SignOutAsync("Cookie");
            return RedirectToAction("Index", "Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
