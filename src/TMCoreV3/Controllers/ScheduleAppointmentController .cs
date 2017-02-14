using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TMCoreV3.DataAccess.Models.User;
using TMCoreV3.Services;
using Microsoft.Extensions.Logging;
using TMCoreV3.DataAccess;
using TMCoreV3.DataAccess.Repos;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using TMCoreV3.ViewModels.CustomerViewModels;
using TMCoreV3.DataAccess.Models.Customer;

namespace TMCoreV3.Controllers
{
    public class ScheduleAppointmentController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private readonly IMailService _emailSender;
        private readonly ISmsService _smsSender;
        private readonly ILogger _logger;

        private ICustomerApplianceTypeRepository _customerApplianceTypeRepo;
        private ICustomerApplianceBrandRepository _customerApplianceBrandRepo;

        public ScheduleAppointmentController(
            ICustomerApplianceTypeRepository customerApplianceTypeRepo,
            ICustomerApplianceBrandRepository customerApplianceBrandRepo,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            IMailService emailSender,
            ISmsService smsSender,
            ILoggerFactory loggerFactory)
        {
            _customerApplianceBrandRepo = customerApplianceBrandRepo;
            _customerApplianceTypeRepo = customerApplianceTypeRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ScheduleAppointmentController>();
        }

        public IActionResult Index()
        {
            ViewBag.SelectiveTab = "scheduleappointment";
            return View();
        }

        [HttpPost]
        public IActionResult Index(ScheduleAppointment form, string returnUrl)
        {
            ViewBag.SelectiveTab = "scheduleappointment";
            if (ModelState.IsValid)
            {
                return RedirectToAction("index", "home");
            }                          

            return View(form);
        }

        [HttpGet, Route("GetCustomerApplianceType")]
        public IActionResult GetCustomerApplianceTypes()
        {
            var customerApplianceTypes = _customerApplianceTypeRepo.GetAll().ToList();
            return Json(customerApplianceTypes);
        }

        [HttpGet, Route("GetCustomerApplianceBrand")]
        public IActionResult GetCustomerApplianceBrands(int? customerApplianceTypes)
        {
            var customerApplianceBrands = _customerApplianceBrandRepo.GetAll().AsQueryable();
            if (customerApplianceTypes!=null)
            {
                customerApplianceBrands = customerApplianceBrands.Where(p =>  p.CustomerApplianceTypeId == customerApplianceTypes);
            }
            return Json(customerApplianceBrands);
        }

        [HttpGet, Route("GetStates")]
        public IActionResult GetStates()
        {
            var states = new[]
            {
                new State
                {
                    StateId=1,
                    StateName="CA"
                },
                new State
                {
                    StateId = 2,
                    StateName = "NV"
                }
            };
            return Json(states);
        }
    }
}
