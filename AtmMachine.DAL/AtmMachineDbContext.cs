using AtmMachine.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.DAL
{
    public class AtmMachineDbContext : DbContext
    {
        public AtmMachineDbContext(DbContextOptions<AtmMachineDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountDetails> AccountDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasData(
                new Account() { AccountNumber = "1122334455", Pin = "1234" },
                new Account() { AccountNumber = "1234567890", Pin = "5432" }
                );

            modelBuilder.Entity<AccountDetails>().HasData(
                new AccountDetails() { AccountNumber = "1122334455", Balance = (decimal) 1234.56},
                new AccountDetails() { AccountNumber = "1234567890", Balance = (decimal) 5999.56 }
                );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "Luka", Surname = "Komljenovic", Username = "lkomljenovic", AccountNumber = "1122334455" },
                new User() { Id = 2, Name = "New", Surname = "User", Username = "nUser", AccountNumber = "1234567890" }
                );
        }
    }
}
