using Microsoft.EntityFrameworkCore;
using TravelTours.Core.Especification;

namespace TravelTours.Infraestructure
{
    public class EvaluatorEspecification<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IEspecification<T> especification)
        {
            var query = inputQuery;

            if (especification.Filter != null)
            {
                query = query.Where(especification.Filter); // p=>p.PaisId == 1
            }

            query = especification.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
