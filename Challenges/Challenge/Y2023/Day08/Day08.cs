namespace Challenges.Challenge.Y2023.Day08;

[ChallengeName("Day 8: Haunted Wasteland")]
public partial class Day08 : IChallenge
{
    public async Task<object> TaskPartOne(string input) => stepsForPeople(input);

    public async Task<object> TaskPartTwo(string input) => stepsForGhosts(input);


    public int stepsForPeople(string input)
    {
        var start = "AAA";
        var end = "ZZZ";
        var lines = input.GetLines();

        var instructions = lines.First().ToCharArray();
        var tree = ParseTree(lines.Skip(2).ToArray());


        int steps = 0;
        string currentStep = start;
        for (var c = 0; c < instructions.Length; c++)
        {
            if (currentStep == end)
                return steps;

            currentStep = instructions[c] switch
            {
                'R' => tree[currentStep].right,
                'L' => tree[currentStep].left,
                _ => currentStep
            };


            var next = c + 1;
            if (next == instructions.Length)
            {
                c = -1;
            }

            steps++;
        }

        return -1;
    }

    public long stepsForGhosts(string input)
    {
        var lines = input.GetLines();

        var instructions = lines.First().ToCharArray();
        var tree = ParseTree(lines.Skip(2).ToArray());


        long result = 1L;
        string[] stepsToMap = tree.Where(s => s.Key.EndsWith("A")).Select(s => s.Key).ToArray();


        foreach (var step in stepsToMap)
        {
            var currentStep = step;
            long steps = 0;
            for (var c = 0; c < instructions.Length; c++)
            {
                if (currentStep.EndsWith("Z"))
                    break;

                currentStep = instructions[c] switch
                {
                    'R' => tree[currentStep].right,
                    'L' => tree[currentStep].left,
                    _ => currentStep
                };


                var next = c + 1;
                if (next == instructions.Length)
                {
                    c = -1;
                }

                steps++;
            }

            result = Lcm(result, steps);

        }

        

        return result;
    }

    Dictionary<string, Graph> ParseTree(string[] input) => input.ToDictionary(s => s.Split(" = ")[0], s => ParseGraph(s.Split(" = ")[1]));

    private Graph ParseGraph(string input)
    {
        var nodes = WordRegex().Matches(input).Select(s => s.Value).ToArray();
        return new Graph(nodes[0], nodes[1]);
    }

    private record Graph(string left, string right);

    [System.Text.RegularExpressions.GeneratedRegex(@"[A-Z]+")]
    private static partial System.Text.RegularExpressions.Regex WordRegex();

    long Lcm(long a, long b) => a * b / Gcd(a, b);
    long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);
}