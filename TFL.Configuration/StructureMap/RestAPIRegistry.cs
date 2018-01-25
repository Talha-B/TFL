using StructureMap; 
using TFL.Services.Interfaces.RestApi;
using TFL.Services.RestApi;

namespace TFL.Configuration.StructureMap
{
    public class RestApiRegistry : Registry
    {
        public RestApiRegistry()
        {
            For<IRestApiCallService>().Use<RestApiCallService>();
            For<IRestApiRequest>().Use<RestApiRequest>();
            For<IRestApiResponse>().Use<RestApiResponse>();
        }
    }
}
