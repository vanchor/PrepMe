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

namespace PrepMe.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<ProductVM>> AddToDb(IEnumerable<ProductVM> productVM)
        {
            try
            {
                List<ProductVM> result = new List<ProductVM>();
                foreach (var item in productVM)
                {

                    item.productName = item.productName.Trim();
                    item.categoryName = item.categoryName.Trim();
                    if (string.IsNullOrEmpty(item.productName) && string.IsNullOrEmpty(item.categoryName))
                    {
                        continue;
                    }
                    else
                    {
                        bool hasProduct = _productRepository.HasProduct(item.productName);
                        bool hasCategory = _categoryRepository.HasCategory(item.categoryName);
                        if (hasCategory && hasProduct)
                        {
                            continue;
                        }
                        else
                        {
                            result.Add(new ProductVM
                            {
                                productName = item.productName,
                                categoryName = item.categoryName,
                            });

                        }
                    }
                }
                //need to change
                _categoryRepository.AddRange(result);
                await _productRepository.SaveChangesAsync();
                await _categoryRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductVM>()
                {
                    Description = $"[AdToDb] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
            

        }
    }
}
