using accountslogin.src.main.aspnet.com.sys.accountslogin.config;
using accountslogin.src.main.aspnet.com.sys.accountslogin.presentation.viewmodels;
using Microsoft.AspNetCore.Mvc;
namespace accountslogin.src.main.aspnet.com.sys.accountslogin.presentation.views
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ApplicationProperties _applicationProperties;
        public HeaderViewComponent(ApplicationProperties applicationProperties)
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

            var model = new HeaderViewModel
            {

            };
            return View("HeaderView", model);
        }
    }
}
