using System.Diagnostics;
using System.Reflection;

namespace Challenges.Util;

public static partial class ChallengeExtensions
{
    public static async Task<Result> CompleteChallenge(this IChallenge challenge, string input, string example1, string example2)
    {
        Stopwatch stopWatch = new();
        stopWatch.Start();
        var examplePartOneResult = await challenge.TaskPartOne(example1);
        var examplePartTwoResult = await challenge.TaskPartTwo(example2);
        var partOneResult = await challenge.TaskPartOne(input);
        var partTwoResult = await challenge.TaskPartTwo(input);
        stopWatch.Stop();

        var duration = stopWatch.Elapsed;
        var name = challenge.GetName();

        var result = new Result(name, duration, examplePartOneResult, examplePartTwoResult, partOneResult, partTwoResult, $"{Year(challenge)}/{Day(challenge):00}");
        return result;
    }

    public static string GetName(this IChallenge challenge)
    {
        return (
            challenge
                .GetType()
                .GetCustomAttribute(typeof(ChallengeName)) as ChallengeName
        )?.Name ?? "Unknown";
    }

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
