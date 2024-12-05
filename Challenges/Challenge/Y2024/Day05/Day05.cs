namespace Challenges.Challenge.Y2024.Day05;

[ChallengeName("Day 5: Print Queue")]
public class Day05 : IChallenge
{
    public async Task<object?> TaskPartOne(string input)
    {
        var (comparer, updates) = Parser(input);

        return updates.Where(u => IsSorted(u, comparer)).Sum(x=> x[x.Length/2]);
    }

    public async Task<object?> TaskPartTwo(string input)
    {
        var (comparer, updates) = Parser(input);

        return updates.Where(u => !IsSorted(u, comparer)).Select(s => s.OrderBy(i => i, comparer).ToArray()).Sum(x=> x[x.Length/2]);
    }

    private (Comparer<int>, int[][]) Parser(string input)
    {
        var blocks = input.GetBlocks().ToArray();

        var rules = blocks[0].GetLines().ToHashSet();
        
        var comparer = Comparer<int>.Create((x, y) => rules.Contains($"{x}|{y}") ? -1 : 1);

        var updates = blocks[1].GetLines().Select(s => s.Split(",").Select(int.Parse).ToArray()).ToArray();

        return (comparer, updates);
    }

    private static bool IsSorted(int[] input, Comparer<int> comparer)
    {
        return input.SequenceEqual(input.OrderBy(x => x, comparer));
    }
}