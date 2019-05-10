using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CodingChallenge.API.Common.Json
{
    public class TolerantStringEnumConverter : StringEnumConverter
    {
        public TolerantStringEnumConverter()
        {
        }

        public TolerantStringEnumConverter(bool camelCaseText)
            : base(camelCaseText)
        {
        }


        public override bool CanConvert(Type objectType)
        {
            var type = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
            return type != null && type.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var isNullable = IsNullableType(objectType);
            var enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            var names = Enum.GetNames(enumType);

            try
            {
                var result = base.ReadJson(reader, objectType, existingValue, serializer);
                if (result != null)
                {
                    return result;
                }
            }
            catch(Exception)
            {
                //this is catch the StringEnumConvertors thrown error
            }

            if (!isNullable)
            {
                var defaultName = names
                                      .FirstOrDefault(n =>
                                          string.Equals(n, "Unknown", StringComparison.OrdinalIgnoreCase) ||
                                          string.Equals(n, "Notset", StringComparison.OrdinalIgnoreCase)) ?? names.First();

                return Enum.Parse(enumType, defaultName);
            }

            return null;
        }

        private bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}