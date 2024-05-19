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
using System.IO;
using Microsoft.Extensions.Hosting.Internal;

namespace Gui.controllers
{
    [Controller]
    public class FirmController : Controller
    {
        private static BuisnessLogic.controllers_inner.FirmController innerCtrl;
        private static ControllerManager innerMng;
        private IWebHostEnvironment env;

        public static void SetInnerManager(ControllerManager mng)
        {
            innerMng = mng;
            innerCtrl = mng.FirmController;
        }

        public FirmController(IWebHostEnvironment _env)
        {
            env = _env;
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
        public async Task<IActionResult> Profile()
        {
            Firm md = null;
            try
            {
                md = GetFirm();
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> Contracts()
        {
            FirmContracts md = new();
            try
            {
                md.User = GetUser();
                md.Firm = md.User.Firm;
                if (md.Firm.Id == 0)
                {
                    md.Contracts = innerMng.ContractController.GetAll();
                }
                else
                {
                    md.Contracts = innerCtrl.GetContracts(md.Firm);
                }
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            FirmProducts md = new();
            try
            {
                md.Firm = GetFirm();
                md.Products = innerCtrl.GetProducts(md.Firm);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet("/Firm/Contracts/Contract/{id}")]
        public async Task<IActionResult> Contract([FromRoute] int id)
        {
            ContractWithPositions md = new();
            try
            {
                md.User = GetUser();
                md.Contract = innerMng.ContractController.Get(id);
                md.Positions = innerMng.ContractController.GetContent(md.Contract);
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost("/Firm/Contracts/Approve/{id}")]
        public async Task<IActionResult> Approve([FromRoute] int id)
        {
            try
            {
                var user = GetUser();
                var contr = innerMng.ContractController.Get(id);
                innerMng.ContractController.Approve(contr, user);
            }
            catch (Exception) { }
            return Redirect(String.Format("/Firm/Contracts/Contract/{0}", id));
        }

        [HttpPost("/Firm/Contracts/Sign/{id}")]
        public async Task<IActionResult> Sign([FromRoute] int id)
        {
            try
            {
                var user = GetUser();
                var contr = innerMng.ContractController.Get(id);
                innerMng.ContractController.Sign(contr, user);
            }
            catch (Exception) { }
            return Redirect(String.Format("/Firm/Contracts/Contract/{0}", id));
        }

        [HttpPost("/Firm/Contracts/Delete/{id}")]
        public async Task<IActionResult> DelContract([FromRoute] int id)
        {
            try
            {
                innerMng.ContractController.Delete(id);
            }
            catch (Exception) { }
            return Redirect("/Firm/Contracts");
        }

        [HttpGet("/Firm/Contracts/Contract/Create")]
        public async Task<IActionResult> ContractCreate()
        {
            User md = null;
            try
            {
                md = GetUser();
                Firm firm = md.Firm;
                ViewBag.Products = innerMng.ProductController.GetAll().Select(x => new SelectListItem { Text = $"{x.Name}, фирма {x.Distributor.Name}", Value = x.Id + "" });
                ViewBag.Warehouses = innerMng.WarehouseController.GetAll().Select(x => new SelectListItem { Text = x.Address, Value = x.Id + "" });
                ViewBag.Firms = innerMng.FirmController.GetAll().Where(x => x.Id != 0).ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost("/Firm/Contracts/Contract/Create")]
        public async Task<IActionResult> ContractCreate([FromForm] DateTime concl, [FromForm] DateTime expir, [FromForm] IFormFile document, [FromForm] IEnumerable<ContractPosElem> pos, [FromForm] int firmId = 0)
        {
            Contract newContr = null;
            try
            {
                User user = GetUser();
                Firm firm = (firmId != 0) ? innerMng.FirmController.Get(firmId) : user.Firm;
                newContr = new(-1, firm, null, null, null, null, concl, expir, document.FileName);
                innerMng.ContractController.Create(newContr);
                if (pos != null)
                {
                    foreach (var elem in pos)
                    {
                        var wh = innerMng.WarehouseController.Get(elem.Warehouse);
                        var prod = innerMng.ProductController.Get(elem.Product);
                        innerMng.ContractPosController.Create(new (newContr, wh, prod, elem.Amount));
                    }
                }
                innerMng.ContractController.Approve(newContr, user);
                newContr.Document = String.Format("{0}_{1}", newContr.Id, document.FileName);
                using (var fileStream = new FileStream(env.WebRootPath + "/files/" + newContr.Document, FileMode.Create))
                {
                    await document.CopyToAsync(fileStream);
                }
                innerMng.ContractController.Update(newContr);
                return Redirect(String.Format("/Firm/Contracts/Contract/{0}", newContr.Id));
            }
            catch (Exception)
            {
                if (newContr != null)
                {
                    innerMng.ContractController.Delete(newContr.Id);
                }
            }
            return Redirect("/Firm/Contracts");
        }

        [HttpPost("/Firm/Products/Delete/{id}")]
        public async Task<IActionResult> DelProduct([FromRoute] int id)
        {
            try
            {
                innerMng.ProductController.Delete(id);
            }
            catch (Exception) { }
            return Redirect("/Firm/Products");
        }

        [HttpGet("/Firm/Products/Product/Create")]
        public async Task<IActionResult> ProductCreate()
        {
            try
            {
                ViewBag.Producers = innerMng.ProducerController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
                ViewBag.Categories = innerMng.CategoryController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
            }
            catch (Exception) { }
            return View();
        }

        [HttpPost("/Firm/Products/Product/Create")]
        public async Task<IActionResult> ProductCreate([FromForm] string name, [FromForm] int categ, [FromForm] int cost, [FromForm] int producer)
        {
            try
            {
                User user = GetUser();
                Category c = innerMng.CategoryController.Get(categ);
                Producer produce = innerMng.ProducerController.Get(producer);
                Product prod = new(-1, name, c, user.Firm, cost, produce);
                innerMng.ProductController.Create(prod);
                return Redirect(String.Format("/Firm/Products/Product/{0}", prod.Id));
            }
            catch (Exception) { }
            return Redirect("/Firm/Products");
        }

        [HttpGet("/Firm/Products/Product/{id}")]
        public async Task<IActionResult> Product([FromRoute] int id)
        {
            ProductWithWareProd md = new();
            try
            {
                md.Product = innerMng.ProductController.Get(id);
                md.Data = innerMng.WareProdController.GetAll().Where(x => x.Product.Id == id).ToList();
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost]
        [Route("/Firm/Products/ProductEdit/{id}")]
        public async Task<IActionResult> ProductEdit([FromRoute] int id,
                                             [FromForm] string name,
                                             [FromForm] int categ,
                                             [FromForm] int cost,
                                             [FromForm] int producer)

        {
            if (innerCtrl == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                Product src = innerMng.ProductController.Get(id);
                Producer produce = innerMng.ProducerController.Get(producer); 
                src.Name = name;
                src.Producer = produce;
                src.Cost = cost;
                src.Category = innerMng.CategoryController.Get(categ);
                innerMng.ProductController.Update(src);
                return Redirect(String.Format("/Firm/Products/Product/{0}", id));
            }
            catch (LogicException e)
            {
                Product md = innerMng.ProductController.Get(id);
                ViewData["Error"] = e.ToString();
                ViewBag.Categories = innerMng.CategoryController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "" });
                return View("ProductEdit", md);
            }
        }

        [HttpGet]
        [Route("/Firm/Products/ProductEdit/{id}")]
        public async Task<IActionResult> ProductEdit([FromRoute] int id)
        {
            Product md = null;
            try
            {
                User user = GetUser();
                md = innerMng.ProductController.Get(id);
                ViewBag.Producers = innerMng.ProducerController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (x.Id == md.Producer.Id)});
                ViewBag.Categories = innerMng.CategoryController.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id + "", Selected = (x.Id == md.Category.Id)});
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpGet("/Firm/Products/Product/{id}/Add")]
        public async Task<IActionResult> ProductAdd([FromRoute] int id)
        {
            Product md = null;
            try
            {
                md = innerMng.ProductController.Get(id);
                ViewBag.Warehouses = innerMng.WarehouseController.GetAll().Select(x => new SelectListItem { Text = x.Address, Value = x.Id + "" });
            }
            catch (Exception) { }
            return View(md);
        }

        [HttpPost("/Firm/Products/Product/{id}/Add")]
        public async Task<IActionResult> ProductAdd([FromRoute] int id, [FromForm] int wh, [FromForm] int amount)
        {
            try
            {
                WareProd wp = null;
                try
                {
                    wp = innerMng.WareProdController.Get(wh, id);
                    wp.Amount += amount;
                    innerMng.WareProdController.Update(wp);
                }
                catch (Exception)
                {
                    Product prod = innerMng.ProductController.Get(id);
                    Warehouse warehouse = innerMng.WarehouseController.Get(wh);
                    wp = new(warehouse, prod, amount);
                    innerMng.WareProdController.Create(wp);
                }
            }
            catch (Exception) { }
            return Redirect(String.Format("/Firm/Products/Product/{0}", id));
        }
    }
}
