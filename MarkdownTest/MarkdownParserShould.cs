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
    public void Up_to_six_sharp_return_string_with_tag_h_with_sharp_number(string actual,string expected)
    {
        Check.That(MarkdownParser.Parse(actual)).Equals(expected);
    }
}