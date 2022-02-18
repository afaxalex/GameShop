using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.ReadAsync());
        }

        public async Task <ActionResult<ProductModel>> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _productService.ReadAsync(id));
        }

        public async Task<IActionResult> Playstation()
        {
            return View(await _productService.ReadAsync());
        }

        public async Task<IActionResult> Xbox()
        {
            return View(await _productService.ReadAsync());
        }

        public async Task<IActionResult> Nintendo()
        {
            return View(await _productService.ReadAsync());
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}