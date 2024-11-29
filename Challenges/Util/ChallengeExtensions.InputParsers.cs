namespace Challenges.Util;
public partial class ChallengeExtensions
{
    private static readonly string[] NewLineSeparator = ["\r\n", "\r", "\n"];
    private static readonly string[] DoubleNewLineSeparator = ["\r\n\r\n", "\r\r", "\n\n"];

    public static IEnumerable<int?> GetNumberEnumerableIncNull(this string input) => input.Split(
        separator: NewLineSeparator,
        StringSplitOptions.None
    ).Select(i => !string.IsNullOrWhiteSpace(i) ? (int?)int.Parse(i) : null);

    public static IEnumerable<string> GetLines(this string input) => input.Split(
        separator: NewLineSeparator,
        StringSplitOptions.None
    );

    public static IEnumerable<string> GetBlocks(this string input) => input.Split(
        separator: DoubleNewLineSeparator,
        StringSplitOptions.None
    );

    public static char[][] GetCharGrid(this string input) => input
        .Split(separator: NewLineSeparator, StringSplitOptions.None).Select(x => x.ToCharArray()).ToArray();

    public static int[] GetIntArr(this string input) => NumberRegex().Matches(input).Select(match => int.Parse(match.Value)).ToArray();

    public static long[] GetLongArr(this string input) => NumberRegex().Matches(input).Select(match => long.Parse(match.Value)).ToArray();
}
