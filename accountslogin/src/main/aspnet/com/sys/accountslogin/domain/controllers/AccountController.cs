using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using accountslogin.Data;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using Microsoft.AspNetCore.Http;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers
{
    public abstract class AccountController : ControllerBase
    {
        // Create a new account
        public abstract Task<IActionResult> Create(IFormCollection formData);

        // Retrieve an account by ID
        public abstract Task<IActionResult> Get(string accountId);

        // Retrieve a list of accounts
        public abstract Task<IActionResult> Retrieve();

        // Update an existing account
        public abstract Task<IActionResult> Update(string accountId, IFormCollection formData);

        // Delete an account
        public abstract Task<IActionResult> Delete(string accountId);

        // Filter accounts based on criteria
        public abstract Task<IActionResult> Filter(IFormCollection formData);

        // Get account by username and password
        public abstract Task<IActionResult> GetByAccountUsernamePassword([FromForm] string username_00, [FromForm] string password_01, [FromForm] string Limit);

        // Get account by username
        public abstract Task<IActionResult> GetByAccountUsername(string username);

        // Get account by additional filtering criteria
        public abstract Task<IActionResult> GetByAccount(string accountId);
    }
}
