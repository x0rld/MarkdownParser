namespace MarkdownTest;

public static class XunitExtensionMethod
{
    public static void EqualIgnoringWhiteSpace(this ICheck<string> check,string expected)
    {
        check.Equals(expected.ReplaceLineEndings("").Replace(" ",""));
    }
}