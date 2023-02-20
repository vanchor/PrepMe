using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Mvc;
using PrepMe.DAL.Interfaces;
using PrepMe.Domain;
using PrepMe.Services.Intefaces;
using PrepMe.Services.ViewModels;

namespace PrepMe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IApiParser _apiParser;
        public ProductController(IProductService productService, IApiParser apiParser)
        {
            _productService = productService;
            _apiParser = apiParser;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> SearchByName(string productName, int count = 10)
        {
            var resFromDb = _productService.FindByName(productName);
            if (resFromDb.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (resFromDb.Data is not null && resFromDb.Data.Count() < count)
                {
                    var resFromApi = await _apiParser.SearchProductsAsync(productName, 100);
                    if (resFromApi.Data is not null 
                        && resFromApi.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var res = await _productService.AddToDbAsync(resFromApi.Data);
                        if (res.Data is not null
                            && res.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return Ok(res.Data.Take(10).ToList());
                        }
                        else
                            return BadRequest(res.Description);
                    }
                    else
                    {
                        return BadRequest(resFromApi.Description);
                    }
                }
                else 
                    return Ok(resFromDb.Data.Take(10).ToList());
            }
            else
            {
                return BadRequest(resFromDb.Description);
            }
        }
    }
}
