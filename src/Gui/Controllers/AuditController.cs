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
    [Authorize(Roles = "Auditor")]
    public class AuditController : Controller
    {
        private static ControllerManager innerMng;

        public static void SetInnerManager(ControllerManager mng)
        {
            innerMng = mng;
        }

        [HttpGet]
        public async Task<IActionResult> Contracts()
        {
            try
            {
                ViewBag.Contracts = innerMng.ContractController.GetAll();
            }
            catch (Exception) { }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Contract([FromRoute] int id)
        {
            ContractWithPositions md = new();
            try
            {
                md.Contract = innerMng.ContractController.Get(id);
                md.Positions = innerMng.ContractController.GetContent(md.Contract);
            }
            catch (Exception) { }
            return View(md);
        }
    }
}