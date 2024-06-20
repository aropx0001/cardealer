using Cardealer.Models;
using Cardealer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarDealerWeb.Pages
{
    public class Update_carModel : PageModel
    {
        private readonly CarDealer _dealer;

        public Update_carModel(CarDealer dealer)
        {
            _dealer = dealer;
        }

        [BindProperty]
        public List<Car> Cars { get; set; } = new();

        [BindProperty]
        public List<int> SelectedCarIDs { get; set; } = new();
        
        [BindProperty]
        public int CarID { get; set; }

        [BindProperty]
        public CarBrand Brand { get; set; }

        [BindProperty]
        public double Price { get; set; }

        public void OnGet()
        {
            Cars = _dealer.Cars;
        }
        public IActionResult OnPost()
        {

            return Page();
        }
    }
}




