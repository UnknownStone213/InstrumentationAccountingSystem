using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using InstrumentationAccountingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InstrumentationAccountingSystem.Common.Dto;
using Microsoft.AspNetCore.Identity;
using InstrumentationAccountingSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace InstrumentationAccountingSystem.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
