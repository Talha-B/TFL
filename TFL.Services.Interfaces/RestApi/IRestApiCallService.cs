namespace TFL.Services.Interfaces.RestApi
{
    public interface IRestApiCallService
    {
        IRestApiResponse ExecuteGet(IRestApiRequest requestConfig);
    }
}
