using System.Linq.Expressions;

namespace TravelTours.Core.Especification
{
    public interface IEspecification<T>
    {
        Expression<Func<T,bool>> Filter { get; }

        List<Expression<Func<T,object>>> Includes { get; }
    }
}
