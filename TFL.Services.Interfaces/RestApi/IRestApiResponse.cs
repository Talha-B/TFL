using System.Net;

namespace TFL.Services.Interfaces.RestApi
{
    public interface IRestApiResponse
    {
        string Content { get; set; }

        HttpStatusCode StatusCode { get; set; }
    }
}
