using System.Collections.Generic;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Models;

namespace CodingChallenge.API.BusinessLogic.Services
{
    public class LogPixabayImageType : IValidationService
    {
        public void Validate(CodingChallengeRequestModel model, out List<string> messages, out bool hardStop)
        {
            hardStop = false;
            messages = new List<string> {$"Info: Image Type Set to {model.Type.ToString().ToLower()}"};
        }
    }
}