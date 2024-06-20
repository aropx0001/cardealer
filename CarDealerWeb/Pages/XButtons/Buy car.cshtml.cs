using Cardealer.Models;
using Cardealer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealerWeb.Pages
{
    public class Buy_carModel : PageModel
    {
        private readonly CarDealer _dealer;

        // Not a constructor
        public Buy_carModel(CarDealer dealer)
        {
            _dealer = dealer;
        }

        [BindProperty]
        public int CarID { get; set; }

        [BindProperty]
        public Car Car { get; set; }

        [BindProperty]
        public Person Person { get; set; }

        [BindProperty]
        public List<Car> Cars { get; set; }


        public IActionResult OnGet()
        {
            Cars = _dealer.Cars;

            return Page();
        }
        public IActionResult OnPost()
        {
            
            int? FoundID = HttpContext.Session.GetInt32("ID");

            if (FoundID == null)
                return Redirect("/Login");

            Person = _dealer.GetPersonById(FoundID.Value);

            _dealer.BuyCar(CarID,Person);

            return OnGet();
        }
    }
}
