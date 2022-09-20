using TravelTours.Core.Entities;

namespace TravelTours.Core.Interfaces
{
    public interface ICountryRepository
    {
        //signaturre of the methods
        Task<IReadOnlyList<Country>> GetCountry();
        Task<Country>GetCountryById(int id);
        Task<Country> AddCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        Task DeleteCountry(int id);
    }
}
