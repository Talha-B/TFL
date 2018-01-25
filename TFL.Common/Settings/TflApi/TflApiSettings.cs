using System.Collections.Generic;

namespace TFL.Common.Settings.TflApi
{
    public class TflApiSettings
    {
        public string BaseUrl { get; set; }

        public Authentication Authentication { get; set; }

        public List<Resource> Resources { get; set; }
    }

    public class Authentication
    {
        public string UsernameIdentifier { get; set; }

        public string Username { get; set; }

        public string PasswordIdentifier { get; set; }

        public string Password { get; set; }
    }

    public class Resource
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
