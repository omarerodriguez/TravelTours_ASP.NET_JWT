using Microsoft.EntityFrameworkCore;
using TravelTours.Core.Especification;
using TravelTours.Core.Interfaces;
using TravelTours.Infraestructure.Data;

namespace TravelTours.Infraestructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext db;

        public Repository(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<IReadOnlyList<T>> GetAllASync()
        {
            return await db.Set<T>().ToListAsync();
        }
        public async Task<T> GetAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllEspecification(IEspecification<T> especification)
        {
            return await ApplyEspecification(especification).ToListAsync();
        }

        public async Task<T> GetEspecification(IEspecification<T> especification)
        {
            return await ApplyEspecification(especification).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplyEspecification(IEspecification<T> especification)
        {
            return EvaluatorEspecification<T>.GetQuery(db.Set<T>().AsQueryable(), especification);
        }

    }
}
