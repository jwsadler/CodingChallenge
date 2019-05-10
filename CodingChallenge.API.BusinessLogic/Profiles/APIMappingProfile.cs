using AutoMapper;
using CodingChallenge.API.Common.Extensions;

namespace CodingChallenge.API.BusinessLogic.Profiles
{
    public class APIMappingProfile : Profile
    {
        protected override void Configure()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            this.IgnoreUnmapped();
        }
    }
}