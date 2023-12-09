namespace Challenges.Util;
public partial class ChallengeExtensions
{
    private static readonly string[] NewLineSeparator = { "\r\n", "\r", "\n" };
    private static readonly string[] DoubleNewLineSeparator = { "\r\n\r\n", "\r\r", "\n\n" };

    public static IEnumerable<int?> GetNumberEnumerableIncNull(this string input) => input.Split(
        separator: NewLineSeparator,
        StringSplitOptions.None
    ).Select(i => !string.IsNullOrWhiteSpace(i) ? (int?)int.Parse(i) : null);

    public static IEnumerable<string?> GetLines(this string input) => input.Split(
        separator: NewLineSeparator,
        StringSplitOptions.None
    );

    public static IEnumerable<string?> GetBlocks(this string input) => input.Split(
        separator: DoubleNewLineSeparator,
        StringSplitOptions.None
    );
}
