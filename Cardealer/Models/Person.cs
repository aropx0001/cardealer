using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cardealer.Models
{
    public class Person
    {
        public int personID;
        public string firstname;
        public string lastname;
        public PersonType Type;
        public double? balance = 0;
        public List<Car> BoughtCars = new();
        public string username;
        public string password;
        public string quote;

        //Constructor
        public Person(int personID, string firstname, string lastname, string username, string password, string quote)
        {
            this.personID = personID;
            this.firstname = firstname;
            this.lastname = lastname;
            this.balance = 0;
            this.username = username;
            this.password = password;
            this.quote = quote;
        }

        public Person()
        {

        }
    }
}
