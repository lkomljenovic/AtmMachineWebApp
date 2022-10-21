using AtmMachine.DAL;
using AtmMachine.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtmMachine.Web.Controllers
{
    [Route("api/accountDetails")]
    [ApiController]
    public class AccountDetailsApiController : Controller
    {
        private AtmMachineDbContext _dbContext;
        public AccountDetailsApiController(AtmMachineDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult Get()
        {
            var accounts = this._dbContext.AccountDetails
                .Select(a => new AccountDetalisDTO()
                {
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance
                })
                .ToList();

            return Ok(accounts);
        }

        [Route("{AccountNumber}")]
        public IActionResult Get(string accountNumber)
        {
            var accounts = this._dbContext.AccountDetails
                .Where(a => a.AccountNumber == accountNumber)
                .Select(a => new AccountDetalisDTO()
                {
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance
                })
                .FirstOrDefault();

            if(accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        public class AccountDetalisDTO
        {
            public string AccountNumber { get; set; }
            public decimal Balance { get; set; }
        }

    }
}