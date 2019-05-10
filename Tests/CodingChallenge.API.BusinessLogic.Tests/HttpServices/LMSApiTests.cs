using System.Collections.Generic;
using CodingChallenge.API.BusinessLogic.CustomSection;
using CodingChallenge.API.BusinessLogic.HttpServices.LMS;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.LMS;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.Common.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.HttpServices
{
    public class LMSApiTests
    {
        [Fact]
        public void Post_to_LMS_Test()
        {
            var stubHttpClient = new Mock<ILMSHttpWrapper>();
            stubHttpClient.Setup(p => p.PostToLMS(It.IsAny<List<KeyValuePair<string,string>>>())).ReturnsAsync(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new System.Net.Http.StringContent("Response from Webform")
            });

            var apiConfigurationHelper = new Mock<IAPIConfigurationHelper>();

            apiConfigurationHelper.Setup(p => p.APIConfiguration)
                .Returns(new APIConfigurationSection {APILogging = new APILoggingElement {VerboseLogging = true}});

            var logger = new Mock<ICCAApiLogger>();

            var mapper = new Mock<IStaticMappingServices>();

            mapper.Setup(p => p.MapLMSFormModel(It.IsAny<LMSFormModel>()))
                .Returns(new List<KeyValuePair<string, string>>{new KeyValuePair<string, string>("FirstName","James")});

            stubHttpClient.Setup(p => p.DefaultRequestHeaders)
                .Returns(new System.Net.Http.HttpClient().DefaultRequestHeaders);

            var apiwrapper = new LMSApiWrapper(stubHttpClient.Object, apiConfigurationHelper.Object, logger.Object);

            var apiService = new LMSApiServices(apiwrapper,mapper.Object,logger.Object);

            apiService.SendToLMS(new LMSFormModel()).Should().BeTrue();

        }

        [Theory]
        [InlineData(System.Net.HttpStatusCode.NotFound)]
        [InlineData(System.Net.HttpStatusCode.InternalServerError)]
        [InlineData(System.Net.HttpStatusCode.BadRequest)]
        [InlineData(System.Net.HttpStatusCode.Unauthorized)]
        [InlineData(System.Net.HttpStatusCode.Forbidden)]
        [InlineData(System.Net.HttpStatusCode.MethodNotAllowed)]
        public void Post_to_LMS_Not_Success_Test(System.Net.HttpStatusCode status)
        {
            var stubHttpClient = new Mock<ILMSHttpWrapper>();
            stubHttpClient.Setup(p => p.PostToLMS(It.IsAny<List<KeyValuePair<string, string>>>())).ReturnsAsync(new System.Net.Http.HttpResponseMessage(status)
            {
                Content = new System.Net.Http.StringContent("Response from Webform")
            });

            var apiConfigurationHelper = new Mock<IAPIConfigurationHelper>();

            apiConfigurationHelper.Setup(p => p.APIConfiguration)
                .Returns(new APIConfigurationSection { APILogging = new APILoggingElement { VerboseLogging = true } });

            var loggingService = new Mock<ILoggingService>();

            var logger = new Mock<ICCAApiLogger>();
            logger.Setup(p => p.Log()).Returns(loggingService.Object);

            var mapper = new Mock<IStaticMappingServices>();

            mapper.Setup(p => p.MapLMSFormModel(It.IsAny<LMSFormModel>()))
                .Returns(new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("FirstName", "James") });

            stubHttpClient.Setup(p => p.DefaultRequestHeaders)
                .Returns(new System.Net.Http.HttpClient().DefaultRequestHeaders);

            var apiwrapper = new LMSApiWrapper(stubHttpClient.Object, apiConfigurationHelper.Object, logger.Object);

            var apiService = new LMSApiServices(apiwrapper, mapper.Object, logger.Object);

            apiService.SendToLMS(new LMSFormModel()).Should().BeFalse();

        }
    }
}
