using Microsoft.AspNetCore.Mvc;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{
    public class AccountinfoController : Controller
    {

        public IActionResult Accountinfo()
        {
            // Default layout logic here
            ViewBag.Layout = "Home";
            return View("AccountinfoContent");
		}
    }
}
