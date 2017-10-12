using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Läser in textfil
            StreamReader textReader = new StreamReader("bankdata.txt");
            Console.WriteLine("Textfilen inläst.");
            
            CustomerBank customerInfo = new CustomerBank();
            
            customerInfo.ReadFromTextFile(textReader);
            while (true)
            {

                Console.WriteLine("HUVUDMENY\n0) Avsluta och spara\n1) Sök kund\n2) Visa kundbild\n3) Skapa kund\n" +
                "4) Ta bort kund\n5) Skapa konto\n6) Ta bort konto\n7) Insättning\n8) Uttag\n9) Överföring");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        {
                            //Sparar textfil

                            customerInfo.SaveToFile();
                                                        
                            Console.WriteLine("Textfil sparad.");
                            Enter();
                            break;
                        }
                    case "1":
                        {
                            Console.WriteLine("Sök kund\nSök på namn eller postnummer: ");
                            choice = Console.ReadLine();
                            customerInfo.SearchCustomer(choice);
                            Enter();
                            break;
                        }
                    case "2":
                        {
                            Console.Write("Visa kundbild\nSök på kundnummer eller kontonummer: ");
                            choice = Console.ReadLine();
                            customerInfo.ShowCustomer(choice);
                            Enter();
                            break;

                        }
                    case "3":
                        {
                            Console.WriteLine("Skapa ny kund: ");
                            customerInfo.CreateCustomer();
                            Enter();
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Ta bort kund\nSkriv in kundnummer: ");
                            choice = Console.ReadLine();
                            customerInfo.DeleteCustomer(choice);
                            Enter();
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Skapa konto\nAnge kundnummer: ");
                            choice = Console.ReadLine();
                            customerInfo.NewAccount(choice);
                            Enter();
                            break;
                        }
                    case "6":
                        {

                            Console.WriteLine("Ta bort konto:\nAnge kontonummer: ");
                            choice = Console.ReadLine();
                            customerInfo.DeleteAccount(choice);
                            Enter();
                            break;
                        }
                    case "7":
                        {

                            Console.WriteLine("Insättning\nAnge kontonummer: ");
                            choice = Console.ReadLine();
                            customerInfo.Deposit(choice);
                            Enter();
                            break;
                        }
                    case "8":
                        {

                            Console.WriteLine("Uttag\nAnge kontonummer: ");
                            choice = Console.ReadLine();
                            Console.WriteLine("Ange belopp: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            customerInfo.WithDraw(choice, amount);
                            Enter();
                            break;
                        }
                    case "9":
                        {
                            Console.WriteLine("Överföring från kontonummer: ");
                            string fromAccount = Console.ReadLine();
                            Console.WriteLine("Till kontonummer: ");
                            string toAccount = Console.ReadLine();
                            customerInfo.Transaction(fromAccount, toAccount);
                            Enter();

                            break;
                        }
                    default:
                        {
                            Enter();
                            break;
                        }

                }


            }








        }
        static void Enter()
        {
            Console.WriteLine("\nTryck [ENTER] för att gå tillbaka.");
            Console.ReadLine();
            Console.Clear();

        }
    }
}




