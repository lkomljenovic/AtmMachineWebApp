using AtmMachine.DAL;
using AtmMachine.Model;
using AtmMachine.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtmMachine.Web.Controllers
{
    [Route("AtmMachine")]
    public class AtmMachineController : Controller
    {
        private AtmMachineDbContext _dbContext;

        public AtmMachineController(AtmMachineDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Route("Actions")]
        public IActionResult Index(Account account)
        {
            var user = _dbContext.Users
                .Where(u => u.AccountNumber == account.AccountNumber)
                .FirstOrDefault();

            return View(model: user);
        }
        [Route("Balance")]
        public IActionResult Balance(string account)
        {
            var accountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == account)
                .FirstOrDefault();

            return View(model: accountDetails);
        }

        [Route("Transfer")]
        public IActionResult Transfer(string account)
        {
            var accountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == account)
                .FirstOrDefault();

            TransferModel transfer = new TransferModel();
            transfer.Sender = accountDetails;

            return View(model: transfer);
        }

        [HttpPost("TransferPost")]
        public async Task<IActionResult> TransferPost(TransferModel transfer)
        {
            var okToSendMoney = false;

            var senderAccountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == transfer.Sender.AccountNumber)
                .FirstOrDefault();

            var recipientAccountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == transfer.Recipient)
                .FirstOrDefault();

            if (senderAccountDetails.Balance >= transfer.Ammount && recipientAccountDetails != null)
            {
                okToSendMoney = true;
                ViewBag.TransferMessage = $"Ammount ({transfer.Ammount}) sucessfully transfered to recipient account ({transfer.Recipient})!";
            }
            else if (senderAccountDetails.Balance < transfer.Ammount && recipientAccountDetails != null)
            {
                okToSendMoney = false;
                ViewBag.TransferMessage = $"Ammount ({transfer.Ammount}) can't be transfered to recipient account ({transfer.Recipient}) because Sender account balance is too low!";
            }
            else
            {
                okToSendMoney = false;
                ViewBag.TransferMessage = $"Recipient account number does not exist in the database!";
            }

            ViewBag.TransferStatus = okToSendMoney;

            if (okToSendMoney)
            {
                var user = _dbContext.Users
                    .Where(u => u.AccountNumber == transfer.Sender.AccountNumber)
                    .FirstOrDefault();

                var senderAccount = _dbContext.AccountDetails
                    .Where(a => a.AccountNumber == transfer.Sender.AccountNumber)
                    .FirstOrDefault();

                var recipientAccount = _dbContext.AccountDetails
                    .Where(a => a.AccountNumber == transfer.Recipient)
                    .FirstOrDefault();

                recipientAccount.Balance += transfer.Ammount;
                senderAccount.Balance -= transfer.Ammount;

                var senderOk = await this.TryUpdateModelAsync(senderAccount);
                var recipientOk = await this.TryUpdateModelAsync(recipientAccount);

                if (senderOk && recipientOk)
                {
                    _dbContext.SaveChanges();
                }
                return View("Index", model: user);
            }
            else
            {
                return View("Transfer", model: transfer);
            }
        }

        [Route("Deposit")]
        public IActionResult Deposit(string account)
        {
            var accountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == account)
                .FirstOrDefault();

            DepositModel deposit = new DepositModel();
            deposit.AccountNumber = account;

            return View(model: deposit);
        }

        [HttpPost("DepositPost")]
        public async Task<IActionResult> DepositPost(DepositModel deposit)
        {
            var accountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == deposit.AccountNumber)
                .FirstOrDefault();
            ViewBag.DepositStatus = true;

            accountDetails.Balance += deposit.Ammount;

            var ok = await this.TryUpdateModelAsync(accountDetails);

            if (ok)
            {
                _dbContext.SaveChanges();
                ViewBag.DepositStatus = true;
            }

            var user = _dbContext.Users
                .Where(u => u.AccountNumber == deposit.AccountNumber)
                .FirstOrDefault();

            ViewBag.DepositMessage = $"Sucessfully deposited {deposit.Ammount} HRK!";

            return View("Index", model: user);
        }

        [Route("Withdrawal")]
        public IActionResult Withdrawal(string account)
        {
            var accountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == account)
                .FirstOrDefault();

            DepositModel deposit = new DepositModel();
            deposit.AccountNumber = account;

            return View(model: deposit);
        }

        [HttpPost("WithdrawalPost")]
        public async Task<IActionResult> WithdrawalPost(DepositModel deposit)
        {
            var accountDetails = _dbContext.AccountDetails
                .Where(a => a.AccountNumber == deposit.AccountNumber)
                .FirstOrDefault();

            var okToWithdraw = false;

            if (accountDetails.Balance >= deposit.Ammount)
            {
                okToWithdraw = true;
                accountDetails.Balance -= deposit.Ammount;

                var ok = await this.TryUpdateModelAsync(accountDetails);

                if (ok)
                {
                    _dbContext.SaveChanges();
                }

                var user = _dbContext.Users
                    .Where(u => u.AccountNumber == deposit.AccountNumber)
                    .FirstOrDefault();

                ViewBag.WithdrawalStatus = true;
                ViewBag.WithdrawalMessage = $"Successfull withdrawal of {deposit.Ammount} HRK!";

                return View("Index", model: user);
            }
            else
            {
                ViewBag.WithdrawalStatus = false;
                ViewBag.WithdrawalMessage = $"The ammout ({deposit.Ammount}) you want to withdraw is greater than account balance!";

                return View("Withdrawal", model: deposit);
            }
        }
    }
}