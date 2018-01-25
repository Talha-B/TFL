using RestSharp;
using RestSharp.Authenticators;
using System;
using TFL.Common.Enums;
using TFL.Services.Interfaces.RestApi;

namespace TFL.Services.RestApi
{
    public class RestApiCallService : IRestApiCallService
    {
        public IRestApiResponse ExecuteGet(IRestApiRequest requestConfig)
        {
            #region Validation
            if (requestConfig == null)
            {
                throw new ArgumentNullException(paramName: nameof(requestConfig), message: $"A valid {nameof(requestConfig)} must be supplied");
            }

            if (string.IsNullOrEmpty(requestConfig.BaseUrl))
            {
                throw new ArgumentNullException(paramName: nameof(requestConfig.BaseUrl), message: $"A valid {nameof(requestConfig.BaseUrl)} must be supplied");
            }

            if (string.IsNullOrEmpty(requestConfig.Resource))
            {
                throw new ArgumentNullException(paramName: nameof(requestConfig.Resource), message: $"A valid {nameof(requestConfig.Resource)} must be supplied");
            }
            #endregion

            var client = new RestClient(requestConfig.BaseUrl);

            // Authenticate with credentials, if passed in
            if (requestConfig.Authentication != null)
            {
                client.Authenticator
                    = new SimpleAuthenticator(requestConfig.Authentication.UsernameIdentifier,
                        requestConfig.Authentication.Username,
                        requestConfig.Authentication.PasswordIdentifier,
                        requestConfig.Authentication.Password);
            }

            var request = new RestRequest(requestConfig.Resource);

            // Default method to GET if nothing passed in
            switch(requestConfig.Method)
            {
                case RestMethod.GET:
                    request.Method = Method.GET;
                    break;
                case RestMethod.POST:
                    request.Method = Method.POST;
                    break;
                case RestMethod.PUT:
                    request.Method = Method.PUT;
                    break;
                case RestMethod.DELETE:
                    request.Method = Method.DELETE;
                    break;
                default:
                    request.Method = Method.GET;
                    break;
            }

            // Send headers if passed in
            if (requestConfig.Headers != null && requestConfig.Headers.Count > 0)
            {
                foreach(var header in requestConfig.Headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            // Send parameters if passed in
            if (requestConfig.Parameters != null && requestConfig.Parameters.Count > 0)
            {
                foreach (var parameter in requestConfig.Parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            // Setup and send URL parameters if passed in
            if (requestConfig.UrlSegments != null && requestConfig.UrlSegments.Count > 0)
            {
                foreach (var urlSegment in requestConfig.UrlSegments)
                {
                    request.AddUrlSegment(urlSegment.Key, urlSegment.Value);
                }
            }

            var response = client.Execute(request);

            return new RestApiResponse
            { 
                Content = response.Content,
                StatusCode = response.StatusCode
            };
        }
    }
}
