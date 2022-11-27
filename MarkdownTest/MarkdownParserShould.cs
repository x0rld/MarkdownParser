using Markdown;

namespace MarkdownTest;

public class MarkdownParserShould
{
    [Theory]
    [InlineData("# Test h1","<h1>Test h1</h1>")]
    [InlineData("## Test h2","<h2>Test h2</h2>")]
    [InlineData("### Test h3","<h3>Test h3</h3>")]
    [InlineData("#### Test h4","<h4>Test h4</h4>")]
    [InlineData("##### Test h5","<h5>Test h5</h5>")]
    [InlineData("###### Test h6","<h6>Test h6</h6>")]
    public void Return_string_with_tag_h_with_sharp_number(string actual,string expected)
    {
        Check.That(MarkdownParser.Parse(actual)).Equals(expected);
    }

    [Fact]
    public void Throw_a_format_exception_when_more_than_6_sharp()
    {
        
        Check.ThatCode(() => MarkdownParser.Parse("#######")).Throws<FormatException>();
    }

    [Fact]
    public void Return_html_list_with_element_star_numbers()
    {
        Check.That(MarkdownParser.Parse("""
        * list
        """).Equals(
                """
            <ul>
                <li>list</li>
            <ul>
            """));
    }
}