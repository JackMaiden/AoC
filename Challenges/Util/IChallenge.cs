namespace Challenges.Util;

public interface IChallenge
{
    public Task<object?> TaskPartOne(string input);

    public Task<object?> TaskPartTwo(string input);
}