namespace CodingChallenge.API.Common.Helpers
{
    public static class CodingChallengeConstants
    {
        public const string APPLICATION_JSON = "application/json";

        public const string API_VALUE_ATTRIBUTE = "Api Value";

        public static class Configuration
        {
            public const string CONFIGURATION_ROOT = "CodeingChallengeConfiguration";

            public static class ConfigurationNodes
            {
                public const string PIXABAY_PROPERTY_NAME = "PixabayAPI";
                public const string OXFORD_PROPERTY_NAME = "OxfordDictionaryAPI";
                public const string API_LOGGIN_NAME = "APILogging";
                public const string VERBOSE_LOGGING_PROPERTY_NAME = "verboseLogging";
                public const string ENABLE_TEST_API_NAME = "enableTestApi";
                public const string PIXABAY_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME = "baseUrl";
                public const string PIXABAY_URL_FORMAT_PROPERTY_NAME = "urlFormat";
                public const string PIXABAY_API_KEY_PROPERTY_NAME = "apiKey";
                public const string CATEGORY_TYPE_PROPERTY_NAME = "defaultCategoryType";
                public const string IMAGE_TYPE_PROPERTY_NAME = "defaultImageType";
                public const string MAX_IMAGES_PROPERTY_NAME = "maxNumberOfImages";
                public const string SAFE_SEARCH_PROPERTY_NAME = "safeSearch";


                public const string OXFORD_API_KEY_PROPERTY_NAME = "apiKey";
                public const string OXFORD_APPID_PROPERTY_NAME = "appId";
                public const string OXFORD_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME = "baseUrl";
                public const string OXFORD_URL_FORMAT_PROPERTY_NAME = "urlFormat";


            }
        }
    }
}