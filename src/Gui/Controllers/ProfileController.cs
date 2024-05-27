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
using Microsoft.AspNetCore.Authorization;

namespace Gui.controllers
{
    [Controller]
    [Authorize]
    public class ProfileController : Controller
    {
        private static BuisnessLogic.controllers_inner.UserController innerCtrl;
        private static ControllerManager innerMng;

        public static void SetInnerManager(ControllerManager mng)
        {
            innerMng = mng;
            innerCtrl = mng.UserController;
        }

        private User GetUser()
        {
            var authFeatures = HttpContext.Features.Get<IAuthenticateResultFeature>();
            var authClaims = authFeatures.AuthenticateResult.Principal.Claims;
            int id = int.Parse(authClaims.First().Value);
            return innerCtrl.Get(id);
        }

        private Firm GetFirm()
        {
            return GetUser().Firm;
        }

        async private Task<bool> UpdateUser(User user)
        {
            Claim[] claims = {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.FullName),
                    new(ClaimTypes.Role, user.UserKind.ToString())
            };
            ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties { IsPersistent = true }
                );
            return true;
        }

        [HttpGet("/Profile")]
        public async Task<IActionResult> Profile()
        {
            User md = null;
            try
            {
                md = GetUser();
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet("/ProfileEdit")]
        public async Task<IActionResult> ProfileEdit()
        {
            User md = null;
            try
            {
                md = GetUser();
                ViewData["Firms"] = new SelectList(innerMng.FirmController.GetAll(), "Id", "Name");
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        [Route("/ProfileEdit/ProfileEdit")]
        public async Task<IActionResult> ProfileEdit([FromForm] string fullname,
                                                     [FromForm] string email,
                                                     [FromForm] string phone)

        {
            if (innerCtrl == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                User user = GetUser();
                user.FullName = fullname;
                user.Email = email;
                user.Phone = phone;
                innerCtrl.Update(user);
                await UpdateUser(user);
                return Redirect("/Profile");
            }
            catch (LogicException e)
            {
                User md = GetUser();
                ViewData["Error"] = e.ToString();
                return View("ProfileEdit", md);
            }
        }
    }
}
