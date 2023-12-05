namespace Challenges.Challenge.Y2022.Day10
{
    [ChallengeName("Day 10: Cathode-Ray Tube")]
    public class Day10: IChallenge
    {
        public async Task<object> TaskPartOne(string input) => await CPUCycles(input, true);

        public async Task<object> TaskPartTwo(string input) => await CPUCycles(input, false);

        public async Task<object> CPUCycles(string input, bool sum)
        {
            var command = input.Split("\r\n").Select(x=>x.Split(" "));

            var cycles = new List<int>();
            var X = 1;

            foreach (var c in command)
            {
                switch (c[0])
                {
                    case "noop":
                        cycles.Add(X);
                        break;
                    case "addx":
                        for (var i = 0; i < 2; i++)
                        {
                            cycles.Add(X);
                        }
                        X += int.Parse(c[1]);
                        break;
                }
            }

            if(sum) return (20 * cycles[19]) + (60 * cycles[59]) + (100 * cycles[99]) + (140 * cycles[139]) + (180 * cycles[179]) + (220 * cycles[219]);

            const string blank = "                                        ";
            var displayRows = new[]
            {
                blank.ToCharArray(),
                blank.ToCharArray(),
                blank.ToCharArray(),
                blank.ToCharArray(),
                blank.ToCharArray(),
                blank.ToCharArray()
            };

            var cycle = 0;
            for (var rowIndex = 0; rowIndex < displayRows.Length; rowIndex++)
            {
                for (var displayPos = 0; displayPos < 40; displayPos++, cycle++)
                {
                    if (displayPos <= cycles[cycle] + 1 && displayPos >= cycles[cycle] - 1)
                    {
                        displayRows[rowIndex][displayPos] = '#';
                    }
                }
            }



            return displayRows.Select(x => new string(x));
        }
    }
}