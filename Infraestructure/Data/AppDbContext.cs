using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TravelTours.Core.Entities;

namespace TravelTours.Infraestructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Place> Place { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//Encargado de las migraciones
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
