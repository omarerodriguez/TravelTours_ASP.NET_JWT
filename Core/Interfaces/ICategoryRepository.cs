using TravelTours.Core.Entities;

namespace TravelTours.Core.Interfaces
{
    public interface ICategoryRepository
    {
        //signature of the methods
        Task<IReadOnlyList<Category>> GetCategories();
        Task<Category> GetCategoryById(int id); 
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
