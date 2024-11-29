using static System.Text.RegularExpressions.Regex;

namespace Challenges.Challenge.Y2023.Day05;

[ChallengeName("Day 5: If You Give A Seed A Fertilizer")]
public class Day05: IChallenge
{
    public async Task<object?> TaskPartOne(string input) => Day5Solver(input, longs => longs.Select(i => new Range(i, i)));
    
    public async Task<object?> TaskPartTwo(string input) => Day5Solver(input, longs => longs.Chunk(2).Select(i => new Range(i[0], i[0] + i[1] - 1)));

    private long Day5Solver(string input, Func<long[], IEnumerable<Range>> parseRangesFunc)
    {
        var inputBlocks = input.Split("\r\n\r\n").ToArray();

        IEnumerable<Range> longsToSearch = parseRangesFunc(ParseSeeds(inputBlocks[0]));

        var map = inputBlocks.Skip(1).Select(ParseMaps).ToArray();

        longsToSearch = map.Aggregate(longsToSearch, (current, search) => current.SelectMany(range => FindRanges(range, search)).ToArray());

        return longsToSearch.Min(r => r.Start);
    }

    private long[] ParseSeeds(string input) =>
        Matches(input, @"\d+").Select(match => long.Parse(match.Value)).ToArray();

    private Map[] ParseMaps(string input) =>
        input.GetLines().Skip(1) //Skip the header row
            .Select(GenerateMap).ToArray();

    private Map GenerateMap(string input)
    {
        var mapRangeArray = input.Split(" ").Select(long.Parse).ToArray();

        var src = new Range(mapRangeArray[1], mapRangeArray[1] + mapRangeArray[2] - 1);
        var dest = new Range(mapRangeArray[0], mapRangeArray[0] + mapRangeArray[2] - 1);

        return new Map(src, dest);
    }

    private IEnumerable<Range> FindRanges(Range range, Map[] map)
    {
        var queue = new Queue<Range>();
        queue.Enqueue(range);
        while (queue.Any())
        {
            range = queue.Dequeue();
            var found = false;
            foreach (var lookup in map)
            {
                if (lookup.Src.Start <= range.Start && range.End <= lookup.Src.End)
                {
                    var offset = lookup.Dest.Start - lookup.Src.Start; // get the offset of the value

                    yield return new Range(range.Start + offset, range.End + offset);
                    found = true;
                }
                else if(range.Start < lookup.Src.Start && lookup.Src.Start <= range.End)
                {
                     queue.Enqueue(new Range(range.Start, lookup.Src.Start - 1));
                     queue.Enqueue(new Range(lookup.Src.Start, range.End));
                     found = true;
                }
                else if (range.Start < lookup.Src.End && lookup.Src.End <= range.End)
                {
                    queue.Enqueue(new Range(range.Start, lookup.Src.End - 1));
                    queue.Enqueue(new Range(lookup.Src.End, range.End));
                    found = true;
                }
            }
            if (!found)
                yield return new Range(range.Start, range.End);
        }
    }

    private record Range(long Start, long End);

    private record Map(Range Src, Range Dest);

}