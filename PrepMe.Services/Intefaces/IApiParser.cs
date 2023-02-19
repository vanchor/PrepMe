using PrepMe.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.Services.Intefaces
{
    public interface IApiParser
    {
        public Task<BaseResponse<List<string>>> SearchProductsAsync(string query, int count = 10);

    }
}
