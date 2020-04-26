using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFoodPluralSight.Pages.Restaurants
{
    public class Edit : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public Edit(IRestaurantData restaurantData,
                    IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.Get(restaurantId.Value);
                
            }
            else
            {
                Restaurant = new Restaurant();
            }
            
            if (Restaurant == null)
            {
                return Redirect("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
                TempData["Message"] = "Restaurant updated!";
            }
            else
            {
                restaurantData.Add(Restaurant);
                TempData["Message"] = "Restaurant saved!";
            }
            
            restaurantData.Commit();

            return RedirectToPage("./Detail", new {restaurantId = Restaurant.Id});
        }
    }
}