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

namespace PrepMe.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly PrepMeDbContext _prepMeDbContext;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private Category categoryID;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, PrepMeDbContext prepMeDbContext)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _prepMeDbContext = prepMeDbContext;
        }

        public async Task<BaseResponse<ProductVM>> AddToDbAsync(IEnumerable<ProductVM> productVM)
        {
            try
            {
                List<Product> result = new List<Product>();
                foreach (var item in productVM)
                {
                    item.productName = item.productName.Trim();
                    item.categoryName = item.categoryName.Trim();
                    if (!string.IsNullOrEmpty(item.productName) || !string.IsNullOrEmpty(item.categoryName))
                    {
                        bool hasProduct = _productRepository.IsProductExist(item.productName);
                        bool hasCategory = _categoryRepository.IsCategoryExist(item.categoryName);
                        if (hasCategory)
                        {
                            var categoryID = _prepMeDbContext.Categories.Where(s => s.CategoryName == item.categoryName);
                        }
                        else
                        {
                            _categoryRepository.Add();
                        }
                        if (!hasProduct)
                        {
                            result.Add(new Product
                            {
                                ProductName = item.productName,
                                Category = categoryID.CategoryId,
                            });
                        }
                    }
                }
                //need to change
                _categoryRepository.AddRange(result);
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
            

        }
    }
}
