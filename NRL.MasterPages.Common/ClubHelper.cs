namespace NRL.MasterPages.Common
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;

    public static class ClubHelper
    {
        public static String Resources<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            var requestedClub = ClubConfig.Instance.FirstOrDefault(club => club.Host == HttpContext.Current.Request.Url.Host);
            if (requestedClub != null)
                return requestedClub.Resources;

            return string.Empty;
        }
    }
}
