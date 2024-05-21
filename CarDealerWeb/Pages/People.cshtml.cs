using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.Xml;

namespace CarDealerWeb.Pages
{
    public class PeopleModel : PageModel
    {
        private readonly CarDealer _dealer;

        public PeopleModel(CarDealer dealer)
        {
            _dealer = dealer;
        }

        public List<Person> Person { get; set; } = new();

        public void OnGet()
        {
            Person = _dealer.GetDealers();
        }
    }
}
