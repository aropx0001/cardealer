using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealerWeb.Pages
{
    public class AddCarModel : PageModel
    {
        private readonly CarDealer _dealer;

        public AddCarModel(CarDealer dealer)
        {
            _dealer = dealer;
        }

        [BindProperty]
        public Car Cars { get; set; }

        public IActionResult OnGet()
        {
            int? type = HttpContext.Session.GetInt32("Type");

            

            if (type != 1)
            {
                return Redirect("/");
            }
            return Page();
        }

        public void OnPost()
        {
            
            _dealer.AddCar(Cars);
        }

    }
}
