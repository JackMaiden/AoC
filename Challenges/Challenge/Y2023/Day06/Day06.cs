namespace Challenges.Challenge.Y2023.Day06;

[ChallengeName("Day 6: Wait For It")]
public partial class Day06 : IChallenge
{
    public async Task<object> TaskPartOne(string input)
    {
        return solve(parseAndGetRaces(input));
    }

    public async Task<object> TaskPartTwo(string input)
    {
        return solve(parseAndGetRaces(input.Replace(" ", "")));
    }

    private long solve(IEnumerable<Race> races) => races.Select(x => x.Possibles.Count(y => y > x.Dist))
        .Aggregate(1, (x, y) => x * y);

    private long[][] parse(string input) => input.GetLines()
        .Select(lines => NumberRegex().Matches(lines).Select(match => long.Parse(match.Value)).ToArray()).ToArray();

    IEnumerable<Race> parseAndGetRaces(string input) => getRaces(parse(input));

    private IEnumerable<Race> getRaces(long[][] races)
    {
        List<Race> parsedRaces = [];

        for (var i = 0; i < races[0].Length ; i++)
        {
            List<long> results = [];

            for (var j = 1; j < races[0][i]; j++)
            {
                var travelDuration = races[0][i] - j;

                results.Add(travelDuration * j);
            }

            parsedRaces.Add(new Race(races[1][i], [.. results]));
        }

        return parsedRaces;
    }

    private record Race (long Dist, long[] Possibles);

    [System.Text.RegularExpressions.GeneratedRegex(@"\d+")]
    private static partial System.Text.RegularExpressions.Regex NumberRegex();
}