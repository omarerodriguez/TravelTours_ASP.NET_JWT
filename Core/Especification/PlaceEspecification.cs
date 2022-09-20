using TravelTours.Core.Entities;

namespace TravelTours.Core.Especification
{
    public class PlaceEspecification: EspecificationBase<Place>
    {
        public PlaceEspecification()
        {
            AddInclude(x => x.Country);
            AddInclude(x => x.Category);
        }

        public PlaceEspecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(x => x.Country);
            AddInclude(x => x.Category);
        }
    }
}
