using System.Configuration;
using CodingChallenge.API.BusinessLogic.CustomSection;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.Common.Helpers;

namespace CodingChallenge.API.BusinessLogic.Helpers
{
    public class APIConfigurationHelper : IAPIConfigurationHelper
    {
        public APIConfigurationSection APIConfiguration =>
            (APIConfigurationSection) ConfigurationManager.GetSection(CCAConstants.Configuration.CONFIGURATION_ROOT);
    }
}