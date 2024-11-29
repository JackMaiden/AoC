namespace Challenges.Challenge.Y2015.Day01;

//Invoked Implicitly
[ChallengeName("Day 1: Not Quite Lisp")]
public class Day01 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => input.Count(c => c == '(') - input.Count(c => c == ')');
    public async Task<object?> TaskPartTwo(string input) => await FloorMap(input, -1);

    private async Task<int?> FloorMap(string input, int target)
    {
        int floor = 0;
        var result = input.Select((c, index) => new { c, index })
                                    .Select(x =>
                                        {
                                            floor += x.c switch
                                            {
                                                '(' => 1,
                                                ')' => -1,
                                                _ => 0
                                            };
                                            return new { floor, x.index };
                                        })
                                    .FirstOrDefault(x => x.floor == target);
        return result?.index;
    }

    
}