using accountslogin.Data;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.repositories;
using Microsoft.Identity.Client;
using static NuGet.Packaging.PackagingConstants;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.services
{
    public class AccountServiceImpl : IAccountService
    {
        IAccountDao? accountDao = null;
        public AccountServiceImpl(ApplicationDbContext context)
        {
            this.accountDao = new AccountDaoImpl(context);
        }

        public Task<IEnumerable<Account>> GetPageAccounts(int page, int pageSize, List<string> orderbycolumns)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            throw new NotImplementedException();
        }

        public Task<int> CountAccounts()
        {
            if (this.accountDao == null)
            {
                // Return a completed task with a default value, such as 0, when accountDao is null.
                // This avoids the method ever returning null, aligning with the Task-based asynchronous pattern.
                return Task.FromResult(0);
            }

            return this.accountDao.CountAccounts();
        }

        public Task SaveAccount(Account account)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with a default value, such as 0, when accountDao is null.
                // This avoids the method ever returning null, aligning with the Task-based asynchronous pattern.
                return Task.FromResult(0);
            }
            return this.accountDao.SaveAccount(account);
        }

        public Task DeleteAccount(string account_id)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with a default value, such as 0, when accountDao is null.
                // This avoids the method ever returning null, aligning with the Task-based asynchronous pattern.
                return Task.FromResult(0);
            }
            return this.accountDao.DeleteAccount(account_id);
        }
        

        public Task<IEnumerable<Account>> GetAccounts()
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            throw new NotImplementedException();
        }
        public Task<Account> GetAccount(string account_id)
        {

            throw new NotImplementedException();
        }
        public Task<IEnumerable<Account>> FilterAccount(Dictionary<string, object> filters)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FilterAccount(filters);
        }


        public Task<Account> FindByaccount_id(string account_id)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<Account>(new Account());
            }
            return this.accountDao.FindByaccount_id(account_id);
        }

        public Task<IEnumerable<Account>> FindByContactNumber(string contactNumber)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FindByContactNumber(contactNumber);
        }

        public Task<IEnumerable<Account>> FindByEmail(string email)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FindByEmail(email);
        }

        public Task<IEnumerable<Account>> FindByFirstName(string firstName)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FindByFirstName(firstName);
        }

        public Task<IEnumerable<Account>> FindByLastName(string lastName)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FindByLastName(lastName);
        }

        public Task<IEnumerable<Account>> FindByPassword(string password)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FindByPassword(password);
        }

        public Task<IEnumerable<Account>> FindByUsername(string username)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FindByUsername(username);
        }

        public Task<IEnumerable<Account>> FindByUserType(string userType)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<IEnumerable<Account>>(new List<Account>());
            }
            return this.accountDao.FindByUserType(userType);
        }

        public Task<Account> GetByAccountUsernamePassword(string username, string password)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<Account>(new Account());
            }
            return this.accountDao.GetByAccountUsernamePassword(username, password);
        }

        public Task<Account> GetByAccountUsername(string username)
        {
            if (this.accountDao == null)
            {
                // Return a completed task with an empty IEnumerable<Account> when accountDao is null.
                // This ensures the return type is always correct and avoids returning null.
                return Task.FromResult<Account>(new Account());
            }
            return this.accountDao.GetByAccountUsername(username);
        }
    }
}
