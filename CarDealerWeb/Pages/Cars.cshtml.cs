using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cardealer;
using Cardealer.Models;

namespace CarDealerWeb.Pages
{
    public class CarsModel : PageModel
    {
        #region Dependency injection
        // Service reference holder.
        private readonly CarDealer _dealer;

        // Inject CarDealer into CarsModel constructor.
        public CarsModel(CarDealer dealer)
        {
            // Set the injected class to _dealer, for a reference to the service.
            _dealer = dealer;
        }
        #endregion

        [BindProperty]
        public List<Car> Cars { get; set; } = new();
        public void OnGet()
        {
            Cars = _dealer.Cars;
        }
    }
}
