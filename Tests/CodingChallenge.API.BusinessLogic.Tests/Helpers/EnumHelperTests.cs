using CodingChallenge.API.Common.Attributes;
using CodingChallenge.API.Common.Helpers;
using FluentAssertions;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Helpers
{
    public class EnumHelperTests
    {
        [Fact]
        public void Test_GetEnumeratesByMetaData()
        {
            var enumerates =
                MetadataHelper.GetEnumeratesByMetaData<TestingEnum>("Value",
                    "1");

            enumerates.Should().BeEquivalentTo(new[]
            {
                TestingEnum.Value1, TestingEnum.Value2
            });
        }

        [Fact]
        public void Test_GetNotEnumeratesByMetaData()
        {
            var enumerates =
                MetadataHelper.GetNotEnumeratesByMetaData<TestingEnum>("Value",
                    "1");

            enumerates.Should()
                .BeEquivalentTo(new[]
                {
                    TestingEnum.Value3
                });
        }

        [Theory]
        [InlineData(TestingEnum.Value1, "Value", "1")]
        [InlineData(TestingEnum.Value2, "Value", "1")]
        [InlineData(TestingEnum.Value3, "Value", "3")]
        [InlineData(TestingEnum.Value1, "Flag", "")]
        [InlineData(TestingEnum.Value2, "Flag", null)]
        [InlineData(TestingEnum.Value3, "Test", null)]
        [InlineData(TestingEnum.Value3, "1", null)]
        public void Test_GetValueFromMetaDataAttribute(TestingEnum testingEnum, string tag, string value)
        {
            var result = testingEnum.GetFirstValueFromMetaDataAttribute(tag);
            result.Should().Be(value);
        }

    }

    public enum TestingEnum
    {
        [Metadata("Value", "1")] [Metadata("Flag")] Value1,
        [Metadata("Value", "1")] Value2,
        [Metadata("Value", "3")] Value3
    }
}
