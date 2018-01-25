using System.Collections.Generic;
using TFL.Common.Enums;
using TFL.Services.Interfaces.RestApi;

namespace TFL.Services.RestApi
{
    public class RestApiRequest : IRestApiRequest
    {
        public string BaseUrl { get; set; }

        public string Resource { get; set; }

        public RestMethod Method { get; set; }

        public IRestAuthentication Authentication { get; set; }

        public Dictionary<string, string> Headers { get; set; } = null;

        public Dictionary<string, string> Parameters { get; set; } = null;

        public Dictionary<string, string> UrlSegments { get; set; } = null;
    }

    public class RestAuthentication : IRestAuthentication
    {
        public string UsernameIdentifier { get; set; }

        public string Username { get; set; }

        public string PasswordIdentifier { get; set; }

        public string Password { get; set; }
    }
}
