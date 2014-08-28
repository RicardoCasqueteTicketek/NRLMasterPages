using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NRL.MasterPages.Resources
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
              name: "Content",
              url: "{action}/{name}",
              defaults : new 
              {
                  controller = "content",
                  action = "{action}"
              }
            );
        }
    }
}
