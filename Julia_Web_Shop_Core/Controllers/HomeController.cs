using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Julia_Web_Shop_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Julia_Web_Shop_Core.Controllers
{
    public class HomeController : Controller
    {
        private AppDBContent db;
        private readonly Users_Function users_Function;
        public HomeController(AppDBContent content, Users_Function users_Function)
        {
            this.db = content;
            this.users_Function = users_Function;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Главная";

            return View(await db.Products.ToListAsync());
        }
        public IActionResult Productdetail()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Shoppingcart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Input_Output()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Input_Output(Users viewModel)
        {
            if (db.User.Count() != 0)
            {
                if (!users_Function.GetByLogin(viewModel.Login))
                {
                    ModelState.AddModelError(nameof(viewModel.Login), "Такой пользователь уже есть");
                }
                if (!users_Function.GetByEmail(viewModel.Login))
                {
                    ModelState.AddModelError(nameof(viewModel.Login), "Такая почта уже использутеся");
                }

            }
            if (ModelState.IsValid)
            {
                Debug.Write("Вы зарегистрировались");
                users_Function.SaveArticle(viewModel);

                return View("Index", await db.Products.ToListAsync());
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OutPut(Users viewModel)
        {
        
                var user = await db.User.FirstOrDefaultAsync(u => u.Login == viewModel.Login && u.Password == viewModel.Password);
                if (user != null)
                {
                    await Authenticate(viewModel.Login); // аутентификация

                    return View("Index", await db.Products.ToListAsync());
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View("Input_Output");
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}
