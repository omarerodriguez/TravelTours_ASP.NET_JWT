using AutoMapper;
using TravelTours.Core.Entities;
using TravelTours.Dtos;

namespace TravelTours.Helpers
{
    public class PlaceUrlResolve: IValueResolver<Place,PlaceDto, string>
    {
        private readonly IConfiguration config;

        public PlaceUrlResolve(IConfiguration config)
        {
            this.config = config;
        }
        public string Resolve(Place source, PlaceDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return config["ApiUrl"] + source.ImageUrl;
            }
            return null;
        }
    }
}
