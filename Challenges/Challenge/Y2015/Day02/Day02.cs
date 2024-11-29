namespace Challenges.Challenge.Y2015.Day02;

[ChallengeName("Day 2: I Was Told There Would Be No Math")]
public class Day02 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => await Day02Part1(input.GetLines());

    public async Task<object?> TaskPartTwo(string input) => null;

    private record struct Box(int L, int W, int H)
    {
        public int W { get; set; } = W;
        public int L { get; set; } = L;
        public int H { get; set; } = H;
        public readonly IReadOnlyCollection<int> Areas = [L*W, W*H, H*L];
        public int Paper => Areas.Sum() * 2 + Areas.Min();
    }

    private async Task<int> Day02Part1(IEnumerable<string> input)
    {
        var boxes = input.Select(s =>
        {
            var splits = s.Split("x");
            return new Box(int.Parse(splits[0]), int.Parse(splits[1]), int.Parse(splits[2]));
        });
        
        return boxes.Sum(s => s.Paper);
    }
    

}
