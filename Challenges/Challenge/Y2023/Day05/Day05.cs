using System.Text.RegularExpressions;

namespace Challenges.Challenge.Y2023.Day05;

[ChallengeName("Day 5: If You Give A Seed A Fertilizer")]
public class Day05: IChallenge
{
    public async Task<object> TaskPartOne(string input)
    {
        return almanacParse(input, longs => longs.Select(i => new Range(i, i)));
    }

    public async Task<object> TaskPartTwo(string input)
    {
        return almanacParse(input, longs => longs.Chunk(2).Select(i => new Range(i[0], i[0] + i[1] - 1))); ;
    }

    long almanacParse(string input, Func<long[], IEnumerable<Range>> parseRangesFunc)
    {
        IEnumerable<Range> longsToSearch;

        var inputBlocks = input.Split("\r\n\r\n").ToArray();

        longsToSearch = parseRangesFunc(parseSeeds(inputBlocks[0]));

        var map = inputBlocks.Skip(1).Select(parseMaps).ToArray();

        foreach (var search in map)
        {
            longsToSearch = longsToSearch.SelectMany(range => findRanges(range, search)).ToArray();
        }

        return longsToSearch.Min(r => r.start);
    }


    long[] parseSeeds(string input)
    {
        return (from match in Regex.Matches(input, @"\d+")
            select long.Parse(match.Value)).ToArray();
    }

    Map[] parseMaps(string input)
    {

        var mapBodies = input.GetLines().Skip(1);

        return mapBodies
            .Select(s =>
            {
                var x = s.Split(" ").Select(long.Parse).ToArray();

                var src = new Range(x[1], x[1] + x[2] - 1);
                var dest = new Range(x[0], x[0] + x[2] - 1);
                return new Map(src, dest);
            }).ToArray();
    }

    IEnumerable<Range> findRanges(Range range, Map[] map)
    {
        var queue = new Queue<Range>();
        queue.Enqueue(range);
        while (queue.Any())
        {
            range = queue.Dequeue();
            bool found = false;
            foreach (var lookup in map)
            {
                if (lookup.src.start <= range.start && range.end <= lookup.src.end)
                {
                    var offset = lookup.dest.start - lookup.src.start; //get the offset of the value

                    yield return new Range(range.start + offset, range.end + offset);
                    found = true;
                }
                else if(range.start < lookup.src.start && lookup.src.start <= range.end)
                {
                     queue.Enqueue(new Range(range.start, lookup.src.start - 1));
                     queue.Enqueue(new Range(lookup.src.start, range.end));
                    found = true;
                }
                if (range.start < lookup.src.end && lookup.src.end <= range.end)
                {
                    queue.Enqueue(new Range(range.start, lookup.src.end - 1));
                    queue.Enqueue(new Range(lookup.src.end, range.end));
                    found = true;
                }

            }

            if (!found)
                yield return new Range(range.start, range.end);
        }
    }

    record Range(long start, long end);

    record Map(Range src, Range dest);

}