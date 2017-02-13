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
        private TMDbContext _TMDbContext;

        public ScheduleAppointmentController(
            TMDbContext dmContext,
            ICustomerApplianceTypeRepository customerApplianceTypeRepo,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            IMailService emailSender,
            ISmsService smsSender,
            ILoggerFactory loggerFactory)
        {
            _TMDbContext = dmContext;
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

        [HttpGet, Route("GetCustomerApplianceType")]
        public IActionResult GetCustomerApplianceType()
        {
            ViewBag.SelectiveTab = "scheduleappointment";
            var customerApplianceTypes = _customerApplianceTypeRepo.GetAllWithBrands().ToList();
            return Json(customerApplianceTypes);
        }
    }
}
