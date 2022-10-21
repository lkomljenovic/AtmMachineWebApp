using AtmMachine.DAL;
using AtmMachine.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtmMachine.Web.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountApiController : Controller
    {
        private AtmMachineDbContext _dbContext;
        public AccountApiController(AtmMachineDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult Get()
        {
            var accounts = this._dbContext.Accounts
                .Select(a => new AccountDTO()
                {
                    AccountNumber = a.AccountNumber
                })
                .ToList();

            return Ok(accounts);
        }

        [Route("{AccountNumber}")]
        public IActionResult Get(string accountNumber)
        {
            var accounts = this._dbContext.Accounts
                .Where(a => a.AccountNumber == accountNumber)
                .Select(a => new AccountDTO()
                {
                    AccountNumber = a.AccountNumber,
                    Pin = a.Pin
                })
                .FirstOrDefault();

            if(accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        public class AccountDTO
        {
            public string AccountNumber { get; set; }
            public string Pin { get; set; }
        }

    }
}