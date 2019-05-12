using System.Linq;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Services;
using FluentAssertions;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Validation
{
    public class SearchTermsTests
    {
        [Fact]
        public void Pass_Test()
        {
            var service = new ValidateSearchTerms();
            ;
            var model = new CodingChallengeRequestModel {Query = "red car"};

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be($"Info: Query string {model.Query} is valid");
            hardStop.Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Fail_Test(string value)
        {
            var service = new ValidateSearchTerms();
            ;
            var model = new CodingChallengeRequestModel { Query = value };

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be($"Failure: Query string cannot be empty");
            hardStop.Should().BeTrue();
        }

    }
}