using CodingChallenge.API.Common.Attributes;
using CodingChallenge.API.Common.Helpers;

namespace CodingChallenge.API.BusinessLogic.Enums
{
    public enum PixabayCategoryTypes
    {
        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "all")]
        All,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "fashion")]
        Fashion,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "nature")]
        Nature,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "backgrounds")]
        Backgrounds,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "science")]
        Science,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "education")]
        Education,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "people")]
        People,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "feelings")]
        Feelings,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "religion")]
        Religion,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "health")]
        Health,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "places")]
        Places,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "animals")]
        Animals,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "industry")]
        Industry,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "food")]
        Food,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "computer")]
        Computer,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "sports")]
        Sports,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "transportation")]
        Transportation,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "travel")]
        Travel,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "buildings")]
        Buildings,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "business")]
        Business,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "music")]
        Music
    }
}