using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sparc.TagCloud;
using WebExample.Properties;

namespace WebExample.Pages
{
    public class IndexModel : PageModel
    {
        public TagCloudTag[] Tags { get; private set; }

        public void OnGet()
        {
            var phrases = new[] {Resources.CloudInput};
            Tags = new TagCloudAnalyzer()
               .ComputeTagCloud(phrases)
               .Shuffle()
               .ToArray();
        }
    }
}