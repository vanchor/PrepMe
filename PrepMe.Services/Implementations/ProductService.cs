using Microsoft.IdentityModel.Tokens;
using PrepMe.Services.Intefaces;
using PrepMe.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrepMe.DAL.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using PrepMe.Domain;
using PrepMe.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PrepMe.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<List<Product>>> AddToDbAsync(IEnumerable<string> products)
        {
            try
            {
                List<Product> result = new List<Product>();
                foreach (var item in products)
                {
                    string newItem = item.Trim();
                    if (!string.IsNullOrEmpty(newItem)
                        && !result.Any(x => x.ProductName.Equals(newItem, StringComparison.CurrentCultureIgnoreCase)) 
                        && !_productRepository.IsProductExist(newItem)
                        )
                    {
                        result.Add(new Product(newItem));
                    }
                }
                _productRepository.AddRange(result);
                await _productRepository.SaveChangesAsync();
                return new BaseResponse<List<Product>>(
                    description: "Success",
                    data: result);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>(
                    description: $"[AdToDb] : {ex.Message}",
                    statusCode: System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public BaseResponse<List<Product>> FindByName(string name, int number = 10)
        {
            return new BaseResponse<List<Product>>(
                description: "",
                data: new List<Product>
                {
                    {new Product{ProductName = name} }
                });
        }
    }
}
