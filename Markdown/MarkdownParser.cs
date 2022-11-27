namespace Markdown;

public class MarkdownParser
{
    private const string TemplateTitleTag = @"<h{0}>Test h{0}</h{0}>";

    public static string Parse(string markdownString)
    {
        var htmlTitle = SharpToHtmlTitle(markdownString);
        return htmlTitle;
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
            _ => throw new ArgumentException(markdownString)
        };
    }

    private static int CountSharpChar(string markdownString)
    {
        return markdownString.Count(str => str == '#');
    }
}