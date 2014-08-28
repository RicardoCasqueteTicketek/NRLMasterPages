namespace NRL.MasterPages.Resources.Controllers
{
    //using NRL.MasterPages.Common;
    using NRL.MasterPages.Common;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ContentController : Controller
    {
        public ActionResult Jpg(string name)
        {
            var path = getContentPath("Images", string.Format("{0}.jpg", name));
            return File(Server.MapPath(path), "image/jpeg");
        }

        public ActionResult Png(string name)
        {
            var path = getContentPath("Images", string.Format("{0}.png", name));
            return File(Server.MapPath(path), "image/png");
        }

        public ActionResult Css(string name)
        {
            var path = getContentPath("CSS", string.Format("{0}.css", name));
            return File(Server.MapPath(path), "text/css");
        }

        public ActionResult Script(string name)
        {
            var path = getContentPath("JavaScript", string.Format("{0}.js", name));
            using (var sr = new StreamReader(Server.MapPath(path)))
            {
                return JavaScript(sr.ReadToEnd());
            }
        }

        public ActionResult Json(string name)
        {
            var path = getContentPath("JSON", string.Format("{0}.json", name));
            using (var sr = new StreamReader(Server.MapPath(path)))
            {
                return JavaScript(string.Format("var {0} = {1}", name, sr.ReadToEnd()));
            }
        }

        private string getContentPath(string type, string fileName)
        {
            var requestedClub = ClubConfig.Instance.FirstOrDefault(club => club.Resources == Request.Url.Host);

            if (requestedClub != null)
            {
                var sharedPath = string.Format(@"~/Content/{0}/{1}", type, fileName);
                var clubPath = string.Format(@"~/Content/{0}/{1}/{2}", type, requestedClub.Name, fileName);

                if (System.IO.File.Exists(Server.MapPath(sharedPath)))
                    return sharedPath;

                return clubPath;
            }
            return string.Empty;
        }
	}
}