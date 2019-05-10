using System.Linq;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Services;
using FluentAssertions;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Validation
{
    public class RequiredTests
    {
        [Theory]
        [InlineData("test@test.com", "11111","www.carpetone.com")]
        public void Pass_Test(string emailAddress, string postalCode, string leadSourceUrl)
        {
            var service = new ValidateRequired();
            var model = new ChatbotRequestModel
            {
                EmailAddress = emailAddress,
                PostalCode = postalCode,
                LeadSourceUrl = leadSourceUrl
            };

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(ValidateRequired.INFO_REQUIRED_FIELD_VALIDATIONS_PASSED);
            hardStop.Should().BeFalse();
        }

        [Theory]
        [InlineData("", "11111", "www.carpetone.com", ValidateRequired.FAILURE_EMAIL_ADDRESS_IS_REQUIRED)]
        [InlineData(null, "11111", "www.carpetone.com", ValidateRequired.FAILURE_EMAIL_ADDRESS_IS_REQUIRED)]
        [InlineData("test@test.com", "", "www.carpetone.com", ValidateRequired.FAILURE_POSTAL_CODE_ZIP_CODE_IS_REQUIRED)]
        [InlineData("test@test.com", null,"www.carpetone.com", ValidateRequired.FAILURE_POSTAL_CODE_ZIP_CODE_IS_REQUIRED)]
        [InlineData("test@test.com", "11111", "", ValidateRequired.FAILURE_LEAD_SOURCE_URL_IS_REQUIRED)]
        [InlineData("test@test.com", "11111", null, ValidateRequired.FAILURE_LEAD_SOURCE_URL_IS_REQUIRED)]

        public void Failure_Tests(string emailAddress, string postalCode, string leadSourceUrl, string message)
        {
            var service = new ValidateRequired();
            var model = new ChatbotRequestModel
            {
                EmailAddress = emailAddress,
                PostalCode = postalCode,
                LeadSourceUrl = leadSourceUrl
            };

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(message);
            hardStop.Should().BeTrue();
        }


        [Theory]
        [InlineData("", "","")]
        [InlineData(null, null,null)]
        public void Multiple_Failure_Tests(string emailAddress, string postalCode, string leadSourceUrl)
        {
            var service = new ValidateRequired();
            var model = new ChatbotRequestModel
            {
                EmailAddress = emailAddress,
                PostalCode = postalCode,
                LeadSourceUrl = leadSourceUrl
            };

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(3);
            hardStop.Should().BeTrue();
        }
    }
}