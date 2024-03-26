using InstrumentationAccountingSystem.BusinessLogic.Interfaces;
using InstrumentationAccountingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;
//using Microsoft.IdentityModel.Tokens;
//using System.Runtime.CompilerServices;
using InstrumentationAccountingSystem.Common.Dto;
//using InstrumentationAccountingSystem.BusinessLogic.Services;
//using static Azure.Core.HttpHeader;
//using InstrumentationAccountingSystem.BusinessLogic.Services;


namespace InstrumentationAccountingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ITypeService _typeService;
        private readonly ILocationService _locationService;
        private readonly IInstrumentationService _instrumentationService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ITypeService typeService, ILocationService locationService, IInstrumentationService instrumentationService)
        {
            _logger = logger;
            _userService = userService;
            _typeService = typeService;
            _locationService = locationService;
            _instrumentationService = instrumentationService;
        }

        public IActionResult Index()
        {
            User? user = null;
            List<Instrumentation> instrumentations = new List<Instrumentation> { };
            List<InstrumentationAccountingSystem.Models.Type> types = new List<InstrumentationAccountingSystem.Models.Type> { };
            List<Location> locations = new List<Location> { };

            instrumentations = _instrumentationService.GetAll();
            types = _typeService.GetAll();
            locations = _locationService.GetAll();

            //IEnumerable

            //int? UserId = Convert.ToInt32(User.FindFirst("UserId")?.Value); // Auth
            // if UserId != null
            //Auth!!!!
            // filter!

            HomeModel homeModel = new HomeModel
            {
                User = user,
                Instrumentations = instrumentations,
                Types = types,
                Locations = locations
            };
            return View(homeModel);
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

                return RedirectToAction("CreateType");
            }
            return View(typeCreateDto);
        }

        public IActionResult EditType(int id)
        {
            InstrumentationAccountingSystem.Models.Type? type = _typeService.GetTypeById(id);

            return View(type);
        }
        [HttpPost]
        public IActionResult EditType(InstrumentationAccountingSystem.Models.Type type)
        {
            _typeService.EditType(type);
            return RedirectToAction("CreateType");
        }

        [HttpPost]
        public IActionResult DeleteTypeById(int id)
        {
            InstrumentationAccountingSystem.Models.Type? type = _typeService.GetTypeById(id);
            _typeService.DeleteTypeById(id);
            return RedirectToAction("CreateType");
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

                return RedirectToAction("CreateLocation");
            }
            return View(locationCreateDto);
        }

        public IActionResult EditLocation(int id)
        {
            Location? location = _locationService.GetLocationById(id);

            return View(location);
        }
        [HttpPost]
        public IActionResult EditLocation(Location location)
        {
            _locationService.EditLocation(location);
            return RedirectToAction("CreateLocation");
        }

        [HttpPost]
        public IActionResult DeleteLocationById(int id)
        {
            Location location = _locationService.GetLocationById(id);
            _locationService.DeleteLocationById(id);
            return RedirectToAction("CreateLocation");
        }

        public IActionResult CreateInstrumentation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInstrumentation(InstrumentationCreateDto instrumentationCreateDto)
        {
            if (ModelState.IsValid)
            {
                _instrumentationService.Create(instrumentationCreateDto);

                return RedirectToAction("Index");
            }
            return View(instrumentationCreateDto);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
