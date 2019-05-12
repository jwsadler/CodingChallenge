using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CodingChallenge.API.Common.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.Helpers
{
    public class TolerantEnumConverterTests
    {
        //http://stackoverflow.com/questions/22752075/how-can-i-ignore-unknown-enum-values-during-json-deserialization

        public class EnumClass
        {
            public CustomerStatus CustomerStatus { get; set; }

            [Theory]
            [InlineData(CustomerStatus.Active, "{\r\n  \"CustomerStatus\": \"Active\"\r\n}")]
            [InlineData(CustomerStatus.InActive, "{\r\n  \"CustomerStatus\": \"InActive\"\r\n}")]
            [InlineData(CustomerStatus.Active, "{\r\n  \"CustomerStatus\": 0\r\n}")]
            [InlineData(CustomerStatus.Active, "{\r\n  \"CustomerStatus\": \"Closed\"\r\n}")]
            [InlineData(CustomerStatus.InActive, "{\r\n  \"CustomerStatus\": 1\r\n}")]
            [InlineData(CustomerStatus.Active, "{\r\n  \"CustomerStatus\": null\r\n}")]
            public void DeserializeEnumClass(CustomerStatus enum1, string json)
            {
                var enumClass = JsonConvert.DeserializeObject<EnumClass>(json);

                enumClass.CustomerStatus.Should().Be(enum1);
            }
        }

        [JsonConverter(typeof(TolerantStringEnumConverter))]
        public enum CustomerStatus
        {
            [EnumMember(Value = "active")]
            Active,
            [EnumMember(Value = "inactive")]
            InActive
        }
    }
}
