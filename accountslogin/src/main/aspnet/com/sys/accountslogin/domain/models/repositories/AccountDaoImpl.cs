using accountslogin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.repositories;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.repositories
{
    public class AccountDaoImpl : IAccountDao
    {
        private readonly ApplicationDbContext _context;

        public AccountDaoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetPageAccounts(int page, int pageSize, List<string> orderbycolumns)
        {
            var query = _context.Accounts.AsQueryable();
            foreach (string? column in orderbycolumns)
            {   
                if (column != null) {

                    query = DynamicOrderBy(query, column); // Assuming OrderBy can directly accept string column names
                }
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        // Example implementation for DynamicOrderBy
        private IQueryable<Account> DynamicOrderBy(IQueryable<Account> query, string columnName)
        {
            // This method needs to be implemented based on your specific requirements and ORM capabilities
            // Example using reflection and EF Core
            return query.OrderBy(x => EF.Property<object>(x, columnName));
        }
        

        public async Task<int> CountAccounts()
        {
            return await _context.Accounts.CountAsync();
        }

        public async Task SaveAccount(Account account)
        {
            if (account.account_id == default)
            {
                _context.Add(account);
            }
            else
            {
                _context.Update(account);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            if (accounts == null)
            {
                throw new HttpRequestException("Account not found", null, System.Net.HttpStatusCode.NotFound);
            }
            return accounts;
        }

        public async Task<Account> GetAccount(string account_id)
        {
            var account = await _context.Accounts.FindAsync(account_id);
            return account;
        }

        public async Task DeleteAccount(string account_id)
        {
            var account = await GetAccount(account_id);
            if (account != null)
            {
                object value1 = _context.Accounts.Remove(account);
                object value2 = _context.SaveChangesAsync();
            }
        }

        public async Task<Account> FindByaccount_id(string account_id)
        {
            return await _context.Accounts.FindAsync(account_id);
        }

        public async Task<IEnumerable<Account>> FindByPassword(string password)
        {
            // This query handles both cases where `a.password` or the input `password` is null.
            // It compares the passwords if both are not null, or checks for both being null.
            return await _context.Accounts.Where(a => (password == null && a.password == null) || (a.password != null && a.password.Equals(password))).ToListAsync();
        }

        public async Task<IEnumerable<Account>> FindByFirstName(string firstName)
        {
            return await _context.Accounts.Where(a => a.first_name == firstName).ToListAsync();
        }

        public async Task<IEnumerable<Account>> FindByLastName(string lastName)
        {
            return await _context.Accounts.Where(a => a.last_name == lastName).ToListAsync();
        }

        public async Task<IEnumerable<Account>> FindByUsername(string username)
        {
            return await _context.Accounts.Where(a => a.username == username).ToListAsync();
        }

        public async Task<IEnumerable<Account>> FindByEmail(string email)
        {
            return await _context.Accounts.Where(a => a.email == email).ToListAsync();
        }

        public async Task<IEnumerable<Account>> FindByContactNumber(string contactNumber)
        {
            return await _context.Accounts.Where(a => a.contact_number == contactNumber).ToListAsync();
        }

        public async Task<IEnumerable<Account>> FindByUserType(string userType)
        {
            return await _context.Accounts.Where(a => a.user_type == userType).ToListAsync();
        }

        public async Task<IEnumerable<Account>> FilterAccount(Dictionary<string, object> filters)
        {
            var query = _context.Accounts.AsQueryable();
            foreach (var filter in filters)
            {
                query = query.Where(a => EF.Property<object>(a, filter.Key) == filter.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<Account> GetByAccountUsernamePassword(string username, string password)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.username == username && a.password == password);

            return account;
        }

        public async Task<Account> GetByAccountUsername(string username)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.username == username);
            return account;
        }
        
    }
}

