using System.Text;

namespace Markdown;

public class MarkdownParser
{
    private const string TemplateTitleTag = @"<h{0}>Test h{0}</h{0}>";

    public static string Parse(string markdownString)
    {
        var htmlTitle = SharpToHtmlTitle(markdownString);
        var htmlList = StarToHtmlList(htmlTitle);
        return htmlList;
    }

    private static string StarToHtmlList(string toParse)
    {
        if (toParse.All(ch => ch != '*'))
        {
            return toParse;
        }
        var builder = new StringBuilder();
        builder.Append("<ul>");
        builder.Append("<li>");
        builder.Append(toParse.Split('*').Last().Trim());
        builder.Append("</li>");
        builder.Append("</ul>");
        return builder.ToString();
    }

    private static string SharpToHtmlTitle(string markdownString)
    {
        return CountSharpChar(markdownString) switch
        {
            1 => string.Format(TemplateTitleTag,"1"),
            2 => string.Format(TemplateTitleTag,"2"),
            3 => string.Format(TemplateTitleTag,"3"),
            4 => string.Format(TemplateTitleTag,"4"),
            5 => string.Format(TemplateTitleTag,"5"),
            6 => string.Format(TemplateTitleTag,"6"),
            0 => markdownString,
            _ => throw new FormatException("<h> can go only up to 6")
        };
    }

    private static int CountSharpChar(string markdownString)
    {
        return markdownString.Count(ch => ch == '#');
    }
}