using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name = null);
        Restaurant Get(int id);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> restaurants;
        
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant{Id = 1, Name = "Pizza", Location = "Maryland", Cuisine = CuisineType.Italian},
                new Restaurant{Id = 2, Name = "Cinnamon Club", Location = "London", Cuisine = CuisineType.Indian},
                new Restaurant{Id = 3, Name = "La costa", Location = "California", Cuisine = CuisineType.Mexican},
            };    
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return restaurants
                .Where(r => string.IsNullOrEmpty(name) || r.Name.StartsWith(name))
                .OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
    }
}