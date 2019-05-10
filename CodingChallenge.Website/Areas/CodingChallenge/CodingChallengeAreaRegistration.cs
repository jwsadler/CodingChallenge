using System.Web.Mvc;

namespace CodingChallenge.Website.Areas.CodingChallenge
{
    public class CodingChallengeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CodingChallenge";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CodingChallenge_default",
                "CodingChallenge/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}