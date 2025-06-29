using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using accountslogin.Data;
using Microsoft.EntityFrameworkCore;
using System;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using System.Diagnostics;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.repositories;
using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.controllers;
using accountslogin.src.main.aspnet.com.sys.accountslogin.controllers;


namespace accountslogin.sandbox
{
    public class crudtest
    {
        private readonly ILogger<IndexController> _logger;
        private ApplicationDbContext _context;
        private ApplicationDbContext context;

        public crudtest(ILogger<IndexController> logger,ApplicationDbContext context) {
            _context=context;
            _logger=logger;

        }

        public crudtest(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task getPageAccounts()
        {
            var page = 1;
            var pageSize = 3;
            var orderbycolumns = new List<string> { "username" };
            var accounts = await new AccountDaoImpl(_context).GetPageAccounts(page, pageSize, orderbycolumns);
            _logger.LogInformation("getAccounts: <Start print result>");
            foreach (var account in accounts)
            {
                _logger.LogInformation(account.ToJsonString());
            }
            _logger.LogInformation("getAccounts: <End print result>");
        }

        public async Task countAccounts()
        {
            var count = await new AccountDaoImpl(_context).CountAccounts();
            _logger.LogInformation("countAccounts: <Start print result>");
            _logger.LogInformation(count.ToString());
            _logger.LogInformation("countAccounts: <End print result>");
        }

        public async Task saveAccount(Account account)
        {
            _logger.LogInformation($"saveAccount: <Start print result>");
            await new AccountDaoImpl(_context).SaveAccount(account);
            _logger.LogInformation(account.ToJsonString());
            _logger.LogInformation($"saveAccount: <End print result>");
        }

        public async Task getAccounts()
        {
            var accounts = await new AccountDaoImpl(_context).GetAccounts();
            _logger.LogInformation("getAccounts: <Start print result>");
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("getAccounts: <End print result>");
        }

        public async Task<bool> getAccount(string account_id)
        {

            _logger.LogInformation("getAccount: <Start print result>");
            _logger.LogInformation(account_id.ToString());
            Task<Account> accountTask = new AccountDaoImpl(_context).GetAccount(account_id.ToString());
            Account account = await accountTask;
            if (account != null)
            {
                _logger.LogInformation(account.ToJsonString());
                _logger.LogInformation("getAccount: <End print result>");
                return true;
            }
            else
            {
                _logger.LogInformation("getAccount: <End print result>");
                return false;
            }
        }

        public async Task deleteAccount(Account account)
        {
            _logger.LogInformation("deleteAccount: <Start print result>");
            _logger.LogInformation(account.account_id.ToString());
            await new AccountDaoImpl(_context).DeleteAccount(account.account_id);
            if (await getAccount(account.account_id))
            {
                _logger.LogInformation($"Record exists: {account.account_id}");
            }
            else
            {
                _logger.LogInformation($"Record deleted: {account.account_id}");
            }
            _logger.LogInformation("deleteAccount: <End print result>");
        }

        public async Task findByFirstName(string value)
        {
            _logger.LogInformation("findByFirstName: <Start print result>");
            var accounts = await new AccountDaoImpl(_context).FindByFirstName(value);
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("findByFirstName: <End print result>");
        }

        public async Task findByLastName(string value)
        {
            _logger.LogInformation("findByLastName: <Start print result>");
            var accounts = await new AccountDaoImpl(_context).FindByLastName(value);
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("findByLastName: <End print result>");
        }

        public async Task findByUsername(string value)
        {
            _logger.LogInformation("findByUsername: <Start print result>");
            var accounts = await new AccountDaoImpl(_context).FindByUsername(value);
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("findByUsername: <End print result>");
        }

        public async Task findByEmail(string value)
        {
            _logger.LogInformation("findByEmail: <Start print result>");
            var accounts = await new AccountDaoImpl(_context).FindByEmail(value);
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("findByEmail: <End print result>");
        }

        public async Task findByContactNumber(string value)
        {
            _logger.LogInformation("findByContactNumber: <Start print result>");
            var accounts = await new AccountDaoImpl(_context).FindByContactNumber(value);
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("findByContactNumber: <End print result>");
        }

        public async Task findByPassword(string value)
        {
            _logger.LogInformation("findByPassword: <Start print result>");
            var accounts = await new AccountDaoImpl(_context).FindByPassword(value);
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("findByPassword: <End print result>");
        }

        public async Task findByUserType(string value)
        {
            _logger.LogInformation("findByUserType: <Start print result>");
            var accounts = await new AccountDaoImpl(_context).FindByUserType(value);
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    _logger.LogInformation(account.ToJsonString());
                }
            }
            _logger.LogInformation("findByUserType: <End print result>");
        }

        public async Task getByAccountUsernamePassword(string username, string password)
        {
            _logger.LogInformation("getByAccountUsernamePassword: <Start print result>");
            var account = await new AccountDaoImpl(_context).GetByAccountUsernamePassword(username, password);
            if (account != null)
            {
                _logger.LogInformation("getByAccountUsernamePassword: User login success.");
                _logger.LogInformation(account.ToJsonString());
            }
            else
            {
                _logger.LogInformation("getByAccountUsernamePassword: User login fail.");
            }
            _logger.LogInformation("getByAccountUsernamePassword: <End print result>");
        }

        public async Task getByAccountUsername(string username)
        {
            _logger.LogInformation("getByAccountUsername: <Start print result>");
            var account = await new AccountDaoImpl(_context).GetByAccountUsername(username);
            if (account != null)
            {
                _logger.LogInformation(account.ToJsonString());
            }
            _logger.LogInformation("getByAccountUsername: <End print result>");
        }

        public async Task unit_testAsync()
        {
            Account account = new Account();
            account.setFirstname("First Name");
            account.setLastname("Last Name 1111111");
            account.setUsername("admin");
            account.setEmail("admin@hotmail.com");
            account.setContactnumber("123123");
            account.setPassword("password");
            account.setUsertype("testrole");

            await getPageAccounts();
            await countAccounts();
            await saveAccount(account);
            await getAccounts();
            await getAccount(account.getAccountid());
            await findByFirstName(account.getFirstname());
            await findByLastName(account.getLastname());
            await findByUsername(account.getUsername());
            await findByEmail(account.getEmail());
            await findByContactNumber(account.getContactnumber());
            await findByPassword(account.getPassword());
            await findByUserType(account.getUsertype());
            await getByAccountUsernamePassword(account.getUsername(), account.getPassword());
            await getByAccountUsername(account.getUsername());
            await deleteAccount(account);
            

        }
    }
}
