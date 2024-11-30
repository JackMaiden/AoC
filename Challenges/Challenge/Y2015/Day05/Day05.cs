namespace Challenges.Challenge.Y2015.Day05;

[ChallengeName("Day 5: Doesn't He Have Intern-Elves For This?")]
public class Day05 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => NiceStringsA(input.GetLines()).Count();

    public async Task<object?> TaskPartTwo(string input) => NiceStringsB(input.GetLines()).Count();
    
    private IEnumerable<string> NiceStringsA(IEnumerable<string> strings)
    {
        return strings.Where(line =>
        {
            var vowels = line.Count(c => "aeiou".Contains(c)) >= 3;
            var doubles = line.Zip(line.Skip(1), (a, b) => a == b).Any(x => x);
            var forbidden = new[]{"ab", "cd", "pq", "xy"}.Any(line.Contains);
            return vowels && doubles && !forbidden;
        });
    }
    
    private IEnumerable<string> NiceStringsB(IEnumerable<string> strings)
    {
        return strings.Where(line =>
        {
            var doubleDoubles = line.Zip(line.Skip(1), (a, b) => $"{a}{b}")
                .Select((pair, index) => new { Pair = pair, Index = index }).Any(pair =>
                    line.IndexOf(pair.Pair, pair.Index + 2, StringComparison.Ordinal) > -1);
            var repeatChar = line.Zip(line.Skip(2), (a, b) => a == b).Any(x => x);
            return doubleDoubles && repeatChar;
        });
    }
}