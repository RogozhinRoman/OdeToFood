using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFoodPluralSight.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly OdeToFoodDbContext context;

        public RestaurantsController(OdeToFoodDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants()
        {
            return context.Restaurants;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant([FromRoute] int restaurantId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant([FromRoute] int id, [FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            context.Restaurants.Remove(restaurant);
            await context.SaveChangesAsync();

            return Ok(restaurant);
        }

        private bool RestaurantExists(int id)
        {
            return context.Restaurants.Any(e => e.Id == id);
        }
    }
}