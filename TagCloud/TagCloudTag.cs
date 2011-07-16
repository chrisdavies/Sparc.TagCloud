namespace Sparc.TagCloud
{
    /// <summary>
    /// Represents a single tag in a tag cloud.
    /// </summary>
    public class TagCloudTag
    {
        /// <summary>
        /// Gets or sets the text for the tag.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the category of the tag cloud.
        /// </summary>
        /// <remarks>
        /// This is a numerical grouping under which the tag falls.
        /// </remarks>
        public int Category { get; set; }

        /// <summary>
        /// Gets or sets the number of times this tag appeared in the 
        /// analylzed text.
        /// </summary>
        public int Count { get; set; }
    }
}