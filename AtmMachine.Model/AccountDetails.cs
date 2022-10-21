using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Model
{
    public class AccountDetails
    {
        [Key]
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}
