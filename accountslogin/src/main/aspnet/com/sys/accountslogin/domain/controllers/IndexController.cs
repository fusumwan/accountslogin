using accountslogin.Data;
using accountslogin.sandbox;
using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using accountslogin.src.main.aspnet.com.sys.accountslogin.presentation.viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.controllers
{
    public class IndexController : Controller
    {
        private readonly ILogger<IndexController> _logger;
        private readonly ApplicationProperties _applicationProperties;
        private readonly ApplicationDbContext _context;
        private crudtest _crudtestaction;

        public IndexController(ILogger<IndexController> logger, ApplicationProperties applicationProperties, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _applicationProperties = applicationProperties ?? throw new ArgumentNullException(nameof(applicationProperties));
            //_crudtestaction = new crudtest(logger, context);
            JsonConvertTest jsonConvertTest = new JsonConvertTest();
            jsonConvertTest.test();
        }

        public async Task<IActionResult> Index()
        {
            // Default layout logic here
            ViewBag.Layout = "Home";
            try
            {
                // Ensuring the ApplicationProperties and context are not null before accessing
                if (_applicationProperties == null || _context == null)
                {
                    _logger.LogError("Application properties or DB context is null.");
                    return View("Error");
                }


                //await _crudtestaction.unit_testAsync();
                return View("IndexContent");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in the HomeController.Index method: {ex}");
                return View("Error");
            }
        }

        public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in the HomeController.Privacy method: {ex}");
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            try
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in the HomeController.Error method: {ex}");
                return View("CriticalError");
            }
        }
    }
}
