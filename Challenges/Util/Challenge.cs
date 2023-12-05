using System.Reflection;

namespace Challenges.Util
{
    public interface IChallenge
    {
        public Task<object> TaskPartOne(string input);

        public Task<object> TaskPartTwo(string input);
    }

    public static class ChallengeExtensions
    {
        public static async Task<Result> CompleteChallenge(this IChallenge challenge, string input)
        {
            var partOneResult = await challenge.TaskPartOne(input);
            var partTwoResult = await challenge.TaskPartTwo(input);
            var name = challenge.GetName() ?? "Unknown";

            var result = new Result(name, partOneResult, partTwoResult, $"{Year(challenge)}/{Day(challenge):00}");
            return result;
        }

        public static string? GetName(this IChallenge challenge)
        {
            return (
                challenge
                    .GetType()
                    .GetCustomAttribute(typeof(ChallengeName)) as ChallengeName
            )?.Name;
        }

        public static IEnumerable<int?>? GetNumberEnumerableIncNull(this string input) => input.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        ).Select(i => !string.IsNullOrWhiteSpace(i)? (int?) int.Parse(i) : null);

        public static IEnumerable<string?>? GetLines(this string input) => input.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );

        public static string WorkingDir(int year)
        {
            return Path.Combine($"Y{year}");
        }

        public static string WorkingDir(int year, int day)
        {
            return Path.Combine(WorkingDir(year), $"Day{day:00}");
        }

        public static string WorkingDir(this IChallenge challenge)
        {
            return WorkingDir(challenge.Year(), challenge.Day());
        }
        
        public static int Year(Type t)
        {
            return int.Parse((t.FullName?.Split('.')[2])?[1..]!);
        }

        public static int Year(this IChallenge challenge)
        {
            return Year(challenge.GetType());
        }

        public static int Day(Type t)
        {
            return int.Parse((t.FullName?.Split('.')[3])?[3..]!);
        }

        public static int Day(this IChallenge challenge)
        {
            return Day(challenge.GetType());
        }

        public static bool IsChallenge(Type challenge, DateTime startDateTime, DateTime? endDateTime)
        {
            var date = new DateTime(Year(challenge), 12, Day(challenge));
            return date >= startDateTime && (
                (endDateTime is null)
                || (date <= endDateTime));

        }
    }
}