using TagCloud.Core;

namespace Sparc.TagCloud
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using LemmaSharp;

    /// <summary>
    /// Represents the settings for a TagCloudAnalyzer instance.
    /// </summary>
    public class TagCloudSetting
    {
        private static Regex defaultWordFinder = new Regex(@"[\w\']+", RegexOptions.Compiled);
        private static LemmatizerPrebuilt defaultLemmatizer = new LemmatizerPrebuiltFull(LanguagePrebuilt.English);
        private static HashSet<string> defaultStopWords = LoadStopWords();

        /// <summary>
        /// Initializes a new instance of the TagCloudSetting class.
        /// </summary>
        public TagCloudSetting()
        {
            this.WordFinder = defaultWordFinder;
            this.Lemmatizer = defaultLemmatizer;
            this.StopWords = defaultStopWords;
            this.MaxCloudSize = 100;
            this.NumCategories = 10;
        }

        /// <summary>
        /// Gets or sets the regex used to find words within strings.
        /// </summary>
        public Regex WordFinder { get; set; }

        /// <summary>
        /// Gets or sets the lemmatizer used to derive the roots of words.
        /// </summary>
        public Lemmatizer Lemmatizer { get; set; }

        /// <summary>
        /// Gets or sets the set of word roots (e.g. "be" rather than "am") to
        /// be ignored.
        /// </summary>
        public ISet<string> StopWords { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the tag cloud.
        /// </summary>
        public int MaxCloudSize { get; set; }

        /// <summary>
        /// Gets or sets the number of tag categories.
        /// </summary>
        public int NumCategories { get; set; }

        private static HashSet<string> LoadStopWords()
        {
            var wordList = Resources.en_US_stop.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return new HashSet<string>(wordList, StringComparer.OrdinalIgnoreCase);
        }
    }
}
