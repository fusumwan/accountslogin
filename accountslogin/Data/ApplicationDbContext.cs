using accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace accountslogin.Data
{
    /// <summary>
    /// I want to use my existing account table for authentication without relying on ASP.NET Core Identity's default behavior.
    /// So I don't need to inhert IdentityDbContext<Account>
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");
                // Configure the rest of your model as previously shown
            });
        }
    }
}
