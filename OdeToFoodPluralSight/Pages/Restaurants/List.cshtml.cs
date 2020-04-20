using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFoodPluralSight.Pages.Restaurants
{
    public class List : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly IRestaurantData restaurantData;
        
        public string Message { get; set; }
        public IEnumerable<Restaurant> RestaurantsList { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List(
            IConfiguration configuration,
            IRestaurantData restaurantData
            )
        {
            this.configuration = configuration;
            this.restaurantData = restaurantData;
        }
        
        public void OnGet()
        {
            Message = configuration["Message"] ?? "Hello, world!";
            RestaurantsList = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}