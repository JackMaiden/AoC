namespace Challenges.Challenge.Y2015.Day03;

[ChallengeName("Day 3: Perfectly Spherical Houses in a Vacuum")]
public class Day03 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) =>
        PresentLocations(input.ToCharArray(), new Dictionary<int, Coord>
        {
            { 1, new Coord(0, 0) }
        });

    public async Task<object?> TaskPartTwo(string input) => 
        PresentLocations(input.ToCharArray(), new Dictionary<int, Coord>
        {
            { 1, new Coord(0, 0) },
            { -1, new Coord(0, 0) }
        });
    
    private int PresentLocations(char[] input, Dictionary<int, Coord> santaLoc)
    {
        var presents = new Dictionary<Coord, int> { { new Coord(0, 0), santaLoc.Count } };
        var santas = santaLoc.Keys.ToArray();

        for (var i = 0; i < input.Length; i++)
        {
            var santa = santas[i % santas.Length];
            var loc = santaLoc[santa];

            loc = input.ElementAt(i) switch
            {
                '>' => loc with { X = loc.X + 1 },
                '<' => loc with { X = loc.X - 1 },
                'v' => loc with { Y = loc.Y - 1 },
                '^' => loc with { Y = loc.Y + 1 },
                _ => loc
            };

            santaLoc[santa] = loc;
            presents.AddPresent(loc);
        }

        return presents.Count;
    }
}

internal record struct Coord(int X, int Y);

internal static class Day03Extensions
{

    public static void AddPresent(this Dictionary<Coord, int> presents, Coord coord)
    {
        if (!presents.TryAdd(coord, 1))
        {
            presents[coord]++;
        }
    }
        
}