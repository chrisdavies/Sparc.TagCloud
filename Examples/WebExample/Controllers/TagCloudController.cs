namespace WebExample.Controllers
{
    using System.Web.Mvc;
    using Sparc.TagCloud;
    using WebExample.Properties;

    public class TagCloudController : Controller
    {
        public ActionResult Index()
        {
            var phrases = new string[] { Resources.TagCloudInput };
            var model = new TagCloudAnalyzer()
                .ComputeTagCloud(phrases)
                .Shuffle();
            return View(model);
        }
    }
}
