using TravelTours.Core.Entities;

namespace TravelTours.Core.Interfaces
{
    public interface IPlaceRepository
    {
        //signature of the methods
        Task<IReadOnlyList<Place>> GetPlaces();
        Task<Place> GetPlaceById(int id);
        Task<Place>AddPlace(Place place);
        Task<Place>UpdatePlace(Place place);
        Task DeletePlace(int id);
    }
}
