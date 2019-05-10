using System.Linq;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.Enums;
using CodingChallenge.API.BusinessLogic.Services;
using FluentAssertions;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Validation
{
    public class LeadSourceTests
    {
        [Theory]
        [InlineData("www.carpetone.com", LeadSourceBrandTypes.US,ValidateSimpleLeadSourceBrand.INFO_SIMPLE_LEADSOURCEBRAND_VALIDATION_PASSED)]
        [InlineData("wwww.carpetone.ca", LeadSourceBrandTypes.CA, ValidateSimpleLeadSourceBrand.INFO_SIMPLE_LEADSOURCEBRAND_VALIDATION_PASSED)]
        [InlineData("www.carpetone.com", LeadSourceBrandTypes.CA, "Warning: Switching LeadsourceBrand to US because orginzation is .com")]
        [InlineData("wwww.carpetone.ca", LeadSourceBrandTypes.US, "Warning: Switching LeadsourceBrand to CA because orginzation is .ca")]
        public void Pass_Test(string url, LeadSourceBrandTypes leadSourceBrand,string message)
        {
            var service = new ValidateSimpleLeadSourceBrand();
            var model = new ChatbotRequestModel {LeadSourceUrl = url, LeadSourceBrand = leadSourceBrand };

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(message);
            hardStop.Should().BeFalse();
        }

        [Fact]
        public void No_Lead_Source()
        {
            var service = new ValidateSimpleLeadSourceBrand();
            var model = new ChatbotRequestModel();

            service.Validate(model, out var messages, out var hardStop);

            messages.Count.Should().Be(1);
            messages.First().Should().Be(ValidateSimpleLeadSourceBrand.INFO_SIMPLE_LEADSOURCEBRAND_VALIDATION_NOT_RUN);
            hardStop.Should().BeFalse();
        }
    }
}