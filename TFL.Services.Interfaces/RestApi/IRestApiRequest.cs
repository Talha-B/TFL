using System.Collections.Generic;
using TFL.Common.Enums;

namespace TFL.Services.Interfaces.RestApi
{
    public interface IRestApiRequest
    {
        string BaseUrl { get; set; }

        string Resource { get; set; }

        RestMethod Method { get; set; }

        IRestAuthentication Authentication { get; set; }

        Dictionary<string, string> Headers { get; set; }

        Dictionary<string, string> Parameters { get; set; }

        Dictionary<string, string> UrlSegments { get; set; }
    }

    public interface IRestAuthentication
    {
        string UsernameIdentifier { get; set; }

        string Username { get; set; }

        string PasswordIdentifier { get; set; }

        string Password { get; set; }
    }
}