namespace Challenges.Util
{
    public class Result(
        string challengeName,
        string? duration = null,
        object? examplePartOne = null,
        object? examplePartTwo = null,
        object? partOne = null,
        object? partTwo = null,
        string date = "")
    {
        public static implicit operator Result(string result) => new(result);

        public Result(Result result) : this(result.ChallengeName, null, result.ExamplePartOne, result.ExamplePartTwo, result.PartOne, result.PartTwo, result.Date)
        {
        }

        public string ChallengeName { get; set; } = challengeName;
        public object? ExamplePartOne { get; set; } = examplePartOne;
        public object? ExamplePartTwo { get; set; } = examplePartTwo;
        public object? PartOne { get; set; } = partOne;
        public object? PartTwo { get; set; } = partTwo;
        public string Date { get; set; } = date;
        public string? Duration { get; set; } = duration;
    }
}