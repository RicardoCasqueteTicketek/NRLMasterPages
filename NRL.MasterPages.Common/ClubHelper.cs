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

        public static bool IsMarkOnlyUpRequest<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return HttpContext.Current.Request.Url.LocalPath.ToLower().EndsWith("/home/blank");
        }

        public static String Title<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            var requestedClub = ClubConfig.Instance.FirstOrDefault(club => club.Host == HttpContext.Current.Request.Url.Host);
            if (requestedClub != null)
                return string.Format("{0} Memberships Portal", requestedClub.Name.capitlizeFirst());

            return string.Empty;
        }

        private static string capitlizeFirst(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString) || inputString.Length <= 1)
                return string.Empty;

            return string.Format("{0}{1}", char.ToUpper(inputString[0]), inputString.Substring(1));
        }
    }
}
