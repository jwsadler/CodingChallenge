using CodingChallenge.API.Common.Attributes;
using CodingChallenge.API.Common.Helpers;

namespace CodingChallenge.API.BusinessLogic.Enums
{
    public enum PixabayImageTypes
    {
        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "all")]
        All,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "photo")]
        Photo,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "illustration")]
        Illustration,

        [Metadata(CCAConstants.API_VALUE_ATTRIBUTE, "vector")]
        Vector
    }
}