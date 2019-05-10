using System;
using System.Linq.Expressions;

namespace CodingChallenge.API.Common.Helpers
{
    public static class PropertyNameHelper
    {
        private const string YOU_MUST_PASS_A_LAMBDA_OF_THE_FORM_CLASS_PROPERTY_OR_OBJECT_PROPERTY =
            "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'";

        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            if (!(propertyLambda.Body is MemberExpression me))
                throw new ArgumentException(YOU_MUST_PASS_A_LAMBDA_OF_THE_FORM_CLASS_PROPERTY_OR_OBJECT_PROPERTY);

            return me.Member.Name;
        }

        public static string GetPropertyName<T, TReturn>(Expression<Func<T, TReturn>> expression)
        {
            var body = (MemberExpression) expression.Body;
            return body.Member.Name;
        }
    }
}