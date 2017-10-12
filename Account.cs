using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Account
    {
        public Account() { }


        public Account(string accountnumber, string customernumber, string saldo)
        {
            // parsea accountnumber och customernumber till int
            AccountNumber = accountnumber;
            CustomerNumber = customernumber;
            
            saldo = saldo.Replace('.', ',');
            decimal i = decimal.Parse(saldo);
            Saldo = i;
        }

        public string CustomerNumber { get; set; }
        public string AccountNumber  { get; set; }        
        public decimal Saldo { get; set; }

        List<Account> accounts = new List<Account>();
        public List<Account> MakeAccountList(Account konton)
        {

            accounts.Add(konton);
            return accounts;
        }


    }
}
