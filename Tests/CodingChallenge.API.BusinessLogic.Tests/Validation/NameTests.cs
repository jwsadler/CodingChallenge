using System.Linq;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Services;
using FluentAssertions;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Validation
{
    public class NameTests
    {
        [Theory]
        [InlineData("firstname", "lastname")]
        public void Pass_Test(string firstName, string lastName)
        {
            var service = new ValidateNames();
            var model = new ChatbotRequestModel {FirstName = firstName, LastName = lastName};

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(ValidateNames.INFO_NAMES_VALIDATIONS_PASSED);
            hardStop.Should().BeFalse();
        }

        [Theory]
        [InlineData("","LastName",ValidateNames.FAILURE_FIRST_NAME_CANNOT_BE_NULL_OR_EMPTY)]
        [InlineData(null, "LastName", ValidateNames.FAILURE_FIRST_NAME_CANNOT_BE_NULL_OR_EMPTY)]
        [InlineData("FirstName", "", ValidateNames.FAILURE_LAST_NAME_CANNOT_BE_NULL_OR_EMPTY)]
        [InlineData("FirstName", null, ValidateNames.FAILURE_LAST_NAME_CANNOT_BE_NULL_OR_EMPTY)]
        [InlineData("First1Name", "LastName", ValidateNames.FAILURE_FIRST_NAME_CANNOT_CONTAIN_NUMBERS)]
        [InlineData("FirstName", "Last1Name", ValidateNames.FAILURE_LAST_NAME_CANNOT_CONTAIN_NUMBERS)]
        [InlineData("LastName", "LastName", ValidateNames.FAILURE_FIRST_AND_LAST_NAMES_CANNOT_BE_THE_SAME)]
        public void Failure_Tests(string firstName, string lastName, string message)
        {
            var service = new ValidateNames();
            var model = new ChatbotRequestModel{FirstName = firstName,LastName = lastName};

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(message);
            hardStop.Should().BeTrue();
        }


        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("First1Name", "Last1Name")]
        public void Multiple_Failure_Tests(string firstName, string lastName)
        {
            var service = new ValidateNames();
            var model = new ChatbotRequestModel { FirstName = firstName, LastName = lastName };

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(2);
            hardStop.Should().BeTrue();
        }
    }
}