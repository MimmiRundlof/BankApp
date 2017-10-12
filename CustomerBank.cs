using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Reflection;


namespace BankApp
{
    public class CustomerBank
    {
       
            //Skapar lista för kunder och konton
            List<Customer> customers = new List<Customer>();
            List<Account> accounts = new List<Account>();


        //Metod som lägger till värde i kontolista (används i unit test)
        public List<Account> MakeAccountList(Account konton)
        {

            accounts.Add(konton);
            return accounts;
        }
        
        public void ReadFromTextFile(StreamReader textReader)
        {
            //Läser in textfil, tilldelar antalKunder antalet
            string textInformation = textReader.ReadLine();
            int antalKunder = int.Parse(textInformation);

            while (antalKunder > 0)
            {
                //Läser in rad för rad i textfil
                textInformation = textReader.ReadLine();
                string[] info = textInformation.Split(new[] { ';', '\r' });

                Customer kund = new Customer(info[0], info[1], info[2], info[3], info[4],
                    info[5], info[6], info[7], info[8]);
                
                //Lägger till kund i listan
                customers.Add(kund);

                antalKunder--;


            }
            //Läser in antal konton
            textInformation = textReader.ReadLine();
            int antalKonton = int.Parse(textInformation);


            while (antalKonton > 0)

            {
                textInformation = textReader.ReadLine();
                string[] info = textInformation.Split(new[] { ';', '\r' });

                Account konton = new Account(info[0], info[1], info[2]);

                //Lägger till konto i lista
                MakeAccountList(konton);
              
                antalKonton--;

            }



        }
        public void ShowCustomer(string search)
        {
            
            //Söker igenom listan accounts om accountnumber eller customernumber equals "search"
            var searchAccountList = accounts.Where(x => x.AccountNumber.Equals(search)
            || x.CustomerNumber.Equals(search));
            

            string cusNum = "";

            //Tilldelar cusNum värdet av customernumber i account listan
            // cusNum == kundnumret i accounts
            foreach (var item in searchAccountList)
            {
                cusNum = item.CustomerNumber;
            }

            //Söker igenom listan customer om customernumber equals "search" eller samma värde som cusNum
            var seachCustomerList = customers.Where(x => x.CustomerNumber.Equals(search) ||
            x.CustomerNumber.Equals(cusNum));
            
            //Skriver ut kundens information
            foreach (var item in seachCustomerList)
            {
                Console.WriteLine("Kundnummer: " + item.CustomerNumber);
                Console.WriteLine("Namn: " + item.Name);
                Console.WriteLine("Adress: " + item.Adress);
                Console.WriteLine("Stad: " + item.City);
                Console.WriteLine("Postnummer: " + item.PostCode);
                Console.WriteLine("Region: " + item.Region);
                Console.WriteLine("Land: " + item.Country);
                Console.WriteLine("Telefonnummer : " + item.PhoneNumber + "\n");

            }
            //Skriver ut kundens konton
            foreach (var item in searchAccountList)
            {

                Console.WriteLine("Kontonummer: " + item.AccountNumber);
                Console.WriteLine("Saldo: " + item.Saldo);

            }

            //Räknar ut kontons totala saldo
            decimal summa = 0;

            foreach (var item in searchAccountList)
            {

                summa += item.Saldo;
            }

            Console.WriteLine("Totalsumma av konton: " + summa);





        }
        public void SearchCustomer(string search)
        {
            //Söker i listan customers efter namn eller postnummer som har samma värde som "search"
            var searcher = customers.Where(x => x.Name.Contains(search) || x.PostCode.Contains(search));
            
            //Skriver ut resultatet
            Console.WriteLine("Kundnummer: Namn:");
            foreach (var item in searcher)
            {
                Console.WriteLine(item.CustomerNumber + " " + item.Name);
            }


            
        }
        public void CreateCustomer()
        {
            //Skapar nya objekt av kunden
            Customer newCustomer = new Customer();
            Account newAccount = new Account();

            //Tar fram nytt kundnummer i följd av senaste
            var latestCustomerNumber = customers.Max(c => c.CustomerNumber);
            int lastCustomerInt = int.Parse(latestCustomerNumber);
            lastCustomerInt++;
            string customerNumber = lastCustomerInt.ToString();

            //Metod som skapar nytt konto med samma kundnummer
            NewAccount(customerNumber);

            //Skapar kundens info
            newCustomer.CustomerNumber = customerNumber;
            Console.WriteLine("Kundnummer : " + newCustomer.CustomerNumber);
            Console.Write("Organisationsnummer: ");
            newCustomer.OrganisationNumber = TryTextError(Console.ReadLine());
            Console.Write("Namn: ");
            newCustomer.Name= TryTextError(Console.ReadLine());
            Console.Write("Adress: ");
            newCustomer.Adress = TryTextError(Console.ReadLine());
            Console.Write("Stad: ");
            newCustomer.City = TryTextError(Console.ReadLine());
            Console.Write("Region: ");
            newCustomer.Region = Console.ReadLine();
            Console.Write("Postnummer: ");
            newCustomer.PostCode = TryTextError(Console.ReadLine());
            Console.Write("Land: ");
            newCustomer.Country = Console.ReadLine();
            Console.Write("Telefonnummer: ");
            newCustomer.PhoneNumber = Console.ReadLine();

            //Lägger till kund i lista
            customers.Add(newCustomer);


        }
        public void DeleteCustomer(string customerNumber)
        {
            
            Console.WriteLine("Är du säker på att du vill radera kund {0}?\nJ/N?", customerNumber);

            //Om kunden har ett konto med pengar på så går det inte att radera kund
            if (Console.ReadLine().ToLower() == "j")
            {
                //Kontrollerar om kunden har ett eller flera konton med saldo > 0
                var checkAccounts = accounts
                                   .Where(x => x.CustomerNumber.Equals(customerNumber))
                                   .Any(a => a.Saldo > 0);

                //Inga konton med saldo > 0
                if (!checkAccounts)
                {
                    //Letar upp vart kundens index i listan customers är och tar bort
                    var deleteCustomer = customers.FindIndex(x => x.CustomerNumber.Equals(customerNumber));
                    customers.RemoveAt(deleteCustomer);
                    //Letar upp vart kundens konto/konton index är i listan accounts och tar bort
                    var deleteAccounts = accounts.FindIndex(x => x.CustomerNumber.Equals(customerNumber));
                    var countAccounts = accounts.Count(x => x.CustomerNumber.Equals(customerNumber));
                    accounts.RemoveRange(deleteAccounts, countAccounts);


                    Console.WriteLine("Kund {0} är raderad.", customerNumber);
                }

                //Kund har konto med saldo > 0
                else if (checkAccounts)
                {
                    Console.WriteLine("Kunden har pengar kvar på sitt konto.");

                }
            }



        }
     public void DeleteAccount(string accountNumber)
        {
            //Kontrollerar att man skriver in något
            TryTextError(accountNumber);

            //Kollar om konton har pengar kvar
            var checkAccounts = accounts
               .Where(x => x.AccountNumber.Equals(accountNumber))
               .Any(a => a.Saldo > 0);

            //Om konton inte har pengar
            if (!checkAccounts)
            {
                var deleteAccounts = accounts.FindIndex(x => x.AccountNumber.Equals(accountNumber));
                accounts.RemoveAt(deleteAccounts);
                Console.WriteLine("Kontot {0} raderat.",accountNumber);
                
            }  
            //Om kontot har pengar kvar
            else if (checkAccounts)
            {
                Console.WriteLine("Gick inte att radera, saldo är större än 0 på kontot.");

            }

        }
        public void Transaction(string fromAccount, string toAccount)
        {
            //Kontrollerar att man skrivit in nåt
            TryTextError(fromAccount);
            TryTextError(toAccount);
            Console.WriteLine("Ange belopp: ");
            //Belopp som ska föras över
            decimal amount = decimal.Parse(Console.ReadLine());

            // Tar fram kontona ur listan
            var accountFrom = accounts.Where(x => x.AccountNumber.Equals(fromAccount));
            var accountTo = accounts.Where(x => x.AccountNumber.Equals(toAccount));

            // För över amount från accountFrom till account To
            // om saldot är >= amount
            foreach (var item in accountFrom)
            {
                if (item.Saldo >= amount)
                {
                    item.Saldo = item.Saldo - amount;

                    foreach (var acc in accountTo)
                    {
                        acc.Saldo = acc.Saldo + amount;
                    }
                }
                //Om kontot är < amount
                else
                {
                    Console.WriteLine("Du har för lite pengar på kontot.");
                }
            }



        }
        public void NewAccount(string customerNumber)
        {

            //Skapar nytt konto
            Account newAccount = new Account();

            // Tar fram enaste kontonumret i listan och ökar med 1
            var latestAccountNumber = accounts.Max(a => a.AccountNumber);
            int lastAccountInt = int.Parse(latestAccountNumber);
            lastAccountInt++;
            string accountNumber = lastAccountInt.ToString();
            //Sätter kontots saldo till 0
            decimal saldo = 0.0m;

            //Tilldelar kundnummer och kontonummer med saldo 0
            newAccount.CustomerNumber = customerNumber;
            newAccount.AccountNumber = accountNumber;
            newAccount.Saldo = saldo;

            //Lägger till kontot i listan
            accounts.Add(newAccount);
            Console.WriteLine("Nytt konto skapats:\nKontonummer: {0}\nSaldo: {1}", accountNumber, saldo);
        }
        public void SaveToFile()
        {

            //Skapar ny streamWriter som namnges av DateTime.Now
            StreamWriter streamWriter = new StreamWriter(String.Format(DateTime.Now.ToString("yyyyMMdd-HHmm")) + ".txt");
            
            //Skriver in antalet kunder
            streamWriter.WriteLine(customers.Count());
            
            //För varje kund i customers, lägg till i streamWriter
            foreach (var item in customers)
            {

                streamWriter.WriteLine($"{item.CustomerNumber};{item.OrganisationNumber};{item.Name};{item.Adress};" +
                    $"{item.City};{item.Region};{item.PostCode};{item.Country};{item.PhoneNumber}");

            }

            //Skriver in antalet konton
            streamWriter.WriteLine(accounts.Count());
            //För varje konto i konton, lägg till i streamWriter
            foreach (var item in accounts)
            {
                string saldo = item.Saldo.ToString();
                 saldo = saldo.Replace(',', '.');
                streamWriter.WriteLine($"{item.AccountNumber};{item.CustomerNumber};{saldo}");

            }
            //Spara streamWriiter
            streamWriter.Close();
        }
        public void Deposit(string accountNumber)
        {
            //Kontrollera att nåt är skrivet
            TryTextError(accountNumber);

            bool parsing = false;
            decimal amount = 0;

            Console.WriteLine("Ange belopp: ");
            //Så länge beloppet inte går att parsea till decimal
            while (!parsing)
            {
                parsing = decimal.TryParse(Console.ReadLine(), out amount);

                if (!parsing) { Console.WriteLine("Fel inmatat, försök igen."); }
            }

                //Tar fram kontonumret
                var changeSaldo = accounts.Where(x => x.AccountNumber.Equals(accountNumber));
            
                //Lägger till amount på kontots saldo
                foreach (var item in changeSaldo)
                {
                    item.Saldo = item.Saldo + amount;

                }


        }
        public void WithDraw(string accountNumber, decimal amount)
        {
            //Kollar att nåt är inskrivet
            TryTextError(accountNumber);

            //Tar fram kontonummer i listan
            var changeSaldo = accounts.Where(x => x.AccountNumber.Equals(accountNumber));
     
            //Om kontot är >= amount, dra av amount
            foreach(var item in changeSaldo)
            {
                if (item.Saldo >= amount)
                {
                    item.Saldo = item.Saldo - amount;

                }
                //Om kontot < amount
                else
                {
                    Console.WriteLine("Du har för lite pengar på kontot.");
                }

            }





        }
        public string TryTextError(string tryReadLine)
        {
            //Metod som kontrollerar inmatning
            while(tryReadLine == "")

            {

                Console.WriteLine("Ange korrekt inmatning.");
                tryReadLine = Console.ReadLine();

            }
            return tryReadLine;

        }



    }

    
}

