using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using BuisnessLogic.controllers_inner;
using Exceptions;
using Exceptions.logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.Rendering;
using General;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace Gui.controllers
{
    [Controller]
    public class AuthController : Controller
    {
        private static BuisnessLogic.controllers_inner.UserController innerCtrl;
        private static ControllerManager innerMng;

        public static void SetInnerManager(ControllerManager mng)
        {
            innerMng = mng;
            innerCtrl = mng.UserController;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string login, [FromForm] string password)
        {
            if (innerCtrl == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                string hash = General.utils.Hasher.GetHash(password);
                var res = innerCtrl.Auth(login, hash);
                Claim[] claims = {
                    new(ClaimTypes.NameIdentifier, res.Id.ToString()),
                    new(ClaimTypes.Name, res.FullName),
                    new(ClaimTypes.Role, res.UserKind.ToString())
                };
                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties { IsPersistent = true }
                );
                return Redirect("/");
            }
            catch (LogicException e)
            {
                ViewData["Error"] = e.ToString();
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (innerCtrl == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                ViewBag.Firms = innerMng.FirmController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
                return View();
            }
            catch (LogicException e)
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] string fullname,
                                                  [FromForm] int firm,
                                                  [FromForm] string email,
                                                  [FromForm] string phone,
                                                  [FromForm] string password)
        {
            if (innerCtrl == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                string hash = General.utils.Hasher.GetHash(password);
                Firm newFirm = innerMng.FirmController.Get(firm);
                User user = new(-1, fullname, newFirm, email, phone, BuisnessLogic.enums.UsersEnum.NotSet);
                var res = innerCtrl.Register(user, hash);
                await Login(email, password);
                return Redirect("/");
            }
            catch (LogicException e)
            {
                ViewData["Error"] = e.ToString();
                ViewData["Firms"] = new SelectList(innerMng.FirmController.GetAll(), "Id", "Name");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
