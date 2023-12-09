namespace Challenges.Util;
public static partial class ChallengeExtensions
{
    [System.Text.RegularExpressions.GeneratedRegex(@"-?\d+")]
    private static partial System.Text.RegularExpressions.Regex NumberRegex();

    public static IEnumerable<int?>? GetNumberEnumerableIncNull(this string input) => input.Split(
        new[] { "\r\n", "\r", "\n" },
        StringSplitOptions.None
    ).Select(i => !string.IsNullOrWhiteSpace(i) ? (int?)int.Parse(i) : null);

    public static IEnumerable<string?>? GetLines(this string input) => input.Split(
        new[] { "\r\n", "\r", "\n" },
        StringSplitOptions.None
    );
}