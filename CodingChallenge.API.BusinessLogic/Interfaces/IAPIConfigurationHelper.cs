using CodingChallenge.API.BusinessLogic.CustomSection;
using CodingChallenge.API.Common.Interfaces;

namespace CodingChallenge.API.BusinessLogic.Interfaces
{
    public interface IAPIConfigurationHelper : IHelper
    {
        APIConfigurationSection APIConfiguration { get; }
    }
}