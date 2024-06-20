using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealerWeb.Pages.XButtons
{
    public class Sign_upModel : PageModel
    {

        // Service reference holder.
        private readonly CarDealer _dealer;

        // Inject CarDealer into CarsModel constructor.
        public Sign_upModel(CarDealer dealer)
        {
            // Set the injected class to _dealer, for a reference to the service.
            _dealer = dealer;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Quote { get; set; }

        [BindProperty]
        public string Firstname { get; set; }

        [BindProperty]
        public string Lastname { get; set; }

        [BindProperty]
        public Person Person { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            int listMax = _dealer.People.Count();

            _dealer.CreatePerson(Firstname, Lastname, PersonType.Customer, Username, Password,Quote).ToString();

            if (listMax < _dealer.People.Count())
            {
                Response.Redirect("/Login");
            }
        }

    }
}
