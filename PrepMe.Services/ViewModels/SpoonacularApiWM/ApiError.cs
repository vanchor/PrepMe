using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.Services.ViewModels.SpoonacularApiWM
{
    internal class ApiError
    {
        public string status { get; set; }
        public HttpStatusCode code { get; set; }
        public string message { get; set; }
    }
}
