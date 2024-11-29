namespace Challenges.Challenge.Y2023.Day11;

[ChallengeName("Day 11: Cosmic Expansion")]
public class Day11 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => shortestPath(input, 1);

    public async Task<object?> TaskPartTwo(string input) => shortestPath(input, 1000000 - 1);

    private long shortestPath(string input, int expansion)
    {
        var universe = ParseUniverse(input, expansion);

        var galaxy = universe.Where(x => x.Value == '#').Select(x => x.Key).ToArray();

        List<long> pairPath = [];
        int x = 0;
        for (int i = 0; i < galaxy.Count(); i++)
        {
            for (int j = i+1; j < galaxy.Count(); j++)
            {
                var pos1 = galaxy[i];
                var pos2 = galaxy[j];

                var xDiff = Math.Abs(pos1.X - pos2.X);
                var yDiff = Math.Abs(pos1.Y - pos2.Y);

                pairPath.Add(xDiff + yDiff);
                x++;
            }
        }

        return pairPath.Sum();
    }

    private Dictionary<Coord, char> ParseUniverse(string input, int expansion)
    {
        var galaxy = input.GetCharGrid();

        int galaxyOffsetX = 0;
        int galaxyOffsetY = 0;

        Dictionary<Coord, char> map = [];
        Dictionary<int, int> OffsetY = [];
        Dictionary<int, int> OffsetX = [];

        for (int Y = 0; Y < galaxy.Length; Y++)
        {
            OffsetY.Add(Y, galaxyOffsetY);
            if (galaxy[Y].All(x => x == '.'))
                galaxyOffsetY += expansion;
        }

        for (int X = 0; X < galaxy[0].Length; X++)
        {
            OffsetX.Add(X, galaxyOffsetX);
            if (galaxy.All(x => x[X] == '.'))
                galaxyOffsetX+= expansion;
        }

        for (int Y = 0; Y < galaxy.Length; Y++)
        {
            for (int X = 0; X < galaxy[Y].Length; X++)
            {
                map.Add(new Coord(OffsetX[X] + X, OffsetY[Y] + Y), galaxy[Y][X]);
            }
        }

        return map;
    }


    private record Coord(int X, int Y);
}