using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> ReadAsync();
        Task<ProductModel> ReadAsync(int id);
    }
    public class ProductService : IProductService
    {

        public async Task<IEnumerable<ProductModel>> ReadAsync()
        {
            IEnumerable<ProductModel> products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7177/api/Products");
            }

            return products;
        }

        public async Task<ProductModel> ReadAsync(int id)
        {
            ProductModel product = new ProductModel();

            using (var client = new HttpClient())
            {
                product = await client.GetFromJsonAsync<ProductModel>($"https://localhost:7177/api/Products/{id}");
            }

            return product;
        }
    }
}
