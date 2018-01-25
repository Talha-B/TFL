using System;
using System.Net;
using TFL.Services.Interfaces.RestApi;

namespace TFL.Services.RestApi
{
    public class RestApiResponse : IRestApiResponse
    {
        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
