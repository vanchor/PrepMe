using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.Services.ViewModels
{
    public class BaseResponse<T>
    {
        public string Description { get; set; } = "";
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }

        public BaseResponse()
        {
                
        }
        public BaseResponse(HttpStatusCode statusCode, string description)
        {
            Description = description;
            StatusCode = statusCode;
        }

        public BaseResponse(string description, T? data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Description = description;
            StatusCode = statusCode;
            Data = data;
        }
    }
}