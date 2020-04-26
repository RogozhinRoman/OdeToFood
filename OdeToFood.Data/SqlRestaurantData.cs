using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return dbContext.Restaurants
                .Where(r => r.Name.StartsWith(name) || string.IsNullOrEmpty(name))
                .OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return dbContext.Restaurants.Find(id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = dbContext.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            dbContext.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = Get(id);

            if (restaurant != null)
            {
                dbContext.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }
    }
}