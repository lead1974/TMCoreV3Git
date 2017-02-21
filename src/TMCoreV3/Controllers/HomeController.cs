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
    public class HomeController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private readonly IMailService _emailSender;
        private readonly ISmsService _smsSender;
        private readonly ILogger _logger;
        private TMDbContext _TMDbContext;
        private ICustomerCouponRepository _customerCouponRepo;

        public HomeController(
            TMDbContext dmContext,
            ICustomerCouponRepository customerCouponRepo,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            IMailService emailSender,
            ISmsService smsSender,
            ILoggerFactory loggerFactory)
        {            
            _TMDbContext = dmContext;
            _customerCouponRepo = customerCouponRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        [SelectedTabFilter("home")]
        public IActionResult Index()
        {
            ViewBag.SelectiveTab = "home";
            return View();
        }

        [SelectedTabFilter("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewBag.SelectiveTab = "about";
            return View();
        }

        [SelectedTabFilter("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            ViewBag.SelectiveTab = "contact";
            return View();
        }

        public IActionResult GetCustomerCoupons([DataSourceRequest] DataSourceRequest request)
        {
            var customerCoupons = _customerCouponRepo.GetAllNonExpired().ToList();
            return Json(customerCoupons.ToDataSourceResult(request));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
