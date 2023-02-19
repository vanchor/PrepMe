using Microsoft.AspNetCore.Mvc;
using PrepMe.DAL.Interfaces;
using PrepMe.Domain;
using PrepMe.Services.Intefaces;
using PrepMe.Services.ViewModels;

namespace PrepMe.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IApiParser _apiParser;
        public ProductController(IProductService productService, IApiParser apiParser)
        {
            _productService = productService;
            _apiParser = apiParser;
        }

        public async Task<IActionResult> SaveAll()
        {
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                string let = letter.ToString();
                var res = await _apiParser.SearchProductsAsync(let, 100);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (res.Data is not null)
                    {
                        await _productService.AddToDbAsync(res.Data);
                    }
                }
                else
                {
                    return BadRequest(res.Description);
                }
            }
            return Ok();
        }
    }
}
