using System.Diagnostics;

namespace Challenges.Challenge.Y2021.Day01
{
    //Invoked Implicitly
    [ChallengeName("Day 01: Sonar Sweep")]
    public class Day01 : IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await DepthIncrease(GetNumberEnumerable(input));

        public async Task<object?> TaskPartTwo(string input) => await DepthIncreaseGroup(GetNumberEnumerable(input));

        // Count how many values increased compared to the previous
        private async Task<object> DepthIncrease(IEnumerable<int>? numbers)
            // ReSharper disable once PossibleMultipleEnumeration
        {
            if (numbers != null)
                return numbers.Zip(numbers.Skip(1)).Select(n => n).Count(sequence => sequence.First < sequence.Second);

            return "UNKNOWN";
        }


        private async Task<object> DepthIncreaseGroup(IEnumerable<int>? numbers) // Group values in three then count how many groups increased from previous
        {
            Debug.Assert(numbers != null, nameof(numbers) + " != null");
            var groups = numbers.Zip(numbers.Skip(1), numbers.Skip(2)).Select(sequence => sequence.First + sequence.Second + sequence.Third);

            return await DepthIncrease(groups);
        }

        private IEnumerable<int>? GetNumberEnumerable(string input) => input.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        ).Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse);
    }
}