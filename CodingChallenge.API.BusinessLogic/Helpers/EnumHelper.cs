using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CodingChallenge.API.BusinessLogic.Helpers
{
    public static class EnumHelper
    {
        public static T SetEnumFromValue<T>(string value)
        {
            if (!typeof(T).IsEnum) return default(T);
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public static string GetFirstValue<T>(this T value) => GetValue(value).FirstOrDefault();

        public static List<string> GetValue<T>(this T value)
        {
            var attribs =
                value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(EnumMemberAttribute), true);
            return attribs.Any()
                ? (from p in (EnumMemberAttribute[])attribs
                   select p.Value).ToList()
                : new List<string>();
        }

        
    }
}