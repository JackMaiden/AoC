namespace Challenges.Challenge.Y2015.Day02;

[ChallengeName("Day 2: I Was Told There Would Be No Math")]
public class Day02 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => (await ParseBoxes(input.GetLines())).Sum(s => s.Paper);

    public async Task<object?> TaskPartTwo(string input) => (await ParseBoxes(input.GetLines())).Sum(s => s.Ribon);

    private readonly record struct Box(int L, int W, int H)
    {
        private readonly int[] _areas = [L*W, W*H, H*L];
        public int Paper => _areas.Sum() * 2 + _areas.Min();
        
        private readonly int[] _perimeters = [2 * (L + W), 2 * (W + H), 2 * (H + L) ];
        public int Ribon => (L * W * H) + _perimeters.Min();
    }

    private async Task<IEnumerable<Box>> ParseBoxes(IEnumerable<string> input) => input.Select(s =>
    {
        var splits = s.Split("x");
        return new Box(int.Parse(splits[0]), int.Parse(splits[1]), int.Parse(splits[2]));
    });
}