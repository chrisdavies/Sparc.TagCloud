namespace ConsoleExample
{
    using System;
    using System.IO;
    using System.Linq;
    using Sparc.TagCloud;

    public static class Program
    {
        static void Main(string[] args)
        {
            var words = new TagCloudAnalyzer()
                .ComputeTagCloud(File.ReadAllLines("Sample_Text_1.txt"));
            
            Console.Write(string.Join(
                Environment.NewLine, 
                words.Select(p => "[" + p.Count + "] \t" + p.Text).ToArray()));
            
            Console.ReadLine();
        }
    }
}
