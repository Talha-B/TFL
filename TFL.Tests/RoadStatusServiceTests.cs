using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TFL.Common.Settings.TflApi;
using TFL.Services.Interfaces.RestApi;
using TFL.Services.RestApi;
using TFL.Services.RoadStatus;

namespace TFL.Tests
{
    [TestFixture]
    public class RoadServiceTests
    {
        private RoadStatusService _sut;
        private Mock<IRestApiCallService> _mockRestApiCallService;
        private TflApiSettings _dummytflApiSettings;

        private IRestApiResponse _validResponseForA2;
        private IRestApiResponse _notFoundResponseForA233;

        public RoadServiceTests()
        {
            this._mockRestApiCallService = new Mock<IRestApiCallService>();

            this._dummytflApiSettings = new TflApiSettings()
            {
                BaseUrl = "https://api.tfl.gov.uk/",
                Authentication = new Authentication()
                {
                    UsernameIdentifier = "api_id",
                    Username = "XXX",
                    PasswordIdentifier = "api_key",
                    Password = "XXX"
                },
                Resources = new List<Resource>()
                {
                    new Resource { Name = "road", Value = "road/{roadId}" }
                }
            };

            this._validResponseForA2 = new RestApiResponse()
            {
                Content = JsonConvert.SerializeObject(
                    new RoadStatusLookupSuccessResponse[1] 
                    {
                        new RoadStatusLookupSuccessResponse()
                        {
                            Type = "Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities",
                            Id = "a2",
                            DisplayName = "A2",
                            Bounds = "[[-0.0857,51.44091],[0.17118,51.49438]]",
                            Envelope = "[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]",
                            StatusSeverity = "Good",
                            StatusSeverityDescription = "No Exceptional Delays",
                            Url = "/Road/a2"
                        }
                    }
                ),
                StatusCode = System.Net.HttpStatusCode.OK
            };

            this._notFoundResponseForA233 = new RestApiResponse()
            {
                Content = JsonConvert.SerializeObject(                    
                    new RoadStatusLookupErrorResponse()
                    {
                        Type = "Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities",
                        ExceptionType = "EntityNotFoundException",
                        HttpStatusCode = 404,
                        HttpStatus = "NotFound",
                        RelativeUri = "/Road/A233",
                        Message = "The following road id is not recognised: A233",
                        TimestampUtc = DateTime.Parse("2017-11-21T14:37:39.7206118Z")
                    }
                ),
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            var options = Options.Create(_dummytflApiSettings);
            this._sut = new RoadStatusService(_mockRestApiCallService.Object, options);
        }

        [Test]
        public void GetStatus_AValidRoadIsSpecified_RoadDisplayNameIsNotNullOrEmpty()
        {
            // Arrange
            var road = "a2";

            this._mockRestApiCallService
                .Setup(r => r.ExecuteGet(It.IsAny<RestApiRequest>()))
                .Returns(this._validResponseForA2);

            // Act
            var roadStatus = this._sut.GetStatus(road);

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(roadStatus.DisplayName));
        }

        [Test]
        public void GetStatus_AValidRoadIsSpecified_RoadStatusSeverityIsNotNullOrEmpty()
        {
            // Arrange
            var road = "a2";

            this._mockRestApiCallService
                .Setup(r => r.ExecuteGet(It.IsAny<RestApiRequest>()))
                .Returns(this._validResponseForA2);

            // Act
            var roadStatus = this._sut.GetStatus(road);

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(roadStatus.Status));
        }

        [Test]
        public void GetStatus_AValidRoadIsSpecified_RoadStatusSeverityDescriptionIsNotNullOrEmpty()
        {
            // Arrange
            var road = "a2";

            this._mockRestApiCallService
                .Setup(r => r.ExecuteGet(It.IsAny<RestApiRequest>()))
                .Returns(this._validResponseForA2);

            // Act
            var roadStatus = this._sut.GetStatus(road);

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(roadStatus.StatusDescription));
        }

        [Test]
        public void GetStatus_AValidRoadIsSpecified_ZeroErrorCodeReturned()
        {
            // Arrange
            var road = "a2";

            this._mockRestApiCallService
                .Setup(r => r.ExecuteGet(It.IsAny<RestApiRequest>()))
                .Returns(this._validResponseForA2);

            // Act
            var roadStatus = this._sut.GetStatus(road);

            // Assert
            Assert.IsTrue(roadStatus.ErrorCode == 0);
        }

        [Test]
        public void GetStatus_AnInvalidRoadIsSpecified_NotValidReturned()
        {
            // Arrange
            var road = "a233";

            this._mockRestApiCallService
                .Setup(r => r.ExecuteGet(It.IsAny<RestApiRequest>()))
                .Returns(this._notFoundResponseForA233);

            // Act
            var roadStatus = this._sut.GetStatus(road);

            // Assert
            Assert.IsFalse(roadStatus.ValidRoad);
        }

        [Test]
        public void GetStatus_AnInvalidRoadIsSpecified_NonZeroErrorCodeReturned()
        {
            // Arrange
            var road = "a233";

            this._mockRestApiCallService
                .Setup(r => r.ExecuteGet(It.IsAny<RestApiRequest>()))
                .Returns(this._notFoundResponseForA233);

            // Act
            var roadStatus = this._sut.GetStatus(road);

            // Assert
            Assert.IsTrue(roadStatus.ErrorCode > 0);
        }
    }
}
