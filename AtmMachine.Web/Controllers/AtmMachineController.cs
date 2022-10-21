using AtmMachine.DAL;
using AtmMachine.Model;
using AtmMachine.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtmMachine.Web.Controllers
{
    public class AtmMachineController : Controller
    {
        private AtmMachineDbContext _dbContext;

        public AtmMachineController(AtmMachineDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index(string accountNumber)
        {
            var user = _dbContext.Users
                .Where(u => u.AccountNumber == accountNumber)
                .FirstOrDefault();

            return View(model: user);
        }
    }
}