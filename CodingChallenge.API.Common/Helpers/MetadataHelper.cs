using System.Collections.Generic;
using System.Linq;
using CodingChallenge.API.Common.Attributes;

namespace CodingChallenge.API.Common.Helpers
{
    public static class MetadataHelper
    {
        public static string GetFirstValueFromMetaDataAttribute<T>(this T value, string metaDataDescription)
        {
            return GetValueFromMetaDataAttribute(value, metaDataDescription).FirstOrDefault();
        }

        private static IEnumerable<string> GetValueFromMetaDataAttribute<T>(T value, string metaDataDescription)
        {
            var attributes =
                value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(MetadataAttribute), true);
            return attributes.Any()
                ? (from p in (MetadataAttribute[]) attributes
                    where p.Description.ToLower() == metaDataDescription.ToLower()
                    select p.MetaData).ToList()
                : new List<string>();
        }

        public static List<T> GetEnumeratesByMetaData<T>(string metadataDescription, string value)
        {
            return
                typeof(T).GetEnumValues().Cast<T>().Where(
                    enumerate =>
                        GetValueFromMetaDataAttribute(enumerate, metadataDescription).Any(
                            p => p.ToLower() == value.ToLower())).ToList();
        }

        public static List<T> GetNotEnumeratesByMetaData<T>(string metadataDescription, string value)
        {
            return
                typeof(T).GetEnumValues().Cast<T>().Where(
                    enumerate =>
                        GetValueFromMetaDataAttribute(enumerate, metadataDescription).All(
                            p => p.ToLower() != value.ToLower())).ToList();
        }


        public static bool ContainsMetaData<T>(this T value, string metadataDescription, string metaDataValue = null)
        {
            var attributes =
                value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(MetadataAttribute), true);

            return string.IsNullOrEmpty(metaDataValue)
                ? (from p in (MetadataAttribute[]) attributes
                    where p.Description.ToLower() == metadataDescription.ToLower()
                    select p.MetaData).Any()
                : (from p in (MetadataAttribute[]) attributes
                    where p.Description.ToLower() == metadataDescription.ToLower()
                          && p.MetaData.ToLower() == metaDataValue.ToLower()
                    select p.MetaData).Any();
        }
    }
}