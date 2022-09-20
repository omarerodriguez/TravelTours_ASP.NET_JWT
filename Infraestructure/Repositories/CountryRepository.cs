using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTours.Core.Entities;
using TravelTours.Core.Interfaces;
using TravelTours.Infraestructure.Data;

namespace TravelTours.Infraestructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext db;

        public CountryRepository(AppDbContext db)
        {
            this.db = db;
        }
        //READ ALL COUNTRIES
        public async Task<IReadOnlyList<Country>> GetCountry()
        {
            return await db.Country.ToListAsync();
        }
        //READ COUNTRY BY ID
        public async Task<Country> GetCountryById(int id)
        {
            return await db.Country.FirstOrDefaultAsync(c => c.Id == id);
        }
        //CREATE COUNTRY
        public async Task<Country> AddCountry(Country country)
        {
            var countryResult = await db.Country.AddAsync(country);
            await db.SaveChangesAsync();
            return countryResult.Entity;
        }
        //UPDATE COUNTRY
        public async Task<Country> UpdateCountry(Country country)
        {
            var countryResult = await db.Country.FirstOrDefaultAsync(c=>c.Id == country.Id);
            if(countryResult == null)
            {
                countryResult.Name = country.Name;
                countryResult.State = country.State;
            }
            await db.SaveChangesAsync();
            return countryResult;
        }
        //DELETE COUNTRY
        public async Task DeleteCountry(int countryId)
        {
            var countryResult = await db.Country.FirstOrDefaultAsync(c => c.Id == countryId);
            if(countryResult != null)
            {
                db.Country.Remove(countryResult);
                await db.SaveChangesAsync();
            }
        }

    }
}
