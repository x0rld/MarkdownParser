namespace Markdown.Tests;

public class MarkdownParserShould
{
    [Fact]
    public void AcceptanceTest()
    {
        var actual = new MarkdownParser("""
        # title1
        * list
        * list
        ##### title5
        """
        ).Parse();
        var expected = """
        <h1>title1</h1>
        <ul>
            <li>list</li>
            <li>list</li>
        </ul>  
        <h5>title5</h5>
        """;
        Check.That(actual).EqualIgnoringWhiteSpace(expected);
    }

    [Theory]
    [InlineData("# Test h1", "<h1>Test h1</h1>")]
    [InlineData("## Test h2", "<h2>Test h2</h2>")]
    [InlineData("### Test h3", "<h3>Test h3</h3>")]
    [InlineData("#### Test h4", "<h4>Test h4</h4>")]
    [InlineData("##### Test h5", "<h5>Test h5</h5>")]
    [InlineData("###### Test h6", "<h6>Test h6</h6>")]
    public void Return_string_with_tag_h_with_sharp_number(string actual, string expected)
    {
        Check.That(new MarkdownParser(actual).Parse()).Equals(expected);
    }

    [Fact]
    public void Throw_a_format_exception_when_more_than_6_sharp()
    {
        Check.ThatCode(() => new MarkdownParser("#######").Parse()).Throws<FormatException>();
    }

    [Fact]
    public void Return_html_list_with_element_star_numbers()
    {
        var actual = new MarkdownParser("""
        * list
        * list
        """).Parse();
        var expected = """  
            <ul>
                <li>list</li>
                <li>list</li>
            </ul>
            """;
        Check.That(actual).EqualIgnoringWhiteSpace(expected);
    }
}