using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Challenges.Challenge.Y2023.Day10;

[ChallengeName("Day 10: Pipe Maze")]
public class Day10 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => GetLoop(input.GetCharGrid()).Values.Max();

    public async Task<object?> TaskPartTwo(string input)
    {
        var grid =  input.GetCharGrid();
        var loop = GetLoop(grid);

        Dictionary<Coord, char> map = new();

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                var coord = new Coord(j, i);

                map.Add(coord, loop.ContainsKey(coord) ? grid[i][j] : '.');

                grid[i][j] = map[coord];
            }
        }

        return map.Keys.Count(pos => IsInside(map, pos));
    }

    bool IsInside(Dictionary<Coord, char> map, Coord? position)
    {
        var cell = map[position!];
        if (cell != '.') return false;

        var inside = false;
        position = position! with{X = position.X -1};
        while (map.ContainsKey(position))
        {
            if ("SJL|".Contains(map[position]))
            {
                inside = !inside;
            }
            position = position with { X = position.X - 1 };
        }
        return inside;
    }

    Dictionary<Coord, int> GetLoop(char[][] grid)
    {
        Coord? startCoord = null;
        for (var y = 0; y < grid.Length; y++)
        {
            if (!grid[y].Contains('S')) continue;

            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 'S') startCoord = new Coord(x, y);
            }

            if (startCoord is not null) break;
        }

        Dictionary<Coord, int> steps = new();

        List<Coord> moves = new();
        List<Coord> previousMoves = new();
        moves.Add(startCoord!);

        int i = 0;

        do
        {
            moves.ForEach(s => steps[s] = i);
            List<Coord> nextMoves = new();
            foreach (var pos in moves)
            {
                getPosNextMoves(grid, pos, previousMoves).ForEach(s => nextMoves.Add(s));
            }

            previousMoves = moves;
            moves = nextMoves;

            i++;
        } while (previousMoves.Any());

        return steps;
    }

    private List<Coord> getPosNextMoves(char[][] grid, Coord? pos, List<Coord> previousMoves)
    {
        List<Coord> nextMoves = new();

        bool canTravelNorth = false;
        bool canTravelSouth = false;
        bool canTravelWest = false;
        bool canTravelEast = false;

        Debug.Assert(pos != null, nameof(pos) + " != null");
        var tube = grid[pos.Y][pos.X];

        switch (tube)
        {
            case 'S': // All Routes
                canTravelNorth = true;
                canTravelSouth = true;
                canTravelWest = true;
                canTravelEast = true;
                break;
            case '|':
                canTravelNorth = true;
                canTravelSouth = true;
                break;
            case '-':
                canTravelEast = true;
                canTravelWest = true;
                break;
            case 'L':
                canTravelNorth = true;
                canTravelEast = true;
                break;
            case '7':
                canTravelSouth = true;
                canTravelWest = true;
                break;
            case 'J':
                canTravelNorth = true;
                canTravelWest = true;
                break;
            case 'F':
                canTravelSouth = true;
                canTravelEast = true;
                break;
        }

        if (canTravelNorth)
        {
            var test = grid[pos.Y - 1][pos.X];

            if(test is '|' or '7' or 'F') nextMoves.Add(pos with { Y = pos.Y - 1 });
        }

        if (canTravelSouth)
        {
            var test = grid[pos.Y + 1][pos.X];

            if (test is '|' or 'L' or 'J') nextMoves.Add(pos with { Y = pos.Y + 1 });
        }

        if (canTravelWest)
        {
            var test = grid[pos.Y][pos.X - 1];

            if (test is '-' or 'L' or 'F') nextMoves.Add(pos with { X = pos.X - 1 });
        }

        if (canTravelEast)
        {
            var test = grid[pos.Y][pos.X + 1];

            if (test is '-' or '7' or 'J') nextMoves.Add(pos with { X = pos.X + 1 });
        }

        return nextMoves.Where(x => !previousMoves.Contains(x)).ToList();
    }

    
    /*
        grid[nextCoord.X][nextCoord.Y] == '|'
       | is a vertical pipe connecting north and south.
       - is a horizontal pipe connecting east and west.
       L is a 90-degree bend connecting north and east.
       J is a 90-degree bend connecting north and west.
       7 is a 90-degree bend connecting south and west.
       F is a 90-degree bend connecting south and east.
       . is ground; there is no pipe in this tile.
       S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
     */



    private record Coord(int X, int Y);


}