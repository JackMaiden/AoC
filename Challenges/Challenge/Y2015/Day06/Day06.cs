using System.Text.RegularExpressions;

namespace Challenges.Challenge.Y2015.Day06;

[ChallengeName("Day 6: Probably a Fire Hazard")]
public class Day06 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => PartOne(input);

    public async Task<object?> TaskPartTwo(string input) => PartTwo(input);

    private record struct Coord(int X, int Y);

    private int PartOne(string input)
    {
        var map = GetMap();
        var instructions = ParseInstructions(input);

        map = instructions.Aggregate(map, (current, instruction) => FollowInstruction(instruction, current, _ => 1, _ => 0, v => 1 - v));

        return map.Values.Count(x => x==1);
    }

    private int PartTwo(string input)
    {
        var map = GetMap();
        var instructions = ParseInstructions(input);

        map = instructions.Aggregate(map, (current, instruction) => FollowInstruction(instruction, current, v => v + 1, v => v > 0 ? v - 1 : 0 , v => v + 2));

        return map.Values.Sum();
    }

    private Dictionary<Coord, int> FollowInstruction(Instruction instruction, 
        Dictionary<Coord, int> map,
        Func<int,int> turnOn,
        Func<int,int> turnOff,
        Func<int,int> toggle)
    {
        for (var x = instruction.Start.X; x <= instruction.End.X; x++)
        {
            for (var y = instruction.Start.Y; y <= instruction.End.Y; y++)
            {
                var coord = new Coord(x, y);
                
                map[coord] = instruction.Action switch
                {
                    "turn on" => turnOn(map[coord]),
                    "turn off" => turnOff(map[coord]),
                    _ => toggle(map[coord])
                };
            }
        }
        return map;
    }
    
    private Dictionary<Coord, int> GetMap()
    {
        var map = new Dictionary<Coord, int>();
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                map.Add(new Coord(i, j), 0);
            }
        }
        return map;
    }

    private record struct Instruction(string Action, Coord Start, Coord End);


    private IEnumerable<Instruction> ParseInstructions(string input)
    {
        var regex = new Regex(@"(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)"); 
        
        var lines = input.GetLines();

        return lines.Select(line =>
        {
            var match = regex.Match(line);

            var action = match.Groups[1].Value;
            var coordA = new Coord(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
            var coordB = new Coord(int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value));

            return new Instruction(action, coordA, coordB);
        });
    }
}