using System;
using CodingChallenge.API.BusinessLogic.Enums;
using CodingChallenge.API.Common.Helpers;
using CodingChallenge.API.Common.Json;
using Newtonsoft.Json;

namespace CodingChallenge.API.BusinessLogic.Json
{
    public class PixabayStringEnumConverter : TolerantStringEnumConverter
    {
        private const string VECTOR = "vector/";

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PixabayCategoryTypes) || objectType == typeof(PixabayImageTypes);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var metadataValue = value.GetFirstValueFromMetaDataAttribute(CodingChallengeConstants.API_VALUE_ATTRIBUTE);

            writer.WriteValue(!string.IsNullOrEmpty(metadataValue) ? metadataValue : ((string) value).ToLower());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (objectType == typeof(PixabayImageTypes) && ((string) reader.Value).StartsWith(VECTOR)) return PixabayImageTypes.Vector;
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}