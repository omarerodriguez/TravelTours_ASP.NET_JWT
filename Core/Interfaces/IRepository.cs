using TravelTours.Core.Especification;

namespace TravelTours.Core.Interfaces
{
    public interface IRepository<T> where T:class
    {
        Task<T> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllASync();

        Task<T>GetEspecification(IEspecification<T> especification);
        Task<IReadOnlyList<T>> GetAllEspecification(IEspecification<T> especification);

    }
}

