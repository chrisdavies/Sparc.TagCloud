#Sparc.TagCloud - A TagCloud library for .NET
The TagCloud library is a simple tag-cloud library for .NET.  It consumes a set of strings and produces a set of tag objects.  Each tag object contains the tag's text or phrase, the number of times that phrase occurred inside of the set of strings, and the tag's category.

##Lemmatizer
This library uses the wonderful LemmaGen library to lemmatize the analyzed text.  This means that words will be reduced to their bases (e.g. "cat" and "cats") will show up in the tag-cloud simply as "cat".  (The LemmaGen library can be found here: http://lemmatise.ijs.si/Software/Version3).

##Tag Categories
The category is simply a mechanism for grouping tags.  The library defaults to 10 category types.  So, given a set of 100 words, the 10 most frequently occuring would be category 0.  The next 10 most frequently occuring words would be category 1, etc.  This categorization allows one to easily cluster tags for a nice visual effect.  (See the ASP.NET MVC example.)

##Examples
Below is a simple code example that analyzes a set of blogposts and returns a tag-cloud.


```csharp
var analyzer = new TagCloudAnalyzer();

// blogPosts is an IEnumerable<String>, loaded from
// the database or whatevz.
var tags = analyzer.ComputeTagCloud(blogPosts);

// Shuffle the tags, if you like for a random
// display
tags = tags.Shuffle();

```

For more examples, including rendering a tag-cloud using ASP.NET MVC, look at the Examples folder in the source.

##License
This is licenced under the "do whatever the hell you want with this, just don't pester me if your mods screw things up" license.