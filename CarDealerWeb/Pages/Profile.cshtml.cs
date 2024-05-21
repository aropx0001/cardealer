using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        [BindProperty]
        public double? Balance { get; set; }

        [BindProperty]
        public string Firstname { get; set; }

        [BindProperty]
        public string Lastname { get; set; }

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
            {
                return Redirect("/Login");
            }
            Person = _dealer.GetPersonById(FoundID.Value);
            _dealer.AddBalance(Person.personID, Balance);
;
            return Page();
        }

        public IActionResult NogetOnPost()
        {
            
            return Page();
        }

    }
}
