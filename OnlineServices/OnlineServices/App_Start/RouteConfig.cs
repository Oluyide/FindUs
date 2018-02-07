using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineServices
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute("GetStatesByCountryId",
                            "ProfileSetup/getStatesByCountryId/",
                            new { controller = "ProfileSetup", action = "GetStatesByCountryId" },
                            new[] { "OnlineServices.Controllers" });

            routes.MapRoute("GetLagBystateId",
                          "ProfileSetup/getLagBystateId/",
                          new { controller = "ProfileSetup", action = "GetLagBystateId" },
                         new[] { "OnlineServices.Controllers" });

            routes.MapRoute("ApproveUser",
                        "quotation/approveUser/",
                        new { controller = "Admin", action = "ApproveUser" },
                        new[] { "OnlineServices.Controllers" });

            routes.MapRoute("ProfilePage",
                    "profilesetup/profilepage/",
                    new { controller = "ProfileSetup", action = "ProfilePage" },
                    new[] { "OnlineServices.Controllers" });

            routes.MapRoute("LoadPost",
                   "profilesetup/loadpost/",
                   new { controller = "ProfileSetup", action = "ProfilePage" },
                   new[] { "OnlineServices.Controllers" });

        }
    }
}
