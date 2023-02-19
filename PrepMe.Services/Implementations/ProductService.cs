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

        public async Task<BaseResponse<ProductVM>> AddToDbAsync(IEnumerable<ProductVM> productVM)
        {
            try
            {
                List<Product> result = new List<Product>();
                foreach (var item in productVM)
                {
                    item.productName = item.productName.Trim();
                    if (!string.IsNullOrEmpty(item.productName))
                    {
                        bool hasProduct = _productRepository.IsProductExist(item.productName);
                        if (!hasProduct)
                        {
                            result.Add(new Product
                            {
                                ProductName = item.productName,
                            });
                        }
                    }
                }
                _productRepository.AddRange(result);
                await _productRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductVM>()
                {
                    Description = $"[AdToDb] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }

            return new BaseResponse<ProductVM>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
