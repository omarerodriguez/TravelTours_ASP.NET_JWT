using Microsoft.EntityFrameworkCore;
using TravelTours.Core.Entities;
using TravelTours.Core.Interfaces;
using TravelTours.Infraestructure.Data;

namespace TravelTours.Infraestructure.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly AppDbContext db;

        public PlaceRepository(AppDbContext db)
        {
            this.db = db;
        }
        //READ ALL PLACES
        public async Task<IReadOnlyList<Place>> GetPlaces()
        {
            //var paisId = 1;
            //var lugares = db.Lugar.Where(p => p.PaisId == paisId);
            return await db.Place
                .Include(p => p.Country)
                .Include(c => c.Category)
                .ToListAsync();
        }
        //READ PLACE BY ID
        public async Task<Place> GetPlaceById(int id)
        {
            return await db.Place
                .Include(p => p.Country)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
        //CREATE PLACE
        public async Task<Place> AddPlace(Place place)
        {
            if (place.Country != null)
            {
                db.Entry(place.Country)
                    .State = EntityState.Unchanged;
            }
            if (place.Category != null)
            {
                db.Entry(place.Category)
                    .State = EntityState.Unchanged;
            }
            var result = await db.Place
                .AddAsync(place);
            await db.SaveChangesAsync();
            return result.Entity;
        }
        //UPDATE PLACE
        public async Task<Place> UpdatePlace(Place place)
        {
            var result = await db.Place.FirstOrDefaultAsync
                (e => e.Id == place.Id);
            if (result != null)
            {
                result.Name = place.Name;
                result.Description = place.Description;
                result.ApproximateCost = place.ApproximateCost;
                result.ImageUrl = place.ImageUrl;
                if (place.CategoryId != 0)
                {
                    result.CountryId = place.CountryId;
                }
                else if (place.Country != null)
                {
                    result.CountryId = place.Country.Id;
                }
                if (place.CategoryId != 0)
                {
                    result.CategoryId = place.CategoryId;
                }
                else if (place.Category != null)
                {
                    result.CategoryId = place.Category.Id;
                }
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }
        //DELETE PLACE
        public async Task DeletePlace(int placeId)
        {
            var result = await db.Place.FirstOrDefaultAsync(
                e => e.Id == placeId);
            if (result != null)
            {
                db.Place.Remove(result);
                await db.SaveChangesAsync();
            }
        }
    }
}
