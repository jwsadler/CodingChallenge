using System.Collections.Generic;
using System.Linq;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Services;
using FluentAssertions;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Profiles
{
    public class StaticMappingTests
    {
        public StaticMappingTests()
        {
            _mappingServices = new StaticMappingServices();
        }

        private readonly IStaticMappingServices _mappingServices;

        [Fact]
        public void Test_AllProperty_Mapping()
        {
            var model = new LMSFormModel {FirstName = "Test", LastName = "Tester"};

            var result = _mappingServices.MapLMSFormModel(model);

            result.Count.Should().Be(2);
            result.FirstOrDefault(p => p.Key == "FirstName").Should()
                .Be(new KeyValuePair<string, string>("FirstName", "Test"));
            result.FirstOrDefault(p => p.Key == "LastName").Should()
                .Be(new KeyValuePair<string, string>("LastName", "Tester"));
        }
    }
}