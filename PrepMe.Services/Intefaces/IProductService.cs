using PrepMe.Domain;
using PrepMe.Services.ViewModels;

namespace PrepMe.Services.Intefaces
{
    public interface IProductService
    {
        public Task<BaseResponse<List<Product>>> AddToDbAsync(IEnumerable<string> productVM);
        public BaseResponse<IEnumerable<Product>> FindByName(string name, int number = 10);
    }
}
