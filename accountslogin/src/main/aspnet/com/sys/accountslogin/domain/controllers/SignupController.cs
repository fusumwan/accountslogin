using Microsoft.AspNetCore.Mvc;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{
    public class SignupController : Controller
    {
        public IActionResult Signup()
        {
            // Default layout logic here
            ViewBag.Layout = "Home";
            return View("SignupContent");
        }
    }
}
