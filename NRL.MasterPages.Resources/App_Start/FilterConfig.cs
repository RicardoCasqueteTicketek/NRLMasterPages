﻿using System.Web;
using System.Web.Mvc;

namespace NRL.MasterPages.Resources
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
