using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFoodPluralSight.Pages.Restaurants
{
    public class Detail : PageModel
    {
        private readonly IRestaurantData restaurantData;
        public Restaurant Restaurant { get; set; }

        public Detail(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.Get(restaurantId);

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            
            return Page();
        }
    }
}