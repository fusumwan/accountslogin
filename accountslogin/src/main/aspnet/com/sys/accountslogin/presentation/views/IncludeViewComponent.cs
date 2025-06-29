using Microsoft.AspNetCore.Mvc;
using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using System.Threading.Tasks;
using accountslogin.src.main.aspnet.com.sys.accountslogin.presentation.viewmodels;

/*
 By using View Components, I can gain more control over the data and behavior of the parts of my views that are independent of the main actions' concerns, achieving a more modular and clean architecture in your ASP.NET Core application.
 */
namespace accountslogin.src.main.aspnet.com.sys.accountslogin.presentation.views
{
    public class IncludeViewComponent : ViewComponent
    {
        private readonly ApplicationProperties _applicationProperties;

        public IncludeViewComponent(ApplicationProperties applicationProperties)
        {
            _applicationProperties = applicationProperties;
        }

        public IViewComponentResult Invoke()
        {
            if (_applicationProperties == null)
            {
                // Handle null case appropriately
                return Content("Configuration is missing.");
            }

            var model = new IncludeViewModel
            {
                ContextPath = _applicationProperties.ContextPath(),
                SecurityJwtEnable = _applicationProperties.SecurityJwtEnable,
                AppName = _applicationProperties.AppName(true)
            };
            return View("IncludeView", model);
        }

    }
}
