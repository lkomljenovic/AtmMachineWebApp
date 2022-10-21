using AtmMachine.DAL;
using AtmMachine.Model;
using AtmMachine.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtmMachine.Web.Controllers
{
    public class HomeController : Controller
    {
        private AtmMachineDbContext _dbContext;
        private string PinEntry { get; set; }

        public HomeController(AtmMachineDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Account entry)
        {
            var validatedAccount = _dbContext.Accounts
                .Where(a => a.AccountNumber == entry.AccountNumber)
                .FirstOrDefault();

            if (validatedAccount != null)
            {
                ViewBag.ValidationMessage = $"Successfull validation of entered account number ({entry.AccountNumber})!";
                ViewBag.ValidationCode = 200;
            }
            else
            {
                ViewBag.ValidationMessage = $"Entered account number ({entry.AccountNumber}) does not exist in the database!";
                ViewBag.ValidationCode = 404;
            }
            return View(model: validatedAccount);
        }

        [HttpPost]
        public ActionResult CheckPin(PinEntryModel entry)
        {
            var validatedAccount = _dbContext.Accounts
                .Where(a => a.AccountNumber == entry.Account.AccountNumber && a.Pin == entry.PinEntry)
                .FirstOrDefault();

            if(validatedAccount != null)
            {
                var user = _dbContext.Users
                    .Where(u => u.AccountNumber == entry.Account.AccountNumber)
                    .FirstOrDefault();

                //TODO provjeriti ima li bolji nacin za otvarati view-ove
                return View("Views\\AtmMachine\\Index.cshtml", model: user);
            }
            else
            {
                var account = _dbContext.Accounts
                    .Where(a => a.AccountNumber == entry.Account.AccountNumber)
                    .FirstOrDefault();

                return View("Index", model: account);
            }
        }
    }
}