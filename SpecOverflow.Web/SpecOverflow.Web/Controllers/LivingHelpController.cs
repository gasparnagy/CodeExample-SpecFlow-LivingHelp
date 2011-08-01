using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using SpecOverflow.LivingHelp;

namespace SpecOverflow.Web.Controllers
{
    public class LivingHelpController : Controller
    {
        public ActionResult Index()
        {
            string descriptorPath = Request.MapPath("/Content/SpecOverFlow.xml");

            HelpDescriptionParser parser = new HelpDescriptionParser();
            var helpDescription = parser.Parse(descriptorPath);

            var urlReferrer = Request.UrlReferrer;
            if (urlReferrer == null)
                urlReferrer = Request.Url;
            var page = helpDescription.Pages.FirstOrDefault(p => p.Path == urlReferrer.AbsolutePath);
            if (page == null)
                return Content("");

            return PartialView("_IndexPartial", page);
        }
    }
}
