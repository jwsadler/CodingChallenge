using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodingChallenge.API.BusinessLogic.CustomSection;
using CodingChallenge.API.BusinessLogic.HttpServices.Pixabay;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Pixabay;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.Common.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.HttpServices
{
    public class PixabayApiTests
    {
        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.MethodNotAllowed)]
        public void Post_to_Pixabay_Not_Success_Test(HttpStatusCode status)
        {


            var stubHttpClient = new Mock<IPixabayHttpWrapper>();
            stubHttpClient.Setup(p => p.GetPixabayResponse(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(status)
            {
                Content = new StringContent(PIXABAY_RESPONSE)
            });

            var apiConfigurationHelper = new Mock<IAPIConfigurationHelper>();

            apiConfigurationHelper.Setup(p => p.APIConfiguration)
                .Returns(new APIConfigurationSection { APILogging = new APILoggingElement { VerboseLogging = true } });

            var loggingService = new Mock<ILoggingService>();

            var logger = new Mock<ICodingChallengeApiLogger>();
            logger.Setup(p => p.Log()).Returns(loggingService.Object);

            stubHttpClient.Setup(p => p.DefaultRequestHeaders)
                .Returns(new HttpClient().DefaultRequestHeaders);

            var pixabayApiWrapper = new PixabayApiWrapper(stubHttpClient.Object, apiConfigurationHelper.Object, logger.Object);

            var apiService = new PixabayApiServices(pixabayApiWrapper, apiConfigurationHelper.Object);

            var response = apiService.Pixabay(new CodingChallengeRequestModel { Query = "red car" }, true);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(status);
        }

        [Fact]
        public void Post_to_Pixabay_Test()
        {
            var stubHttpClient = new Mock<IPixabayHttpWrapper>();
            stubHttpClient.Setup(p => p.GetPixabayResponse(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(PIXABAY_RESPONSE)
            });

            var apiConfigurationHelper = new Mock<IAPIConfigurationHelper>();

            apiConfigurationHelper.Setup(p => p.APIConfiguration)
                .Returns(new APIConfigurationSection { APILogging = new APILoggingElement { VerboseLogging = true } });

            var logger = new Mock<ICodingChallengeApiLogger>();

            stubHttpClient.Setup(p => p.DefaultRequestHeaders)
                .Returns(new HttpClient().DefaultRequestHeaders);

            var pixabayApiWrapper = new PixabayApiWrapper(stubHttpClient.Object, apiConfigurationHelper.Object, logger.Object);

            var apiService = new PixabayApiServices(pixabayApiWrapper, apiConfigurationHelper.Object);

            var response = apiService.Pixabay(new CodingChallengeRequestModel { Query = "red car" }, true);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        public const string PIXABAY_RESPONSE = @"{
    ""totalHits"": 179,
    ""hits"": [
        {
            ""largeImageURL"": ""https://pixabay.com/get/e831b60f2ff11c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_1280.png"",
            ""webformatHeight"": 586,
            ""webformatWidth"": 640,
            ""likes"": 88,
            ""imageWidth"": 1920,
            ""id"": 147075,
            ""user_id"": 30363,
            ""views"": 37112,
            ""comments"": 15,
            ""pageURL"": ""https://pixabay.com/vectors/sunflower-flower-yellow-orange-147075/"",
            ""imageHeight"": 1759,
            ""webformatURL"": ""https://pixabay.com/get/e831b60f2ff11c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 137,
            ""tags"": ""sunflower, flower, yellow"",
            ""downloads"": 7456,
            ""user"": ""OpenClipart-Vectors"",
            ""favorites"": 108,
            ""imageSize"": 575548,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2013/08/21/16-02-05-675_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2013/07/12/13/27/sunflower-147075_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/eb33b60a2ef3033ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 623,
            ""webformatWidth"": 640,
            ""likes"": 142,
            ""imageWidth"": 1920,
            ""id"": 2675672,
            ""user_id"": 6190330,
            ""views"": 24048,
            ""comments"": 5,
            ""pageURL"": ""https://pixabay.com/photos/abstract-art-works-of-art-2675672/"",
            ""imageHeight"": 1871,
            ""webformatURL"": ""https://pixabay.com/get/eb33b60a2ef3033ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""photo"",
            ""previewHeight"": 146,
            ""tags"": ""abstract, art, works of art"",
            ""downloads"": 10286,
            ""user"": ""gorartser"",
            ""favorites"": 228,
            ""imageSize"": 1232662,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2017/08/24/22-01-16-223_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2017/08/24/07/40/abstract-2675672_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/ea34b10f29f31c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_1280.png"",
            ""webformatHeight"": 482,
            ""webformatWidth"": 640,
            ""likes"": 80,
            ""imageWidth"": 1920,
            ""id"": 310017,
            ""user_id"": 3736,
            ""views"": 58826,
            ""comments"": 10,
            ""pageURL"": ""https://pixabay.com/vectors/flowers-floral-leaves-pattern-310017/"",
            ""imageHeight"": 1447,
            ""webformatURL"": ""https://pixabay.com/get/ea34b10f29f31c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 112,
            ""tags"": ""flowers, floral, leaves"",
            ""downloads"": 5238,
            ""user"": ""Clker-Free-Vector-Images"",
            ""favorites"": 94,
            ""imageSize"": 419964,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2012/04/01/00-18-38-212_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2014/04/03/10/10/flowers-310017_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/ea36b50f21f5023ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 640,
            ""likes"": 86,
            ""imageWidth"": 1920,
            ""id"": 3340913,
            ""user_id"": 637659,
            ""views"": 9751,
            ""comments"": 4,
            ""pageURL"": ""https://pixabay.com/illustrations/flowers-pattern-lotus-lily-3340913/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/ea36b50f21f5023ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/ai"",
            ""previewHeight"": 150,
            ""tags"": ""flowers, pattern, lotus"",
            ""downloads"": 4104,
            ""user"": ""monstreh"",
            ""favorites"": 203,
            ""imageSize"": 1937637,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2017/12/06/15-05-40-37_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2018/04/22/12/25/flowers-3340913_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e830b70c2cf7033ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 636,
            ""likes"": 53,
            ""imageWidth"": 1906,
            ""id"": 1563432,
            ""user_id"": 2722131,
            ""views"": 8030,
            ""comments"": 5,
            ""pageURL"": ""https://pixabay.com/vectors/sunflower-summer-plant-garden-1563432/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/e830b70c2cf7033ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 150,
            ""tags"": ""sunflower, summer, plant"",
            ""downloads"": 1983,
            ""user"": ""SabrinaSchleifer"",
            ""favorites"": 60,
            ""imageSize"": 733453,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2017/09/06/19-03-49-612_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2016/08/02/13/40/sun-flower-1563432_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e831b40b2cf7033ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 476,
            ""likes"": 62,
            ""imageWidth"": 1429,
            ""id"": 1454432,
            ""user_id"": 2641041,
            ""views"": 5779,
            ""comments"": 3,
            ""pageURL"": ""https://pixabay.com/illustrations/dog-character-animal-flower-cute-1454432/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/e831b40b2cf7033ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/ai"",
            ""previewHeight"": 150,
            ""tags"": ""dog, character, animal"",
            ""downloads"": 1971,
            ""user"": ""GraphicMama-team"",
            ""favorites"": 75,
            ""imageSize"": 638732,
            ""previewWidth"": 111,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2016/06/01/15-27-35-456_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2016/06/13/15/23/dog-1454432_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e833b30f2ff31c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_1280.png"",
            ""webformatHeight"": 403,
            ""webformatWidth"": 640,
            ""likes"": 41,
            ""imageWidth"": 1920,
            ""id"": 162077,
            ""user_id"": 30363,
            ""views"": 15916,
            ""comments"": 9,
            ""pageURL"": ""https://pixabay.com/vectors/flowers-bloom-sun-flower-plants-162077/"",
            ""imageHeight"": 1210,
            ""webformatURL"": ""https://pixabay.com/get/e833b30f2ff31c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 94,
            ""tags"": ""flowers, bloom, sun flower"",
            ""downloads"": 2501,
            ""user"": ""OpenClipart-Vectors"",
            ""favorites"": 49,
            ""imageSize"": 988000,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2013/08/21/16-02-05-675_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2013/07/13/14/05/flowers-162077_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e83db2082cf4013ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 552,
            ""webformatWidth"": 640,
            ""likes"": 34,
            ""imageWidth"": 1920,
            ""id"": 1837400,
            ""user_id"": 162579,
            ""views"": 2624,
            ""comments"": 2,
            ""pageURL"": ""https://pixabay.com/vectors/flower-floral-botanical-plant-1837400/"",
            ""imageHeight"": 1656,
            ""webformatURL"": ""https://pixabay.com/get/e83db2082cf4013ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 129,
            ""tags"": ""flower, floral, botanical"",
            ""downloads"": 975,
            ""user"": ""Prawny"",
            ""favorites"": 50,
            ""imageSize"": 327069,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2014/02/17/17-51-05-748_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2016/11/18/23/56/flower-1837400_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/ea35b50a2ff71c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 638,
            ""likes"": 52,
            ""imageWidth"": 1914,
            ""id"": 304573,
            ""user_id"": 3736,
            ""views"": 10910,
            ""comments"": 6,
            ""pageURL"": ""https://pixabay.com/vectors/red-green-blue-shaded-yellow-304573/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/ea35b50a2ff71c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 150,
            ""tags"": ""red, green, blue"",
            ""downloads"": 3040,
            ""user"": ""Clker-Free-Vector-Images"",
            ""favorites"": 72,
            ""imageSize"": 326388,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2012/04/01/00-18-38-212_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2014/04/02/10/47/red-304573_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e13cb00b2bf11c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 640,
            ""likes"": 42,
            ""imageWidth"": 1920,
            ""id"": 891435,
            ""user_id"": 1306029,
            ""views"": 16582,
            ""comments"": 3,
            ""pageURL"": ""https://pixabay.com/illustrations/season-winter-spring-summer-fall-891435/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/e13cb00b2bf11c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_640.png"",
            ""type"": ""vector/ai"",
            ""previewHeight"": 150,
            ""tags"": ""season, winter, spring"",
            ""downloads"": 3949,
            ""user"": ""UnboxScience"",
            ""favorites"": 74,
            ""imageSize"": 294313,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2015/09/03/14-29-34-96_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2015/08/16/18/35/season-891435_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/eb36b1072cf5093ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 640,
            ""likes"": 52,
            ""imageWidth"": 1920,
            ""id"": 2308418,
            ""user_id"": 2595351,
            ""views"": 3711,
            ""comments"": 2,
            ""pageURL"": ""https://pixabay.com/illustrations/love-peace-romantic-romance-happy-2308418/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/eb36b1072cf5093ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/ai"",
            ""previewHeight"": 150,
            ""tags"": ""love, peace, romantic"",
            ""downloads"": 1303,
            ""user"": ""DavidRockDesign"",
            ""favorites"": 80,
            ""imageSize"": 380025,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2018/02/12/11-53-38-901_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2017/05/12/23/32/love-2308418_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/ed34b50f2ff7003ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 632,
            ""likes"": 5,
            ""imageWidth"": 1894,
            ""id"": 4140731,
            ""user_id"": 5214362,
            ""views"": 161,
            ""comments"": 3,
            ""pageURL"": ""https://pixabay.com/illustrations/easter-rabbits-spring-flowers-4140731/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/ed34b50f2ff7003ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/ai"",
            ""previewHeight"": 150,
            ""tags"": ""easter, rabbits, spring"",
            ""downloads"": 59,
            ""user"": ""deMysticWay"",
            ""favorites"": 5,
            ""imageSize"": 950427,
            ""previewWidth"": 149,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2017/10/10/20-28-05-23_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2019/04/20/00/27/easter-4140731_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/ed30b6072ee90021d85a5854e74e4292e173e6dc04b0144492f6c67bafeab3_1280.png"",
            ""webformatHeight"": 442,
            ""webformatWidth"": 640,
            ""likes"": 98,
            ""imageWidth"": 1920,
            ""id"": 45786,
            ""user_id"": 3736,
            ""views"": 44213,
            ""comments"": 12,
            ""pageURL"": ""https://pixabay.com/vectors/flowers-bees-garden-green-grass-45786/"",
            ""imageHeight"": 1326,
            ""webformatURL"": ""https://pixabay.com/get/ed30b6072ee90021d85a5854e74e4292e173e6dc04b0144492f6c67bafeab3_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 103,
            ""tags"": ""flowers, bees, garden"",
            ""downloads"": 5016,
            ""user"": ""Clker-Free-Vector-Images"",
            ""favorites"": 82,
            ""imageSize"": 473294,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2012/04/01/00-18-38-212_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2012/05/02/17/39/flowers-45786_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/ea35b9072af71c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_1280.png"",
            ""webformatHeight"": 529,
            ""webformatWidth"": 640,
            ""likes"": 24,
            ""imageWidth"": 1920,
            ""id"": 308823,
            ""user_id"": 3736,
            ""views"": 28356,
            ""comments"": 1,
            ""pageURL"": ""https://pixabay.com/vectors/flower-hibiscus-pink-yellow-blue-308823/"",
            ""imageHeight"": 1587,
            ""webformatURL"": ""https://pixabay.com/get/ea35b9072af71c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 123,
            ""tags"": ""flower, hibiscus, pink"",
            ""downloads"": 2823,
            ""user"": ""Clker-Free-Vector-Images"",
            ""favorites"": 50,
            ""imageSize"": 180116,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2012/04/01/00-18-38-212_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2014/04/03/00/36/flower-308823_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/eb30b0062ff3053ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 640,
            ""likes"": 41,
            ""imageWidth"": 1920,
            ""id"": 2519774,
            ""user_id"": 4860762,
            ""views"": 4123,
            ""comments"": 3,
            ""pageURL"": ""https://pixabay.com/vectors/bee-flower-mascot-fun-funny-cute-2519774/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/eb30b0062ff3053ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 150,
            ""tags"": ""bee, flower, mascot"",
            ""downloads"": 1501,
            ""user"": ""jambulboy"",
            ""favorites"": 53,
            ""imageSize"": 263306,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2019/03/02/02-20-34-483_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2017/07/19/17/16/bee-2519774_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e830b3092ff0023ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 640,
            ""likes"": 19,
            ""imageWidth"": 1920,
            ""id"": 1526743,
            ""user_id"": 1496775,
            ""views"": 5663,
            ""comments"": 3,
            ""pageURL"": ""https://pixabay.com/illustrations/flower-yellow-garden-nature-plants-1526743/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/e830b3092ff0023ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/ai"",
            ""previewHeight"": 150,
            ""tags"": ""flower, yellow, garden"",
            ""downloads"": 1006,
            ""user"": ""starchim01"",
            ""favorites"": 32,
            ""imageSize"": 623603,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2016/01/12/10-17-37-643_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2016/07/18/20/42/flower-1526743_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e831b70e2ffc1c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_1280.png"",
            ""webformatHeight"": 502,
            ""webformatWidth"": 640,
            ""likes"": 15,
            ""imageWidth"": 1920,
            ""id"": 146178,
            ""user_id"": 30363,
            ""views"": 1839,
            ""comments"": 2,
            ""pageURL"": ""https://pixabay.com/vectors/flower-yellow-flourish-blossom-146178/"",
            ""imageHeight"": 1506,
            ""webformatURL"": ""https://pixabay.com/get/e831b70e2ffc1c22d2524518b74d4492e775e6d110ac104490f7c77ea5e5b2be_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 117,
            ""tags"": ""flower, yellow, flourish"",
            ""downloads"": 636,
            ""user"": ""OpenClipart-Vectors"",
            ""favorites"": 25,
            ""imageSize"": 993882,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2013/08/21/16-02-05-675_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2013/07/12/12/45/flower-146178_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/eb31b5072bf1003ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 640,
            ""webformatWidth"": 510,
            ""likes"": 28,
            ""imageWidth"": 1532,
            ""id"": 2448351,
            ""user_id"": 2157612,
            ""views"": 8749,
            ""comments"": 2,
            ""pageURL"": ""https://pixabay.com/vectors/sunflowers-vector-flower-summer-2448351/"",
            ""imageHeight"": 1920,
            ""webformatURL"": ""https://pixabay.com/get/eb31b5072bf1003ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 150,
            ""tags"": ""sunflowers, vector, flower"",
            ""downloads"": 1442,
            ""user"": ""dawnydawny"",
            ""favorites"": 26,
            ""imageSize"": 610204,
            ""previewWidth"": 119,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2017/06/07/19-28-20-230_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2017/06/27/18/22/sunflowers-2448351_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/eb35b90828f3023ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 360,
            ""webformatWidth"": 640,
            ""likes"": 83,
            ""imageWidth"": 1920,
            ""id"": 2087073,
            ""user_id"": 2006397,
            ""views"": 15606,
            ""comments"": 5,
            ""pageURL"": ""https://pixabay.com/vectors/cross-yellow-christian-church-2087073/"",
            ""imageHeight"": 1080,
            ""webformatURL"": ""https://pixabay.com/get/eb35b90828f3023ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 84,
            ""tags"": ""cross, yellow, christian"",
            ""downloads"": 4762,
            ""user"": ""slightly_different"",
            ""favorites"": 72,
            ""imageSize"": 121164,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2018/02/24/18-41-47-124_250x250.jpg"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2017/02/21/18/47/cross-2087073_150.png""
        },
        {
            ""largeImageURL"": ""https://pixabay.com/get/e83db2082bfd043ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_1280.png"",
            ""webformatHeight"": 570,
            ""webformatWidth"": 640,
            ""likes"": 19,
            ""imageWidth"": 1920,
            ""id"": 1837395,
            ""user_id"": 162579,
            ""views"": 1505,
            ""comments"": 0,
            ""pageURL"": ""https://pixabay.com/vectors/flower-floral-botanical-plant-1837395/"",
            ""imageHeight"": 1710,
            ""webformatURL"": ""https://pixabay.com/get/e83db2082bfd043ed1584d05fb1d4794e773e0d11db80c4090f5c67fa0efbdbfde_640.png"",
            ""type"": ""vector/svg"",
            ""previewHeight"": 133,
            ""tags"": ""flower, floral, botanical"",
            ""downloads"": 611,
            ""user"": ""Prawny"",
            ""favorites"": 28,
            ""imageSize"": 354414,
            ""previewWidth"": 150,
            ""userImageURL"": ""https://cdn.pixabay.com/user/2014/02/17/17-51-05-748_250x250.png"",
            ""previewURL"": ""https://cdn.pixabay.com/photo/2016/11/18/23/56/flower-1837395_150.png""
        }
    ],
    ""total"": 179
}";
    }
}
