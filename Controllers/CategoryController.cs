using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTours.Core.Entities;
using TravelTours.Core.Interfaces;

namespace TravelTours.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> categoryRepo;
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(IRepository<Category> categoryRepo,ICategoryRepository categoryRepository)
        {
            this.categoryRepo = categoryRepo;
            this.categoryRepository = categoryRepository;
        }
        //READ CATEGORY
        [HttpGet("Categories")]
        public async Task<ActionResult<Category>> GetCategories()
        {
            return Ok(await categoryRepo.GetAllASync());
        }
        //READ CATEGORY BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var getCategory = await categoryRepository.GetCategoryById(id);
                if (getCategory == null)
                {
                    return NotFound();
                }
                return getCategory;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        //CREATE CATEGORY
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                var catResult = await categoryRepository.AddCategory(category);
                return CreatedAtAction(nameof(CreateCategory), new { Id = catResult.Id }, catResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new category record");
            }
        }
        //UPDATE CATEGORY
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest("category Id mismatch");
                }
                var catResult = await categoryRepository.UpdateCategory(category);
                if (catResult == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }
                return await categoryRepository.UpdateCategory(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Update category record");
            }
        }
        //DELETE CATEGORY
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                var catdelete = await categoryRepository.GetCategoryById(id);
                if (catdelete == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }
                await categoryRepository.DeleteCategory(id);
                return Ok($"Category with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error delete category record");
            }
        }
    }
   
}
