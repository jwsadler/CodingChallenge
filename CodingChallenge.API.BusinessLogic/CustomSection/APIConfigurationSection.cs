using System.Configuration;
using CodingChallenge.API.BusinessLogic.Enums;
using CodingChallenge.API.Common.Helpers;

namespace CodingChallenge.API.BusinessLogic.CustomSection
{
    public class APIConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.PIXABAY_PROPERTY_NAME)]
        public PixabayElement PixabayAPI
        {
            get => (PixabayElement) this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.OXFORD_PROPERTY_NAME)]
        public OxfordDictionaryElement OxfordDictionaryAPI
        {
            get => (OxfordDictionaryElement) this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.API_LOGGIN_NAME)]
        public APILoggingElement APILogging
        {
            get => (APILoggingElement) this[CCAConstants.Configuration.ConfigurationNodes.API_LOGGIN_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.API_LOGGIN_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.ENABLE_TEST_API_NAME, DefaultValue = "false", IsRequired = true)]
        public bool EnableTestApi
        {
            get => (bool) this[CCAConstants.Configuration.ConfigurationNodes.ENABLE_TEST_API_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.ENABLE_TEST_API_NAME] = value;
        }
    }

    public class APILoggingElement : ConfigurationElement
    {
        [ConfigurationProperty("verboseLogging", DefaultValue = "false", IsRequired = false)]
        public bool VerboseLogging
        {
            get => (bool) this[CCAConstants.Configuration.ConfigurationNodes.VERBOSE_LOGGING_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.VERBOSE_LOGGING_PROPERTY_NAME] = value;
        }
    }

    public class PixabayElement : ConfigurationElement
    {
        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.PIXABAY_API_KEY_PROPERTY_NAME, IsRequired = true)]
        public string APIKey
        {
            get => (string)this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_API_KEY_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_API_KEY_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.PIXABAY_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME,
            DefaultValue = "https://pixabay.com/api/", IsRequired = true)]
        public string BaseUrl
        {
            get => (string) this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME] = value;
        }


        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.PIXABAY_URL_FORMAT_PROPERTY_NAME,
            DefaultValue = "?key={key}&q={query}&image_type={type}&category={category}&orientation=horizontal", IsRequired = true)]
        public string UrlFormat
        {
            get => (string) this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_URL_FORMAT_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.PIXABAY_URL_FORMAT_PROPERTY_NAME] = value;
        }
        
        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.CATEGORY_TYPE_PROPERTY_NAME,
            DefaultValue = PixabayCategoryTypes.Business, IsRequired = true)]
        public PixabayCategoryTypes DefaultCategoryType
        {
            get => (PixabayCategoryTypes) this[CCAConstants.Configuration.ConfigurationNodes.CATEGORY_TYPE_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.CATEGORY_TYPE_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.IMAGE_TYPE_PROPERTY_NAME,
            DefaultValue = PixabayImageTypes.All, IsRequired = true)]
        public PixabayImageTypes DefaultImageType
        {
            get => (PixabayImageTypes)this[CCAConstants.Configuration.ConfigurationNodes.IMAGE_TYPE_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.IMAGE_TYPE_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.MAX_IMAGES_PROPERTY_NAME,
            DefaultValue = 8, IsRequired = true)]
        public int MaxNumberOfImages
        {
            get => (int)this[CCAConstants.Configuration.ConfigurationNodes.MAX_IMAGES_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.MAX_IMAGES_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.SAFE_SEARCH_PROPERTY_NAME,
            DefaultValue = true, IsRequired = true)]
        public bool SafeSearch
        {
            get => (bool)this[CCAConstants.Configuration.ConfigurationNodes.SAFE_SEARCH_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.SAFE_SEARCH_PROPERTY_NAME] = value;
        }
    }

    public class OxfordDictionaryElement : ConfigurationElement
    {
        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.OXFORD_API_KEY_PROPERTY_NAME, IsRequired = true)]
        public string APIKey
        {
            get => (string)this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_API_KEY_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_API_KEY_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.OXFORD_APPID_PROPERTY_NAME, IsRequired = true)]
        public string AppId
        {
            get => (string)this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_APPID_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_APPID_PROPERTY_NAME] = value;
        }

        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.OXFORD_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME,
            DefaultValue = "https://od-api.oxforddictionaries.com:443/api/v2/entries/en-gb/", IsRequired = true)]
        public string BaseUrl
        {
            get => (string)this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_ENDPOINT_BASE_ADDRESS_PROPERTY_NAME] = value;
        }


        [ConfigurationProperty(CCAConstants.Configuration.ConfigurationNodes.OXFORD_URL_FORMAT_PROPERTY_NAME,
            DefaultValue = "{query}?strictMatch=false", IsRequired = true)]
        public string UrlFormat
        {
            get => (string)this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_URL_FORMAT_PROPERTY_NAME];
            set => this[CCAConstants.Configuration.ConfigurationNodes.OXFORD_URL_FORMAT_PROPERTY_NAME] = value;
        }
    }
}