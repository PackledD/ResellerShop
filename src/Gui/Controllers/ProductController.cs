using BuisnessLogic.interfaces.repository;
using BuisnessLogic.models;
using BuisnessLogic;
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
using Gui.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gui.controllers
{
    [Controller]
    [Authorize]
    public class ProductController : Controller
    {
        private static BuisnessLogic.controllers_inner.ProductController innerCtrl;
        private static ControllerManager innerMng;

        public static void SetInnerManager(ControllerManager mng)
        {
            innerMng = mng;
            innerCtrl = mng.ProductController;
        }

        private User GetUser()
        {
            var authFeatures = HttpContext.Features.Get<IAuthenticateResultFeature>();
            var authClaims = authFeatures.AuthenticateResult.Principal.Claims;
            int id = int.Parse(authClaims.First().Value);
            return innerMng.UserController.Get(id);
        }

        private Firm GetFirm()
        {
            return GetUser().Firm;
        }

        [HttpGet("Product/Catalog")]
        public async Task<IActionResult> Catalog([FromQuery] int category = -1)
        {
            Catalog md = new();
            try
            {
                Category cat = null;
                if (category != -1)
                {
                    try
                    {
                        cat = innerMng.CategoryController.Get(category);
                    }
                    catch { }
                }
                Firm frm = innerMng.FirmController.Get(0);
                var cats = innerMng.CategoryController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (cat != null && x.Id == cat.Id) });
                ViewBag.Categories = cats.Append(new SelectListItem { Text = "---", Value = -1 + "", Selected = (cat == null) });
                var firms = innerMng.FirmController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (frm != null && x.Id == frm.Id) });
                ViewBag.Firms = firms.Append(new SelectListItem { Text = "---", Value = -1 + "", Selected = (frm == null) });
                md.Products = (cat != null) ? innerCtrl.GetByCategory(cat) : innerCtrl.GetAll();
                if (frm != null)
                {
                    if (GetFirm().Id == 0)
                    {
                        md.Products = md.Products.Where(x => x.Distributor.Id != frm.Id).ToList();
                    }
                    else
                    {
                        md.Products = md.Products.Where(x => x.Distributor.Id == frm.Id).ToList();
                    }
                }
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet("/Product/{id}")]
        public async Task<IActionResult> Product([FromRoute] int id)
        {
            ProductWithWareProd md = new();
            try
            {
                md.Product = innerCtrl.Get(id);
                md.Data = innerMng.WareProdController.GetAll().Where(x => x.Product.Id == id).ToList();
            }
            catch (Exception) { }
            return View(md);
        }
    }
}
