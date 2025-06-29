using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // For HttpRequest
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetPageAccounts(int page, int pageSize, List<string> orderbycolumns);
        Task<int> CountAccounts();
        Task SaveAccount(Account account);
        Task<IEnumerable<Account>> GetAccounts();
        Task<Account> GetAccount(string account_id);
        Task DeleteAccount(string account_id);
        Task<Account> FindByaccount_id(string account_id);
        Task<IEnumerable<Account>> FindByFirstName(string firstName);
        Task<IEnumerable<Account>> FindByLastName(string lastName);
        Task<IEnumerable<Account>> FindByUsername(string username);
        Task<IEnumerable<Account>> FindByEmail(string email);
        Task<IEnumerable<Account>> FindByContactNumber(string contactNumber);
        Task<IEnumerable<Account>> FindByPassword(string password);
        Task<IEnumerable<Account>> FindByUserType(string userType);
        Task<IEnumerable<Account>> FilterAccount(Dictionary<string, object> filters);
        Task<Account> GetByAccountUsernamePassword(string username, string password);
        Task<Account> GetByAccountUsername(string username);
    }
}
