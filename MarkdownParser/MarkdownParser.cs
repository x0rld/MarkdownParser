using System.Text;

namespace Markdown;

public class MarkdownParser
{
    private const string TemplateTitleTag = @"<h{0}>{1}</h{0}>";
    private readonly StringBuilder _html = new();
    private readonly string _markdownString;
    private bool _ulOpen;

    public MarkdownParser(string markdownString)
    {
        _markdownString = markdownString;
    }

    public string Parse()
    {
        foreach (var line in _markdownString.Split(Environment.NewLine))
        {
            if (line.StartsWith('#'))
            {
                if (_ulOpen)
                {
                    _html.Append("</ul>");
                    _ulOpen = false;
                }
                _html.Append(SharpToHtmlTitle(line));
            }
            else if (line.StartsWith('*'))
            {
                if (_ulOpen == false)
                {
                    _html.Append("<ul>");
                    _ulOpen = true;
                }
                _html.Append(StarToHtmlList(line));
            }
        }
        if (_ulOpen)
        {
            _html.Append("</ul>");
            _ulOpen = false;
        }
        return _html.ToString();
    }

    private string StarToHtmlList(string toParse)
    {
        if (toParse.All(ch => ch != '*'))
        {
            return toParse;
        }

        var builder = new StringBuilder();
        foreach (var element in toParse.Split('*').Skip(1))
        {
            builder.Append("<li>");
            builder.Append(element.Trim());
            builder.Append("</li>");
        }
        return builder.ToString();
    }

    private  string SharpToHtmlTitle(string markdownString)
    {
        var content = string.Join("",markdownString.Skip(CountSharpChar(markdownString)).ToList()).Trim();
        return CountSharpChar(markdownString) switch
        {
            1 => string.Format(TemplateTitleTag, "1",content),
            2 => string.Format(TemplateTitleTag, "2",content),
            3 => string.Format(TemplateTitleTag, "3",content),
            4 => string.Format(TemplateTitleTag, "4",content),
            5 => string.Format(TemplateTitleTag, "5",content),
            6 => string.Format(TemplateTitleTag, "6",content),
            0 => markdownString,
            _ => throw new FormatException("<h> can go only up to 6")
        };
    }

    private  int CountSharpChar(string markdownString)
    {
        return markdownString.Count(ch => ch == '#');
    }
}