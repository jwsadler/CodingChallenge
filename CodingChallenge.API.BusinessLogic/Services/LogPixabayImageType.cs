using System.Collections.Generic;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Models;

namespace CodingChallenge.API.BusinessLogic.Services
{
    public class ValidateSearchTerms : IValidationService
    {
        public void Validate(CodingChallengeRequestModel model, out List<string> messages, out bool hardStop)
        {
            hardStop = false;

            if (string.IsNullOrEmpty(model.Query))
            {
                hardStop = true;
                messages = new List<string> { $"Failure: Query string cannot be empty" };
                return;
            }

            messages = new List<string> {$"Info: Query string {model.Query} is valid"};
        }
    }
}