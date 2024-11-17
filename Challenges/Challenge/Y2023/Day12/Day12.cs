namespace Challenges.Challenge.Y2023.Day12;

[ChallengeName("")]
public class Day12 : IChallenge
{
    public async Task<object> TaskPartOne(string input) => null;

    public async Task<object> TaskPartTwo(string input) => null;

    private int PossibleArrangements(string input)
    {
        var hotSprings = Parser(input);

        Dictionary<HotSpring, int> usage = [];
        foreach (var hotSpring in hotSprings)
        {
            Dictionary<int, int[]> possibilities = [];

            for (int i = 0; i < hotSpring.Records.Count(); i++)
            {
                List<int> pos = [];

                for (int j = 0; j < hotSpring.Conditions.Length; j++)
                {
                    
                }


                possibilities.Add(i, pos.ToArray());
            }
        }
        return usage.Count;
    }


    private IEnumerable<HotSpring> Parser(string input) =>
        input.GetLines().Select(s =>
        {
            var x = s.Split(" ");
            return new HotSpring(x[0], x[1].GetIntArr());
        });

    private record HotSpring(string Conditions, IEnumerable<int> Records);
}