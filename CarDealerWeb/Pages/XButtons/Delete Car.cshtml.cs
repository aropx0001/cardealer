using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealerWeb.Pages.Shared.Buttons
{
    

    public class Delete_CarModel : PageModel
    {
        private readonly CarDealer _dealer;

        public Delete_CarModel(CarDealer dealer)
        {
            _dealer = dealer;
        }

        [BindProperty]
        public List<Car> Cars { get; set; } = new();
        public void OnGet()
        {
            Cars = _dealer.Cars;
        }
        public void OnPost()
        {
            
        }
    }
}
