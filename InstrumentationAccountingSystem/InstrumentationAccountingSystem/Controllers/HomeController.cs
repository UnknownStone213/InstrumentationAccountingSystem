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
    [Authorize(Roles = "Admin, Member")]
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

        [AllowAnonymous]
        public ActionResult Index(int? typeId, string? model, string? factoryNumber, string? locationName, string? sortName, string? checkMonth)
        {
            if (sortName != null)
            {
                HttpContext.Session.SetString("SortName", sortName);
                if (HttpContext.Session.GetString("sortCheck") == "0" || HttpContext.Session.GetString("sortCheck") == null)
                {
                    HttpContext.Session.SetString("sortCheck", "1");
                }
                else
                {
                    HttpContext.Session.SetString("sortCheck", "0");
                }
            }

            ViewData["UserId"] = _userManager.GetUserId(User);
            ViewData["TypeId"] = typeId;
            ViewData["Model"] = model;
            ViewData["FactoryNumber"] = factoryNumber;
            ViewData["LocationName"] = locationName;
            ViewData["checkMonth"] = checkMonth;

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
                if ((typeId == null || item.TypeId == typeId) && ((model == null) || (item.Model != null && item.Model.Equals(model, StringComparison.OrdinalIgnoreCase))) && ((factoryNumber == null) || (item.FactoryNumber != null && item.FactoryNumber.Equals(factoryNumber, StringComparison.OrdinalIgnoreCase))) && ((locationName == null) || (locations.FirstOrDefault(u => u.Id == item.LocationId).Name.Contains(locationName, StringComparison.OrdinalIgnoreCase))) && ((checkMonth == null) || (checkMonth == "checkMonthTrue") && ((_verificationService.GetLastVerificationByInstrumentationId(item.Id)?.Date.ToDateTime(TimeOnly.MinValue).AddMonths(item.Frequency) ?? DateTime.MinValue) < DateTime.Now.AddDays(30))))
                {
                    instrumentations.Add(item);
                }
            }

            //sorting
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
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.TypeId).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.TypeId).ToList();
                    }
                    break;
                case "Модель":
                    instrumentations = instrumentations.OrderBy(u => u.Model).ToList();
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.Model).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.Model).ToList();
                    }
                    break;
                case "Заводской номер":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.FactoryNumber).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.FactoryNumber).ToList();
                    }
                    break;
                case "Место установки":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.LocationId).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.LocationId).ToList();
                    }
                    break;
                case "Пределы измерений":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.MeasurementLimits).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.MeasurementLimits).ToList();
                    }
                    break;
                case "Дата последней поверки":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        Dictionary<int, DateTime> lastVerifications = new Dictionary<int, DateTime>();
                        foreach (var item2 in instrumentations)
                        {
                            lastVerifications.Add(item2.Id, _verificationService.GetLastVerificationByInstrumentationId(item2.Id)?.Date.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue);
                        }
                        lastVerifications = lastVerifications.OrderBy(u => u.Value).ToDictionary();
                        List<int> sortIds = new List<int>();
                        foreach (var item2 in lastVerifications)
                        {
                            sortIds.Add(item2.Key);
                        }
                        List<Instrumentation> instrumentations2 = new List<Instrumentation>();
                        foreach (var item2 in sortIds)
                        {
                            instrumentations2.Add(instrumentations.First(o => item2 == o.Id));
                        }
                        instrumentations = instrumentations2;
                    }
                    else
                    {
                        Dictionary<int, DateTime> lastVerifications = new Dictionary<int, DateTime>();
                        foreach (var item2 in instrumentations)
                        {
                            lastVerifications.Add(item2.Id, _verificationService.GetLastVerificationByInstrumentationId(item2.Id)?.Date.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue);
                        }
                        lastVerifications = lastVerifications.OrderByDescending(u => u.Value).ToDictionary();
                        List<int> sortIds = new List<int>();
                        foreach (var item2 in lastVerifications)
                        {
                            sortIds.Add(item2.Key);
                        }
                        List<Instrumentation> instrumentations2 = new List<Instrumentation>();
                        foreach (var item2 in sortIds)
                        {
                            instrumentations2.Add(instrumentations.First(o => item2 == o.Id));
                        }
                        instrumentations = instrumentations2;
                    }
                    break;
                case "Периодичность измерений":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.Frequency).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.Frequency).ToList();
                    }
                    break;
                case "Дата очередной поверки":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        Dictionary<int, DateTime> nextVerifications = new Dictionary<int, DateTime>();
                        foreach (var item2 in instrumentations)
                        {
                            nextVerifications.Add(item2.Id, _verificationService.GetLastVerificationByInstrumentationId(item2.Id)?.Date.ToDateTime(TimeOnly.MinValue).AddMonths(item2.Frequency) ?? DateTime.MinValue);
                        }
                        nextVerifications = nextVerifications.OrderBy(u => u.Value).ToDictionary();
                        List<int> sortIds = new List<int>();
                        foreach (var item2 in nextVerifications)
                        {
                            sortIds.Add(item2.Key);
                        }
                        List<Instrumentation> instrumentations2 = new List<Instrumentation>();
                        foreach (var item2 in sortIds)
                        {
                            instrumentations2.Add(instrumentations.First(o => item2 == o.Id));
                        }
                        instrumentations = instrumentations2;
                    }
                    else
                    {
                        Dictionary<int, DateTime> lastVerifications = new Dictionary<int, DateTime>();
                        foreach (var item2 in instrumentations)
                        {
                            lastVerifications.Add(item2.Id, _verificationService.GetLastVerificationByInstrumentationId(item2.Id)?.Date.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue);
                        }
                        lastVerifications = lastVerifications.OrderByDescending(u => u.Value).ToDictionary();
                        List<int> sortIds = new List<int>();
                        foreach (var item2 in lastVerifications)
                        {
                            sortIds.Add(item2.Key);
                        }
                        List<Instrumentation> instrumentations2 = new List<Instrumentation>();
                        foreach (var item2 in sortIds)
                        {
                            instrumentations2.Add(instrumentations.First(o => item2 == o.Id));
                        }
                        instrumentations = instrumentations2;
                    }
                    break;
                case "Присоединение к процессу":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.Connection).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.Connection).ToList();
                    }
                    break;
                case "Примечание":
                    if (HttpContext.Session.GetString("sortCheck") == "1")
                    {
                        instrumentations = instrumentations.OrderBy(u => u.Comment).ToList();
                    }
                    else
                    {
                        instrumentations = instrumentations.OrderByDescending(u => u.Comment).ToList();
                    }
                    break;
                default:
                    break;
            }

            HomeModel homeModel = new HomeModel
            {
                User = user,
                Instrumentations = instrumentations,
                Types = types,
                Locations = locations,
                Verifications = verifications
            };
            return View(homeModel);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult EditVerification(int id)
        {
            Verification? verification = _verificationService.GetVerificationById(id);

            return View(verification);
        }
        [HttpPost]
        public ActionResult EditVerification(Verification verification)
        {
            _verificationService.EditVerification(verification);
            //return RedirectToAction("EditInstrumentation", verification.InstrumentationId);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteVerificationById(int id)
        {
            Verification? verification = _verificationService.GetVerificationById(id);
            _verificationService.DeleteVerificationById(id);
            return RedirectToAction("CreateVerification");
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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
