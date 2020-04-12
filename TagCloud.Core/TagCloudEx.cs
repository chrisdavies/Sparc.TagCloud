namespace Sparc.TagCloud
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Extension methods for tag cloud collections.
    /// </summary>
    public static class TagCloudEx
    {
        /// <summary>
        /// Shuffles the specified tag cloud.
        /// </summary>
        /// <param name="tags">The tag cloud to be shuffled.</param>
        /// <returns>The shuffled tag cloud.</returns>
        public static IEnumerable<TagCloudTag> Shuffle(this IEnumerable<TagCloudTag> tags)
        {
            var rand = new Random();
            var tagArr = tags.ToArray();
            for (int i = 0; i < tagArr.Length; ++i)
            {
                var x = rand.Next(0, tagArr.Length);
                var y = rand.Next(0, tagArr.Length);
                tagArr.SwapAt(x, y);
            }

            return tagArr;
        }

        private static void SwapAt(this TagCloudTag[] tagArr, int x, int y)
        {
            var tmp = tagArr[x];
            tagArr[x] = tagArr[y];
            tagArr[y] = tmp;
        }
    }
}
