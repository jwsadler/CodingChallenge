using System.Linq;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Services;
using FluentAssertions;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Validation
{
    public class LocationTests
    {
        [Theory]
        [InlineData("J3222", "11111")]
        [InlineData("", "11111")]
        [InlineData("J3222", "")]
        [InlineData(null, "11111")]
        [InlineData("J3222", null)]
        public void Pass_Test(string locationId, string postalCode)
        {
            var service = new ValidateLocation();
            var model = new ChatbotRequestModel {LocationId = locationId, PostalCode = postalCode};

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(ValidateLocation.INFO_LOCATION_VALIDATIONS_PASSED);
            hardStop.Should().BeFalse();
        }

        [Fact]
        public void Failure_Test()
        {
            var service = new ValidateLocation();
            var model = new ChatbotRequestModel();

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(ValidateLocation.INFO_LOCATION_VALIDATIONS_FAILURE);
            hardStop.Should().BeTrue();
        }
    }
}