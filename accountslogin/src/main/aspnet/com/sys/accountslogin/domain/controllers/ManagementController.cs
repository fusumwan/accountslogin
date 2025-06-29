using Microsoft.AspNetCore.Mvc;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{
    public class ManagementController : Controller
    {
        public IActionResult Management()
        {
            // Default layout logic here
            ViewBag.Layout = "Home";
            return View("ManagementContent");
        }
    }
}
