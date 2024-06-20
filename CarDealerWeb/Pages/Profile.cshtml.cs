using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace CarDealerWeb.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly CarDealer _dealer;

        public Person Person { get; set; }

        public ProfileModel(CarDealer dealer)
        {
            _dealer = dealer;
            
        }
        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }
        [BindProperty]
        public double? Balance { get; set; }

        [BindProperty]
        public string Firstname { get; set; }

        [BindProperty]
        public string Lastname { get; set; }

        [BindProperty]
        public Person car { get; set; }

        public IActionResult OnGet()
        {
            int? FoundID = HttpContext.Session.GetInt32("ID");
            
            if (FoundID == null)
            {
                return Redirect("/Login");
            }
            Person = _dealer.GetPersonById(FoundID.Value);

            return Page();
        }

        

        public IActionResult OnPost()
        {
            int? FoundID = HttpContext.Session.GetInt32("ID");

            if (FoundID == null)
                return Redirect("/Login");

            Person = _dealer.GetPersonById(FoundID.Value);

            if (Balance != null)
                _dealer.AddBalance(Person.personID, Balance);

            return Page();
        }

        public IActionResult OnPostNoget()
        {
            //Vi henter personen igen fordi ellers sletter den dataen fra person. Derefter vil den sige person er null, når vi returner page.
            int? FoundID = HttpContext.Session.GetInt32("ID");

            if (FoundID == null)
                return Redirect("/Login");

            Person = _dealer.GetPersonById(FoundID.Value);

            Person.firstname = Firstname;
            Person.lastname = Lastname;

            return Page();
        }

    }
}
