using AutoMapper;
using TravelTours.Core.Entities;
using TravelTours.Dtos;

namespace TravelTours.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Place, PlaceDto>()
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom<PlaceUrlResolve>());
        }
    }
}
