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

        public async Task<BaseResponse<List<Product>>> AddToDbAsync(IEnumerable<ProductVM> productVM)
        {
            try
            {
                List<Product> result = new List<Product>();
                foreach (var item in productVM)
                {
                    item.productName = item.productName.Trim();
                    if (!string.IsNullOrEmpty(item.productName)
                        && !result.Any(x => x.ProductName.Equals(item.productName, StringComparison.CurrentCultureIgnoreCase)) 
                        && !_productRepository.IsProductExist(item.productName)
                        )
                    {
                        result.Add(new Product
                        {
                            ProductName = item.productName
                        });
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
