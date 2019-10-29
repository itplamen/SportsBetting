namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes
{
    using System.Text.RegularExpressions;

    using HtmlAgilityPack;

    public static class HtmlNodesLoader
    {
        public static HtmlNode Load(string html)
        {
            html = Regex.Replace(html, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            return document.DocumentNode.FirstChild;
        }
    }
}