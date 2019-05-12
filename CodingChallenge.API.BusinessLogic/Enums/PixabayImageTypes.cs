using CodingChallenge.API.Common.Attributes;
using CodingChallenge.API.Common.Helpers;

namespace CodingChallenge.API.BusinessLogic.Enums
{
    public enum PixabayImageTypes
    {
        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "all")]
        All,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "photo")]
        Photo,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "illustration")]
        Illustration,

        [Metadata(CodingChallengeConstants.API_VALUE_ATTRIBUTE, "vector")]
        Vector
    }
}