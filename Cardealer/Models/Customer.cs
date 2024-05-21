using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cardealer.Models
{
    internal class Customer : Person
    {
        string customer;
        string Dealer;

        public Customer(int personID, string firstname, string lastname, string username, string password) : base(personID,firstname,lastname,username,password)
        {
            this.Type = PersonType.Customer;
        }
    }

    internal class Dealer : Person
    {
        public Dealer(int personID, string firstname, string lastname, string username, string password) : base(personID, firstname, lastname, username, password)
        {
            this.Type = PersonType.Dealer;
        }
    }
}
