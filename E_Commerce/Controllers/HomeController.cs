using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductModel> products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7177/api/Products");
            }

            return View(products);
        }

        public async Task <ActionResult<ProductModel>> Details(int id)
        {
            ProductModel product = new ProductModel();

            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                product = await client.GetFromJsonAsync<ProductModel>($"https://localhost:7177/api/Products/{id}");
            }

            return View(product);
        }

        public async Task<IActionResult> Playstation()
        {
            IEnumerable<ProductModel> products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7177/api/Products");
            }

            return View(products);
        }

        public async Task<IActionResult> Xbox()
        {
            IEnumerable<ProductModel> products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7177/api/Products");
            }

            return View(products);
        }


        public async Task<IActionResult> Nintendo()
        {
            IEnumerable<ProductModel> products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7177/api/Products");
            }

            return View(products);
        }


        
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}