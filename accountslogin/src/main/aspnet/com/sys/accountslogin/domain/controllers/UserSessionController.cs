using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{
    // Abstract class that defines methods to be implemented by subclasses.
    public abstract class UserSessionController : ControllerBase
    {
        // Define an abstract method equivalent to the FastAPI's get_usersession
        public abstract Task<IActionResult> GetUsersession();

        // Define an abstract method equivalent to the FastAPI's set_usersession
        public abstract Task<IActionResult> SetUsersession([FromBody] Account user);

        // Define an abstract method equivalent to the FastAPI's get_pagesession
        public abstract Task<IActionResult> GetPagesession();

        // Define an abstract method equivalent to the FastAPI's set_pagesession
        public abstract Task<IActionResult> SetPagesession([FromBody] PageSession pageSession);

        // Define an abstract method equivalent to the FastAPI's get_profile
        public abstract Task<IActionResult> GetProfile();
    }
}
