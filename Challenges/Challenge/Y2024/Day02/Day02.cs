namespace Challenges.Challenge.Y2024.Day02;

[ChallengeName("Day 2: Red-Nosed Reports")]
public class Day02 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => 
        input.GetLines().Select(s => s.GetIntArr().ToList()).Count(IsSafe);

    private bool IsSafe(List<int> row)
    {
        var check = row.Zip(row.Skip(1), (a, b) => a - b).ToList();
        return check.All(v => Math.Abs(v) <= 3) && (check.All(v => v > 0) || check.All(v => v < 0));
    }
    
    
    public async Task<object?> TaskPartTwo(string input) => input.GetLines().Select(s => s.GetIntArr().ToList()).Count(IsSafeDampener);

    private bool IsSafeDampener(List<int> row)
    {
        if(IsSafe(row))
            return true;

        for (int i = 0; i < row.Count; i++)
        {
            var rowCopy = row.ToList();
            rowCopy.RemoveAt(i);
            
            if(IsSafe(rowCopy))
                return true;
        }
        return false;
    }

    
}