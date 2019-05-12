using System.Collections.Generic;
using System.Linq;
using System.Net;
using CodingChallenge.API.BusinessLogic.Enums;
using CodingChallenge.API.BusinessLogic.Json;
using CodingChallenge.API.Common.Interfaces;
using Newtonsoft.Json;

namespace CodingChallenge.API.BusinessLogic.Models.PixaBay
{
    public class PixabayResponseModel: IStatus
    {
        [JsonProperty(PropertyName = "totalHits")]
        public int TotalHits { get; set; }

        [JsonProperty(PropertyName = "hits")] public List<PixabayImageModel> Hits { get; set; }

        public int MaxRequested { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class PixabayImageModel
    {
        [JsonProperty(PropertyName = "webformatHeight")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "webformatWidth")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "likes")] public int Likes { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(PixabayStringEnumConverter))]
        public PixabayImageTypes Type { get; set; }

        [JsonProperty(PropertyName = "tags")] public string Tags { get; set; }

        [JsonProperty(PropertyName = "webformatURL")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "tagList")]
        public List<string> TagList => Tags.Split(',').ToList();
    }
}