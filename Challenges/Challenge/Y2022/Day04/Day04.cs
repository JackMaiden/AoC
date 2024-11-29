namespace Challenges.Challenge.Y2022.Day04
{
    [ChallengeName("Day 4: Camp Cleanup")]
    public class Day04 :IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await FullyOverlappingPairs(input);

        public async Task<object?> TaskPartTwo(string input) => await OverlappingPairs(input);

        public async Task<object> FullyOverlappingPairs(string input)
        {
            var pairs = input.Split("\r\n").ToList();

            var overlap = 0;

            foreach (var pair in pairs)
            {
                var sections = pair.Split(",").Select(s => s.Split("-").Select(int.Parse).ToArray()).ToArray();

                //sections[0][0] elf 1 section 1
                //sections[0][1] elf 1 section 2
                //sections[1][0] elf 2 section 1
                //sections[1][1] elf 2 section 2

                if ((sections[0][0] <= sections[1][0] && sections[0][1] >= sections[1][1])
                    || (sections[0][0] >= sections[1][0] && sections[0][1] <= sections[1][1]))
                {
                    overlap++;
                }
            }

            return overlap;
        }


        public async Task<object> OverlappingPairs(string input)
        {
            var pairs = input.Split("\r\n").ToList();

            var overlap = 0;

            foreach (var pair in pairs)
            {
                var sections = pair.Split(",").Select(s => s.Split("-").Select(int.Parse).ToArray()).ToArray();

                var elf1 = Enumerable.Range(sections[0][0], sections[0][1]-sections[0][0]+1).ToList();
                var elf2 = Enumerable.Range(sections[1][0], sections[1][1]-sections[1][0]+1).ToList();

                if (elf1.Intersect(elf2).Any())
                {
                    overlap++;
                }
            }

            return overlap;
        }
    }
}