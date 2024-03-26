using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using InstrumentationAccountingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;
//using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using InstrumentationAccountingSystem.Common.Dto;


namespace InstrumentationAccountingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ITypeService _typeService;
        private readonly ILocationService _locationService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ITypeService typeService, ILocationService locationService)
        {
            _logger = logger;
            _userService = userService;
            _typeService = typeService;
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            IEnumerable<Instrumentation> Instrumentations = new List<Instrumentation> { };
            User? user = null;

            //int? UserId = Convert.ToInt32(User.FindFirst("UserId")?.Value); // Auth

            //Auth!!!!
            //Instrumentations = _noteService.GetNotes

            return View();
        }

        public IActionResult CreateType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateType(TypeCreateDto typeCreateDto)
        {
            if (ModelState.IsValid)
            {
                _typeService.Create(typeCreateDto);

                return RedirectToAction("Index", "Home");
            }
            return View(typeCreateDto);
        }

        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLocation(LocationCreateDto locationCreateDto)
        {
            if (ModelState.IsValid)
            {
                _locationService.Create(locationCreateDto);

                return RedirectToAction("Index", "Home");
            }
            return View(locationCreateDto);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
