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
using InstrumentationAccountingSystem.BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;
using InstrumentationAccountingSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//using InstrumentationAccountingSystem.BusinessLogic.Services;
//using static Azure.Core.HttpHeader;
//using InstrumentationAccountingSystem.BusinessLogic.Services;

namespace InstrumentationAccountingSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ITypeService _typeService;
        private readonly ILocationService _locationService;
        private readonly IInstrumentationService _instrumentationService;
        private readonly IVerificationService _verificationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ITypeService typeService, ILocationService locationService, IInstrumentationService instrumentationService, IVerificationService verificationService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userService = userService;
            _typeService = typeService;
            _locationService = locationService;
            _instrumentationService = instrumentationService;
            _verificationService = verificationService;
            _userManager = userManager;
        }

        public ActionResult Index(int? typeId, string? model, string? factoryNumber, string? locationName, string? sortName)
        {
            //HttpContext.Session.SetInt32("TypeId", typeId ?? 0);
            //HttpContext.Session.SetString("Model", model ?? "");
            if (sortName == HttpContext.Session.GetString("SortName"))
            {
                if (HttpContext.Session.GetString("sortCheck") == "0" || HttpContext.Session.GetString("sortCheck") == null)
                {
                    HttpContext.Session.SetString("sortCheck", "1");
                }
                else
                {
                    HttpContext.Session.SetString("sortCheck", "0");
                }
            }
            if (sortName != null)
            {
                HttpContext.Session.SetString("SortName", sortName);
            }

            //User.
            //_userManager.
            ViewData["UserId"] = _userManager.GetUserId(User);
            ViewData["TypeId"] = typeId;
            ViewData["Model"] = model;
            ViewData["FactoryNumber"] = factoryNumber;
            ViewData["LocationName"] = locationName;

            User? user = null;
            List<Instrumentation> instrumentations = new List<Instrumentation> { };
            List<InstrumentationAccountingSystem.Models.Type> types = new List<InstrumentationAccountingSystem.Models.Type> { };
            List<Location> locations = new List<Location> { };
            List<Verification> verifications = new List<Verification> { };

            types = _typeService.GetAll();
            locations = _locationService.GetAll();
            verifications = _verificationService.GetAll();
            foreach (var item in _instrumentationService.GetAll())
            {
                if ((typeId == null || item.TypeId == typeId) && ((model == null) || (item.Model != null && item.Model.Equals(model, StringComparison.OrdinalIgnoreCase))) && ((factoryNumber == null) || (item.FactoryNumber != null && item.FactoryNumber.Equals(factoryNumber, StringComparison.OrdinalIgnoreCase))) && ((locationName == null) || (locations.FirstOrDefault(u => u.Id == item.LocationId).Name.Contains(locationName,StringComparison.OrdinalIgnoreCase))))
                {
                    instrumentations.Add(item);
                }
            }

            //sorting
            foreach (var item in instrumentations)
            {
                switch (HttpContext.Session.GetString("SortName"))
                {
                    case "Id":
                        if (HttpContext.Session.GetString("sortCheck") == "1")
                        {
                            instrumentations = instrumentations.OrderBy(u => u.Id).ToList();
                        }
                        else
                        {
                            instrumentations = instrumentations.OrderByDescending(u => u.Id).ToList();
                        }
                        break;
                    case "Тип":
                        instrumentations = instrumentations.OrderBy(u => u.TypeId).ToList();
                        break;
                    case "Модель":
                        instrumentations = instrumentations.OrderBy(u => u.Model).ToList();
                        break;
                    case "Заводской номер":
                        instrumentations = instrumentations.OrderBy(u => u.FactoryNumber).ToList();
                        break;
                    case "Место установки":
                        instrumentations = instrumentations.OrderBy(u => u.LocationId).ToList();
                        break;
                    case "Пределы измерений":
                        instrumentations = instrumentations.OrderBy(u => u.MeasurementLimits).ToList();
                        break;
                    case "Дата последней поверки":
                        // 111111111111111111111111
                        break;
                    case "Периодичность измерений":
                        instrumentations = instrumentations.OrderBy(u => u.Frequency).ToList();
                        break;
                    case "Дата очередной поверки":
                        // 1111111111111111111111
                        break;
                    case "Присоединение к процессу":
                        instrumentations = instrumentations.OrderBy(u => u.Connection).ToList();
                        break;
                    case "Примечание":
                        instrumentations = instrumentations.OrderBy(u => u.Comment).ToList();
                        break;
                    default:
                        break;
                }
            }

            //int? UserId = Convert.ToInt32(User.FindFirst("UserId")?.Value); // Auth
            // if UserId != null
            //Auth!!!!
            // filter!

            HomeModel homeModel = new HomeModel
            {
                User = user,
                Instrumentations = instrumentations,
                Types = types,
                Locations = locations,
                Verifications = verifications
            };
            Response.Cookies.Append("", "");
            return View(homeModel);
        }

        public ActionResult CreateInstrumentation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInstrumentation(InstrumentationCreateDto instrumentationCreateDto)
        {
            if (ModelState.IsValid)
            {
                _instrumentationService.Create(instrumentationCreateDto);

                return RedirectToAction("Index");
            }
            return View(instrumentationCreateDto);
        }

        public ActionResult EditInstrumentation(int id)
        {
            Instrumentation? instrumentation = _instrumentationService.GetInstrumentationById(id);

            return View(instrumentation);
        }
        [HttpPost]
        public ActionResult EditInstrumentation(Instrumentation instrumentation)
        {
            _instrumentationService.Edit(instrumentation);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteInstrumentationById(int id)
        {
            Instrumentation? instrumentation = _instrumentationService.GetInstrumentationById(id);
            _instrumentationService.DeleteInstrumentationById(id);
            return RedirectToAction("Index");
        }

        public ActionResult CreateVerification(int? instrumentationId)
        {
            ViewData["InstrumentationId"] = instrumentationId;
            return View();
        }

        [HttpPost]
        public ActionResult CreateVerification(VerificationCreateDto verificationCreateDto)
        {
            if (ModelState.IsValid)
            {
                _verificationService.Create(verificationCreateDto);

                return RedirectToAction("CreateVerification");
            }
            return View(verificationCreateDto);
        }

        public ActionResult EditVerification(int id)
        {
            Verification? verification = _verificationService.GetVerificationById(id);

            return View(verification);
        }
        [HttpPost]
        public ActionResult EditVerification(Verification verification)
        {
            _verificationService.EditVerification(verification);
            return RedirectToAction("EditInstrumentation", verification.InstrumentationId);
        }

        public ActionResult DeleteVerificationById(int id)
        {
            Verification? verification = _verificationService.GetVerificationById(id);
            _verificationService.DeleteVerificationById(id);
            return RedirectToAction("CreateVerification");
        }

        public ActionResult CreateType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateType(TypeCreateDto typeCreateDto)
        {
            if (ModelState.IsValid)
            {
                _typeService.Create(typeCreateDto);

                return RedirectToAction("CreateType");
            }
            return View(typeCreateDto);
        }

        public ActionResult EditType(int id)
        {
            InstrumentationAccountingSystem.Models.Type? type = _typeService.GetTypeById(id);

            return View(type);
        }
        [HttpPost]
        public ActionResult EditType(InstrumentationAccountingSystem.Models.Type type)
        {
            _typeService.EditType(type);
            return RedirectToAction("CreateType");
        }

        [HttpPost]
        public ActionResult DeleteTypeById(int id)
        {
            InstrumentationAccountingSystem.Models.Type? type = _typeService.GetTypeById(id);
            _typeService.DeleteTypeById(id);
            return RedirectToAction("CreateType");
        }


        public ActionResult CreateLocation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLocation(LocationCreateDto locationCreateDto)
        {
            if (ModelState.IsValid)
            {
                _locationService.Create(locationCreateDto);

                return RedirectToAction("CreateLocation");
            }
            return View(locationCreateDto);
        }

        public ActionResult EditLocation(int id)
        {
            Location? location = _locationService.GetLocationById(id);

            return View(location);
        }
        [HttpPost]
        public ActionResult EditLocation(Location location)
        {
            _locationService.EditLocation(location);
            return RedirectToAction("CreateLocation");
        }

        [HttpPost]
        public ActionResult DeleteLocationById(int id)
        {
            Location location = _locationService.GetLocationById(id);
            _locationService.DeleteLocationById(id);
            return RedirectToAction("CreateLocation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
