using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodingChallenge.API.Common.Json
{
    public class NullToDefaultValueResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                .Select(p =>
                {
                    var jp = CreateProperty(p, memberSerialization);
                    jp.ValueProvider = new NullToDefaultValueValueProvider(p);
                    return jp;
                }).ToList();
        }
    }

    public class NullToDefaultValueValueProvider : IValueProvider
    {
        private readonly PropertyInfo _memberInfo;

        public NullToDefaultValueValueProvider(PropertyInfo memberInfo)
        {
            _memberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            var result = _memberInfo.GetValue(target);

            var attribute = _memberInfo.GetCustomAttribute<JsonPropertyAttribute>();

            //Allow default behavior to happen if the required attribute is set to DisallowNull or Always
            if (attribute == null || attribute.Required == Required.Default)
            {
                if (_memberInfo.PropertyType == typeof(string) && result == null)
                    result = "";
                //If enum get default value
                else if (_memberInfo.PropertyType.IsAssignableFrom(typeof(Enum)) && result == null)
                    result = Enum.ToObject(_memberInfo.PropertyType, 0);
            }

            return result;
        }

        public void SetValue(object target, object value)
        {
            _memberInfo.SetValue(target, value);
        }
    }
}