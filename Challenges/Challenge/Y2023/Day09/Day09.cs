namespace Challenges.Challenge.Y2023.Day09;

[ChallengeName("Day 9: Mirage Maintenance")]
public class Day09 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => input.GetLines().Select(x => x.GetLongArr()).Select(NextSense).Sum();// Solve(input, s => s.GetLongArr()).Sum();

    public async Task<object?> TaskPartTwo(string input) => input.GetLines().Select(x => x.GetLongArr().Reverse().ToArray()).Select(NextSense).Sum();//Solve(input, s => s.GetLongArr().Reverse().ToArray()).Sum();

    [Obsolete]
    IEnumerable<long> Solve(string input, Func<string, long[]> getArr)
    {
        var sensors = Parse(input, getArr);

        List<long> newVals = [];
        foreach (var sensor in sensors)
        {
            long prevValue = 0;

            foreach (var child in sensor.Reverse())
            {
                prevValue += child.Last();
            }

            newVals.Add(prevValue);
        }
        return newVals;
    }

    [Obsolete]
    private IEnumerable<long[]> GenSequence(long[] numbers)
    {
        var sequence = new List<long[]>();

        while (true)
        {
            sequence.Add(numbers);
            List<long> seq = new();
            for (var i = 1; i < numbers.Length; i++)
            {
                seq.Add(numbers[i] - numbers[i - 1]);
            }
            numbers = seq.ToArray();

            if (seq.Any())
            {
                continue;
            }

            return sequence;
            break;
        }
    }
    [Obsolete]
    private IEnumerable<IEnumerable<long[]>> Parse(string input, Func<string, long[]> getArr) => input.GetLines().Select(x => GetSensor(x, getArr));
    [Obsolete]
    private IEnumerable<long[]> GetSensor(string input, Func<string, long[]> getArr) => GenSequence(getArr(input));


    long NextSense(long[] numbers) => !numbers.Any() ? 0 : NextSense(GetDiff(numbers)) + numbers.Last();

    private long[] GetDiff(long[] numbers) => numbers.Zip(numbers.Skip(1)).Select(x => x.Second - x.First).ToArray();

}