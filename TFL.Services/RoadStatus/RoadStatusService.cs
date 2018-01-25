using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TFL.Common.Enums;
using TFL.Common.Settings.TflApi;
using TFL.Services.Interfaces.RestApi;
using TFL.Services.Interfaces.RoadStatus;
using TFL.Services.RestApi;

namespace TFL.Services.RoadStatus
{
    public class RoadStatusService : IRoadStatusService
    {
        private readonly IRestApiCallService _restApiCallService;
        private readonly TflApiSettings _tflApiSettings;

        public RoadStatusService(IRestApiCallService restApiCallService, IOptions<TflApiSettings> tflApiSettings)
        {
            this._restApiCallService = restApiCallService ?? throw new ArgumentNullException(paramName: nameof(restApiCallService),
                    message: $"A valid {nameof(restApiCallService)} must be supplied");

            this._tflApiSettings = tflApiSettings.Value ?? throw new ArgumentNullException(paramName: nameof(tflApiSettings),
                    message: $"A valid {nameof(tflApiSettings)} must be supplied");
        }

        public IRoadStatus GetStatus(string roadId)
        {
            if (string.IsNullOrEmpty(roadId))
            {
                throw new ArgumentNullException(paramName: nameof(roadId), message: $"A valid {nameof(roadId)} must be supplied");
            }

            var roadResource = this._tflApiSettings
                    ?.Resources
                    ?.FirstOrDefault(r => r.Name.Equals("road", StringComparison.OrdinalIgnoreCase))
                    ?.Value;

            if (roadResource == null)
            {
                throw new NullReferenceException($"A valid TFL road resource must be supplied");
            }

            var apiRequestConfig = new RestApiRequest()
            {
                BaseUrl = this._tflApiSettings.BaseUrl,
                Resource = roadResource,
                Authentication = new RestAuthentication
                {
                    UsernameIdentifier = this._tflApiSettings.Authentication.UsernameIdentifier,
                    Username = this._tflApiSettings.Authentication.Username,
                    PasswordIdentifier = this._tflApiSettings.Authentication.PasswordIdentifier,
                    Password = this._tflApiSettings.Authentication.Password,
                },
                Method = RestMethod.GET,
                UrlSegments = new Dictionary<string, string> { { "roadId", roadId } }
            };

            var lookupResponse = this._restApiCallService.ExecuteGet(apiRequestConfig);

            if (lookupResponse?.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<RoadStatusLookupSuccessResponse[]>(lookupResponse.Content);
                if (result != null && result.Length > 0)
                {
                    return new RoadStatus
                    {
                        RequestedRoad = roadId,
                        DisplayName = result[0].DisplayName,
                        Status = result[0].StatusSeverity,
                        StatusDescription = result[0].StatusSeverityDescription,
                        ErrorCode = 0,
                        ErrorDescription = string.Empty
                    };
                }
            }

            if (lookupResponse?.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var result = JsonConvert.DeserializeObject<RoadStatusLookupErrorResponse>(lookupResponse.Content);
                if (result != null)
                {
                    return new RoadStatus
                    {
                        RequestedRoad = roadId,
                        ValidRoad = false,
                        ErrorCode = result.HttpStatusCode,
                        ErrorDescription = result.Message
                    };
                }
            }

            return null;
        }
    }
}