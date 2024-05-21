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

        public void OnGet()
        {

        }
        public void OnPost()
        {
            _dealer.AddCar(Cars);
        }

    }
}
