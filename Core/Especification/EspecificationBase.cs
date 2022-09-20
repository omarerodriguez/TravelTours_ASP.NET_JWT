using System.Linq.Expressions;

namespace TravelTours.Core.Especification
{
    public class EspecificationBase<T> : IEspecification<T>
    {
        public EspecificationBase(Expression<Func<T,bool>>Filter)
        {
            Filter = Filter;
        }
        public EspecificationBase()
        {

        }

        public Expression<Func<T, bool>> Filter { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }
    }
}
