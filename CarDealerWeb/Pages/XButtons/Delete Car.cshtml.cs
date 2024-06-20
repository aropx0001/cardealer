using Cardealer;
using Cardealer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [BindProperty]
        public List<int> SelectedCarIDs { get; set; } = new();

        public void OnGet()
        {
            Cars = _dealer.Cars;
        }

        public IActionResult OnPost()
        {
            

            if (SelectedCarIDs != null && SelectedCarIDs.Count > 0)
            {
                foreach (int carID in SelectedCarIDs)
                {
                    _dealer.DeleteCarByID(carID);
                }
            }
            return RedirectToPage();
        }
    }
}
