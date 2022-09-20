using Microsoft.EntityFrameworkCore;
using TravelTours.Core.Entities;
using TravelTours.Core.Interfaces;
using TravelTours.Infraestructure.Data;

namespace TravelTours.Infraestructure.Repositories
{
    public class CategoryRespository : ICategoryRepository
    {
        private readonly AppDbContext db;

        public CategoryRespository(AppDbContext db)
        {
            this.db = db;
        }
        //READ ALL CATEGORIES
        public async Task<IReadOnlyList<Category>> GetCategories()
        {
            return await db.Category.ToListAsync();
        }
        //READ CATEGORY BY ID
        public async Task<Category> GetCategoryById(int id)
        {
            return await db.Category.FirstOrDefaultAsync(c => c.Id == id);
        }
        //CREATE CATEGORY
        public async Task<Category> AddCategory(Category category)
        {
            var catResult = await db.AddAsync(category);
            await db.SaveChangesAsync();
            return catResult.Entity;
        }
        //UPDATE CATEGORY
        public async Task<Category> UpdateCategory(Category category)
        {
            var catResult = await db.Category.FirstOrDefaultAsync
                (c => c.Id == category.Id);
            if (catResult != null)
            {
                catResult.Name = category.Name;
                catResult.State = category.State;

                await db.SaveChangesAsync();
                return catResult;
            }
            return null;
        }
        //DELETE CATEGORY
        public async Task DeleteCategory(int categoryId)
        {
            var catResult = await db.Category.FirstOrDefaultAsync
                (c => c.Id == categoryId);
            if (catResult != null)
            {
                db.Category.Remove(catResult);
                await db.SaveChangesAsync();
            }
        }
    }
}
