using System.Text.Json;
using TravelTours.Core.Entities;
using Microsoft.Extensions.Logging;

namespace TravelTours.Infraestructure.Data
{
    public class BaseDataSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Place.Any())
                {
                    var placeData = File.ReadAllText("../Infraestructure/Data/SeedData/Places.json");
                    var places = JsonSerializer.Deserialize<List<Place>>(placeData);

                    foreach(var item in places)
                    {
                        await context.Place.AddRangeAsync(item);
                        
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BaseDataSeed>();
                logger.LogError(ex.Message);

            }
        }
    }
}
