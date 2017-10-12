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

    public class Customer
    {
        public string CustomerNumber { get; set; }
        public string OrganisationNumber { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        List<Account> accounts = new List<Account>();

        public Customer()
        {



        }






        public Customer(string customernumber, string organisationnumber, string name, string adress,
            string city, string region, string postcode, string country, string phonenumber)
        {

            CustomerNumber = customernumber;
            OrganisationNumber = organisationnumber;
            Name = name;
            Adress = adress;
            City = city;
            Region = region;
            PostCode = postcode;
            Country = country;
            PhoneNumber = phonenumber;

        }



    }
}







    
















