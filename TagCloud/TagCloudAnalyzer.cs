namespace Sparc.TagCloud
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Analyzes a batch of text to produce a tag-cloud.
    /// </summary>
    public class TagCloudAnalyzer
    {
        private TagCloudSetting setting;

        /// <summary>
        /// Initializes a new instance of the TagCloudAnalyzer class.
        /// </summary>
        /// <param name="setting">
        /// The settings which dictate the analyzer's behavior.
        /// </param>
        public TagCloudAnalyzer(TagCloudSetting setting = null)
        {
            if (setting == null)
            {
                setting = new TagCloudSetting();
            }

            this.setting = setting;
        }

        /// <summary>
        /// Computes the tag cloud from a set of phrases.
        /// </summary>
        /// <param name="phrases">
        /// The phrases used to compute the tag cloud
        /// </param>
        /// <returns>The tag cloud.</returns>
        public IEnumerable<TagCloudTag> ComputeTagCloud(IEnumerable<string> phrases)
        {
            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            ExtractAndCountWords(phrases, dict);
            return SortAndFilterResults(dict).ToArray();
        }

        private IEnumerable<TagCloudTag> SortAndFilterResults(
            Dictionary<string, int> dict)
        {
            var total = Math.Min(setting.MaxCloudSize, dict.Count);
            var div = Math.Max(1, total / setting.NumCategories);
            var index = 0;

            return dict.OrderByDescending(p => p.Value)
                .Where(p => !setting.StopWords.Contains(p.Key))
                .Take(setting.MaxCloudSize)
                .Select(p => new TagCloudTag()
                {
                    Text = p.Key.Replace('+', ' '),
                    Count = p.Value,
                    Category = (index++)/div
                });
        }

        private void ExtractAndCountWords(IEnumerable<string> phrases, Dictionary<string, int> dict)
        {
            foreach (var phrase in phrases)
            {
                CountWords(phrase, dict);
            }
        }

        private void CountWords(string phrase, Dictionary<string, int> dict)
        {
            var words = setting.WordFinder.Matches(phrase)
                .Cast<Match>()
                .Select(m => setting.Lemmatizer.Lemmatize(m.Value));

            foreach (var word in words)
            {
                int count = 0;
                dict.TryGetValue(word, out count);
                ++count;
                dict[word] = count;
            }
        }
    }
}
