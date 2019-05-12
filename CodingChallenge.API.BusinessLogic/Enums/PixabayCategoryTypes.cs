using CodingChallenge.API.Common.Attributes;
using CodingChallenge.API.Common.Helpers;

namespace CodingChallenge.API.BusinessLogic.Enums
{
    public enum PixabayCategoryTypes
    {
        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "all")]
        All,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "fashion")]
        Fashion,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "nature")]
        Nature,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "backgrounds")]
        Backgrounds,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "science")]
        Science,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "education")]
        Education,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "people")]
        People,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "feelings")]
        Feelings,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "religion")]
        Religion,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "health")]
        Health,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "places")]
        Places,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "animals")]
        Animals,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "industry")]
        Industry,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "food")]
        Food,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "computer")]
        Computer,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "sports")]
        Sports,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "transportation")]
        Transportation,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "travel")]
        Travel,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "buildings")]
        Buildings,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "business")]
        Business,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "music")]
        Music
    }
}