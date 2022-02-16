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
    public class ProductsController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public ProductsController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var items = new List<ProductModel>();

            foreach(var item in await _context.Products.Include(x => x.Category).ToListAsync())
            {
                items.Add(new ProductModel(
                    item.Id,
                    item.Name,
                    item.Descripton,
                    new CategoryModel(item.Category.Id, item.Category.Name)
                    ));
            }

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductEntity(int id)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (productEntity == null)
            {
                return NotFound();
            }

            return new ProductModel(
                    productEntity.Id,
                    productEntity.Name,
                    productEntity.Descripton,
                    new CategoryModel(productEntity.Category.Id, productEntity.Category.Name)
                    );

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, ProductUpdateModel productUpdateModel)
        {
            if (id != productUpdateModel.Id)
            {
                return BadRequest();
            }

            var productEntity = await _context.Products.FindAsync(id);

            productEntity.Name = productUpdateModel.Name;
            productEntity.Descripton = productUpdateModel.Description;
            productEntity.CategoryId = productUpdateModel.CategoryId;
            
            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
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





        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductEntity(ProductCreateModel productCreateModel)
        {
            if(!await _context.Categories.AnyAsync(x => x.Id == productCreateModel.CategoryId))
            {
                return BadRequest();
            }

            var productEntity = new ProductEntity(
                productCreateModel.Name,
                productCreateModel.Description,
                productCreateModel.CategoryId
                );


            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            var _productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == productEntity.Id);

            return CreatedAtAction("GetProductEntity", new { id = productEntity.Id }, new ProductModel(
                    _productEntity.Id,
                    _productEntity.Name,
                    _productEntity.Descripton,
                    new CategoryModel(_productEntity.Category.Id, _productEntity.Category.Name)
                    ));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
