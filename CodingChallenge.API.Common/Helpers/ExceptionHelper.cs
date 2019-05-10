using System;

namespace CodingChallenge.API.Common.Helpers
{
    public static class ExceptionHelper
    {
        public static Exception GetInnerMostException(this Exception e)
        {
            while (e.InnerException != null) e = e.InnerException;

            return e;
        }
    }
}