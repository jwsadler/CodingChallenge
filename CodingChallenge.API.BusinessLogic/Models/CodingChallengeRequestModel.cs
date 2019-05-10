using CodingChallenge.API.BusinessLogic.Enums;
using CodingChallenge.API.BusinessLogic.Json;
using Newtonsoft.Json;

namespace CodingChallenge.API.BusinessLogic.Models
{
    public class CodingChallengeRequestModel
    {
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(PixabayStringEnumConverter))]
        public PixabayImageTypes Type { get; set; }

        [JsonProperty(PropertyName = "category")]
        [JsonConverter(typeof(PixabayStringEnumConverter))]
        public PixabayCategoryTypes Category { get; set; }

        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }
    }
}