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
using BuisnessLogic.enums;
using Microsoft.AspNetCore.Authorization;

namespace Gui.controllers
{
    [Controller]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private static ControllerManager innerMng;

        public static void SetInnerManager(ControllerManager mng)
        {
            innerMng = mng;
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

        [HttpGet]
        public async Task<IActionResult> Panel()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Warehouses()
        {
            try
            {
                ViewBag.Warehouses = innerMng.WarehouseController.GetAll();
            }
            catch (Exception) { }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Warehouse([FromRoute] int id)
        {
            Warehouse md = null;
            try
            {
                md = innerMng.WarehouseController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> WarehouseCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WarehouseCreate([FromForm] string address)
        {
            try
            {
                Warehouse wh = new(-1, address);
                innerMng.WarehouseController.Create(wh);
                return Redirect($"/Admin/Warehouse/{wh.Id}");
            }
            catch (Exception) { }
            return Redirect("/Admin/WarehouseCreate");
        }

        [HttpGet]
        public async Task<IActionResult> WarehouseEdit([FromRoute] int id)
        {
            Warehouse md = null;
            try
            {
                md = innerMng.WarehouseController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> WarehouseEdit([FromRoute] int id, [FromForm] string address)
        {
            try
            {
                Warehouse md = innerMng.WarehouseController.Get(id);
                md.Address = address;
                innerMng.WarehouseController.Update(md);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Warehouse/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> WarehouseDelete([FromRoute] int id)
        {
            try
            {
                innerMng.WarehouseController.Delete(id);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Warehouses");
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            try
            {
                ViewBag.Categories = innerMng.CategoryController.GetAll();
            }
            catch (Exception) { }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Category([FromRoute] int id)
        {
            Category md = null;
            try
            {
                md = innerMng.CategoryController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate([FromForm] string name)
        {
            try
            {
                Category wh = new(-1, name);
                innerMng.CategoryController.Create(wh);
                return Redirect($"/Admin/Category/{wh.Id}");
            }
            catch (Exception) { }
            return Redirect("/Admin/CategoryCreate");
        }

        [HttpGet]
        public async Task<IActionResult> CategoryEdit([FromRoute] int id)
        {
            Category md = null;
            try
            {
                md = innerMng.CategoryController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit([FromRoute] int id, [FromForm] string name)
        {
            try
            {
                Category md = innerMng.CategoryController.Get(id);
                md.Name = name;
                innerMng.CategoryController.Update(md);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Category/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> CategoryDelete([FromRoute] int id)
        {
            try
            {
                innerMng.CategoryController.Delete(id);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            try
            {
                ViewBag.Products = innerMng.ProductController.GetAll();
            }
            catch (Exception) { }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Product([FromRoute] int id)
        {
            Product md = null;
            try
            {
                md = innerMng.ProductController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            try
            {
                ViewBag.Firms = innerMng.FirmController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
                ViewBag.Categories = innerMng.CategoryController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
                ViewBag.Producers = innerMng.ProducerController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
            }
            catch (Exception) { }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate([FromForm] string name,
                                                       [FromForm] int category,
                                                       [FromForm] int distributor,
                                                       [FromForm] int cost,
                                                       [FromForm] int producer)
        {
            try
            {
                Category cat = innerMng.CategoryController.Get(category);
                Firm firm = innerMng.FirmController.Get(distributor);
                Producer produce = innerMng.ProducerController.Get(producer);
                Product wh = new(-1, name, cat, firm, cost, produce);
                innerMng.ProductController.Create(wh);
                return Redirect($"/Admin/Product/{wh.Id}");
            }
            catch (Exception) { }
            return Redirect("/Admin/ProductCreate");
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit([FromRoute] int id)
        {
            Product md = null;
            try
            {
                md = innerMng.ProductController.Get(id);
                ViewBag.Firms = innerMng.FirmController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (x.Id == md.Distributor.Id)});
                ViewBag.Categories = innerMng.CategoryController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (x.Id == md.Category.Id) });
                ViewBag.Producers = innerMng.ProducerController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (x.Id == md.Producer.Id) });
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit([FromRoute] int id,
                                                     [FromForm] string name,
                                                     [FromForm] int category,
                                                     [FromForm] int distributor,
                                                     [FromForm] int cost,
                                                     [FromForm] int producer)
        {
            try
            {
                Category cat = innerMng.CategoryController.Get(category);
                Firm firm = innerMng.FirmController.Get(distributor);
                Product md = innerMng.ProductController.Get(id);
                Producer produce = innerMng.ProducerController.Get(producer);
                md.Name = name;
                md.Category = cat;
                md.Distributor = firm;
                md.Cost = cost;
                md.Producer = produce;
                innerMng.ProductController.Update(md);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Product/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete([FromRoute] int id)
        {
            try
            {
                innerMng.ProductController.Delete(id);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Products");
        }

        [HttpGet]
        public async Task<IActionResult> Firms()
        {
            try
            {
                ViewBag.Firms = innerMng.FirmController.GetAll();
            }
            catch (Exception) { }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Firm([FromRoute] int id)
        {
            Firm md = null;
            try
            {
                md = innerMng.FirmController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> FirmCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FirmCreate([FromForm] string name,
                                                    [FromForm] string email,
                                                    [FromForm] string phone,
                                                    [FromForm] string physAddr,
                                                    [FromForm] string lawAddr)
        {
            try
            {
                Firm wh = new(-1, name, email, phone, physAddr, lawAddr);
                innerMng.FirmController.Create(wh);
                return Redirect($"/Admin/Firm/{wh.Id}");
            }
            catch (Exception) { }
            return Redirect("/Admin/FirmCreate");
        }

        [HttpGet]
        public async Task<IActionResult> FirmEdit([FromRoute] int id)
        {
            Firm md = null;
            try
            {
                md = innerMng.FirmController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> FirmEdit([FromRoute] int id,
                                                  [FromForm] string name,
                                                  [FromForm] string email,
                                                  [FromForm] string phone,
                                                  [FromForm] string physAddr,
                                                  [FromForm] string lawAddr)
        {
            try
            {
                Firm md = innerMng.FirmController.Get(id);
                md.Name = name;
                md.Email = email;
                md.Phone = phone;
                md.PhysAddr = physAddr;
                md.LawAddr = lawAddr;
                innerMng.FirmController.Update(md);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Firm/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> FirmDelete([FromRoute] int id)
        {
            try
            {
                if (id != 0 && id != GetFirm().Id)
                {
                    innerMng.FirmController.Delete(id);
                }
            }
            catch (Exception) { }
            return Redirect($"/Admin/Firms");
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            try
            {
                ViewBag.Users = innerMng.UserController.GetAll();
            }
            catch (Exception) { }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> User([FromRoute] int id)
        {
            User md = null;
            try
            {
                md = innerMng.UserController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> UserCreate()
        {
            try
            {
                ViewBag.Firms = innerMng.FirmController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
                var dict = new Dictionary<string, string>()
                {
                    {UsersEnum.Manager.ToString(), UsersEnum.Manager + ""},
                    {UsersEnum.Director.ToString(), UsersEnum.Director + ""},
                    {UsersEnum.Admin.ToString(), UsersEnum.Admin + ""},
                    {UsersEnum.NotSet.ToString(), UsersEnum.NotSet + ""},
                    {UsersEnum.Auditor.ToString(), UsersEnum.Auditor + ""}
                };
                ViewBag.Kinds = new SelectList(dict, "Key", "Value");
            }
            catch (Exception) { }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate([FromForm] string name,
                                                    [FromForm] string email,
                                                    [FromForm] string phone,
                                                    [FromForm] int firmId,
                                                    [FromForm] UsersEnum kind)
        {
            try
            {
                Firm firm = innerMng.FirmController.Get(firmId);
                User wh = new(-1, name, firm, email, phone, kind);
                innerMng.UserController.Create(wh);
                return Redirect($"/Admin/User/{wh.Id}");
            }
            catch (Exception) { }
            return Redirect("/Admin/UserCreate");
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit([FromRoute] int id)
        {
            User md = null;
            try
            {
                User src = GetUser();
                md = innerMng.UserController.Get(id);
                if (src.Id == md.Id)
                {
                    return Redirect("/ProfileEdit");
                }
                ViewBag.Firms = innerMng.FirmController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (x.Id == md.Firm.Id) });
                var dict = new Dictionary<string, string>()
                {
                    {UsersEnum.Manager.ToString(), UsersEnum.Manager + ""},
                    {UsersEnum.Director.ToString(), UsersEnum.Director + ""},
                    {UsersEnum.Admin.ToString(), UsersEnum.Admin + ""},
                    {UsersEnum.NotSet.ToString(), UsersEnum.NotSet + ""},
                    {UsersEnum.Auditor.ToString(), UsersEnum.Auditor + ""}
                };
                ViewBag.Kinds = new SelectList(dict, "Key", "Value", md.UserKind);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit([FromRoute] int id,
                                                  [FromForm] string name,
                                                  [FromForm] string email,
                                                  [FromForm] string phone,
                                                  [FromForm] int firmId,
                                                  [FromForm] UsersEnum kind)
        {
            try
            {
                Firm firm = innerMng.FirmController.Get(firmId);
                User md = innerMng.UserController.Get(id);
                md.FullName = name;
                md.Email = email;
                md.Phone = phone;
                md.Firm = firm;
                md.UserKind = kind;
                innerMng.UserController.Update(md);
            }
            catch (Exception) { }
            return Redirect($"/Admin/User/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> UserDelete([FromRoute] int id)
        {
            try
            {
                User src = GetUser();
                if (src.Id == id)
                {
                    return Redirect($"/Admin/User/{id}");
                }
                innerMng.UserController.Delete(id);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Users");
        }

        [HttpGet]
        public async Task<IActionResult> Producers()
        {
            try
            {
                ViewBag.Producers = innerMng.ProducerController.GetAll();
            }
            catch (Exception) { }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Producer([FromRoute] int id)
        {
            Producer md = null;
            try
            {
                md = innerMng.ProducerController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> ProducerCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProducerCreate([FromForm] string name)
        {
            try
            {
                Producer wh = new(-1, name);
                innerMng.ProducerController.Create(wh);
                return Redirect($"/Admin/Producer/{wh.Id}");
            }
            catch (Exception) { }
            return Redirect("/Admin/ProducerCreate");
        }

        [HttpGet]
        public async Task<IActionResult> ProducerEdit([FromRoute] int id)
        {
            Producer md = null;
            try
            {
                md = innerMng.ProducerController.Get(id);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        public async Task<IActionResult> ProducerEdit([FromRoute] int id, [FromForm] string name)
        {
            try
            {
                Producer md = innerMng.ProducerController.Get(id);
                md.Name = name;
                innerMng.ProducerController.Update(md);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Producer/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> ProducerDelete([FromRoute] int id)
        {
            try
            {
                innerMng.ProducerController.Delete(id);
            }
            catch (Exception) { }
            return Redirect($"/Admin/Producers");
        }
    }
}
