using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PrepMe.Services;
using PrepMe.Services.Intefaces;
using PrepMe.Services.ViewModels;
using PrepMe.Services.ViewModels.SpoonacularApiWM;
using System.Net.Http.Headers;

namespace PrepMe.Services.Implementations
{
    public class SpoonacularApi : IApiParser
    {
        private readonly string baseURI = "https://api.spoonacular.com/";
        private readonly string _apiToken;
        private HttpClient client = new HttpClient();

        public SpoonacularApi(IConfiguration configuration)
        {
            _apiToken = configuration.GetSection("AppSettings:SpoonacularToken").Value 
                ?? throw new NullReferenceException("There is no AuthToken for Spoonacular Api in the app settings.");
            client.BaseAddress = new Uri(baseURI);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        private async Task<BaseResponse<T>> ParseApiError<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsAsync<ApiError>();
            return new BaseResponse<T>(
                    description: responseContent.message,
                    statusCode: responseContent.code);
        }

        public async Task<BaseResponse<List<string>>> SearchProductsAsync(string query, int count = 10)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"food/ingredients/search?apiKey={_apiToken}&query={query}&sort=name");

                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsAsync<Results<Product>>();

                    return new BaseResponse<List<string>>(
                        description: "Success",
                        data: product.results.Select(x => x.name).ToList());
                }

                return await ParseApiError<List<string>>(response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<string>>(
                    description: ex.Message,
                    statusCode: System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
