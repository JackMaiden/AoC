namespace Challenges.Challenge.Y2024.Day01;

[ChallengeName("Day 1: Historian Hysteria")]
public class Day01 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => ParseInput(input, 0).Zip(ParseInput(input, 1))
        .Select(p => Math.Abs(p.First - p.Second)).Sum();

    public async Task<object?> TaskPartTwo(string input)
    {
        var appearances = ParseInput(input, 1).CountBy(x=> x).ToDictionary();
        return ParseInput(input, 0).Select(i =>  i * appearances.GetValueOrDefault(i)).Sum();
    }

    private IEnumerable<int> ParseInput(string input, int column)
    {
        var x = input.GetLines().Select(l => l.GetIntArr());
        return x.Select(s => s[column]).OrderBy(i => i);
    }
}