#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public CategoriesController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            var items = new List<CategoryModel>();

            foreach (var category in await _context.Categories.ToListAsync())
            {
                items.Add(new CategoryModel(category.Id, category.Name));
            }

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);

            if (categoryEntity == null)
            {
                return NotFound();
            }

            return new CategoryModel(categoryEntity.Id, categoryEntity.Name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryEntity(int id, CategoryModel categoryModel)
        {
            if (id != categoryModel.Id)
            {
                return BadRequest();
            }

            var categoryEntity = await _context.Categories.FindAsync(categoryModel.Id);
            categoryEntity.Name = categoryModel.Name;

            _context.Entry(categoryEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> PostCategoryEntity(CategoryModel categoryModel)
        {
            var categoryEntity = new CategoryEntity(categoryModel.Name);

            if (await _context.Categories.AnyAsync(x => x.Name == categoryModel.Name))
            {
                return Conflict();
            }


            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryEntity", new { id = categoryEntity.Id }, new CategoryModel(categoryEntity.Id, categoryEntity.Name));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
