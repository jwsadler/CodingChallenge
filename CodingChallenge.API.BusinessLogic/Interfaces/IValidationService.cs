using System.Collections.Generic;
using CodingChallenge.API.BusinessLogic.Models;

namespace CodingChallenge.API.BusinessLogic.Interfaces
{
    public interface IValidationService
    {
        void Validate(CodingChallengeRequestModel model, out List<string> messages,out bool hardStop);
    }
}