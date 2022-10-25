using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Model
{
    public class TransferModel
    {
        public AccountDetails Sender { get; set; }
        public string Recipient { get; set; }
        public decimal Ammount { get; set; }
    }
}
