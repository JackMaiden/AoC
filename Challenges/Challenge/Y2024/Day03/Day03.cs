namespace Challenges.Challenge.Y2024.Day03;
[ChallengeName("Day 3: Mull It Over")]
public partial class Day03 : IChallenge
{

    public async Task<object?> TaskPartOne(string input) => input.GetLines().Select(l =>
        MulsRegex().Matches(l).Select(s => Multiply(int.Parse(s.Groups[1].Value), int.Parse(s.Groups[2].Value))).Sum()).Sum();

    public async Task<object?> TaskPartTwo(string input)
    {
        var lineMatches = input.GetLines().Select(l => ComandsRegex().Matches(l).Select(s => s.Value));

        bool runMul = true;

        var sum = 0;
        
        foreach (var matches in lineMatches)
        {
            foreach (var match in matches)
            {
                switch (match.ToLowerInvariant())
                {
                    case "do()":
                        runMul = true;
                        break;
                    case "don't()":
                        runMul = false;
                        break;
                    default:
                        sum += runMul
                            ? MulsRegex().Matches(match).Select(s =>
                                Multiply(int.Parse(s.Groups[1].Value), int.Parse(s.Groups[2].Value))).Sum()
                            : 0;
                        break;
                }
            }
        }
        return sum;
    }


    private int Multiply(int a, int b) => a * b;
    
    [System.Text.RegularExpressions.GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial System.Text.RegularExpressions.Regex MulsRegex();
    
    [System.Text.RegularExpressions.GeneratedRegex(@"(do\(\))|(don't\(\))|(mul\(\d+,\d+\))")]
    private static partial System.Text.RegularExpressions.Regex ComandsRegex();

}
