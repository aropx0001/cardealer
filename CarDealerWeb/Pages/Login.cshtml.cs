using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealerWeb.Pages
{
    public class Login : PageModel
    {
        // Service reference holder.
        private readonly CarDealer _dealer;

        // Inject CarDealer into CarsModel constructor.
        public Login(CarDealer dealer)
        {
            // Set the injected class to _dealer, for a reference to the service.
            _dealer = dealer;
        }

        [BindProperty]
        public string Username{ get; set;}

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            Person foundPerson = _dealer.HtmlLogin(Username, Password);

            if (foundPerson != null)
            {
                HttpContext.Session.SetInt32("ID", foundPerson.personID);
                HttpContext.Session.SetInt32("Type", (int)foundPerson.Type);

                return RedirectToPage("/Profile");
            }
            return Page();
        }

        public IActionResult OnPostSignUp()
        {
            return Page();
        }
    }
}
