using NUnit.Framework;
using Should;

namespace TagCloud.Core.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Sparc.TagCloud;

    public class TagCloudAnalyzerTests
    {
        [Test]
        public void Shuffle_should_work()
        {
            var original = new string[] { "word1", "word2", "word3", "word4" };
            var result = GetShuffledWords(original);

            // Try once more, just in case the randomization happens
            // to have granted the same ordering.
            if (result.SequenceEqual(original))
            {
                result = GetShuffledWords(original);
            }

            original.ShouldNotEqual(result);
            original.ShouldEqual(result.OrderBy(s => s).ToArray());
        }

        [Test]
        public void There_should_be_a_max_result_size()
        {
            var results = new TagCloudAnalyzer(new TagCloudSetting() { MaxCloudSize = 2 })
                .ComputeTagCloud(new string[] { "hi hi fancy pant pant" });

            results.Count().ShouldEqual(2);
            results.Any(t => t.Text == "hi").ShouldBeTrue();
            results.Any(t => t.Text == "pant").ShouldBeTrue();
        }

        [Test]
        public void Common_words_are_ignored()
        {
            var result = GetWords(new string[] {
                "be there or be square"
            });

            result.ContainsKey("be").ShouldBeFalse();
            result.ContainsKey("or").ShouldBeFalse();
            result.ContainsKey("square").ShouldBeTrue();
        }

        [Test]
        public void Results_are_sorted()
        {
            var result = new TagCloudAnalyzer().ComputeTagCloud(new string[] {
                "hi booger bum hi bum guy booger hi booger hi"
            }).Select(t => t.Text).ToArray();

            result.ShouldEqual(new string[] { "hi", "booger", "bum", "guy" });
        }

        [Test]
        public void Words_are_lemmatized()
        {
            var result = GetWords(new string[] {
                "kick kicked"
            });

            result.Count.ShouldEqual(1);
            result["kick"].ShouldEqual(2);
        }

        [Test]
        public void Contractions_are_taken()
        {
            var result = GetWords(new string[] {
                "can't",
                "can't",
                "can"
            });

            result.Count.ShouldEqual(2);
            result["can not"].ShouldEqual(2);
            result["can"].ShouldEqual(1);
        }

        [Test]
        public void Words_are_counted_across_multiple_phrases()
        {
            var result = GetWords(new string[] {
                "Hello world",
                "Hello world",
                "Hello universe"
            });

            result.Count.ShouldEqual(3);
            result["hello"].ShouldEqual(3);
            result["world"].ShouldEqual(2);
            result["universe"].ShouldEqual(1);
        }


        [Test]
        public void Repeated_words_are_counted()
        {
            var result = GetWords(new string[] {
                "Hello Hello Hello Hello"
            });

            result["hello"].ShouldEqual(4);
        }

        [Test]
        public void Case_is_ignored()
        {
            var result = GetWords(new string[] {
                "Hello WORLD",
                "heLLo World"
            });

            result["hello"].ShouldEqual(2);
            result["world"].ShouldEqual(2);
        }

        [Test]
        public void Weird_chars_and_punctuation_are_not_counted()
        {
            var result = GetWords(new string[] {
                "hello&$%world",
                "-+_()hello,!~world{}[]"
            });

            result["hello"].ShouldEqual(2);
            result["world"].ShouldEqual(2);
        }

        [Test]
        public void Numbers_are_counted()
        {
            var result = GetWords(new string[] {
                "hello1 world2",
                "hello1 world3"
            });

            result["hello1"].ShouldEqual(2);
            result["world2"].ShouldEqual(1);
            result["world3"].ShouldEqual(1);
        }

        private static string[] GetShuffledWords(string[] original)
        {
            var result = new TagCloudAnalyzer()
                .ComputeTagCloud(original)
                .Shuffle()
                .Select(t => t.Text)
                .ToArray();
            return result;
        }

        private IDictionary<string, int> GetWords(IEnumerable<string> strs)
        {
            return new TagCloudAnalyzer().ComputeTagCloud(strs)
                .ToDictionary(t => t.Text.ToLowerInvariant(), e => e.Count);
        }
    }
}
