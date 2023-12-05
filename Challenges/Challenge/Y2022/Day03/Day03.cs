namespace Challenges.Challenge.Y2022.Day03
{
    [ChallengeName("Day 3: Rucksack Reorganization")]
    public class Day03 : IChallenge
    {
        public async Task<object> TaskPartOne(string input) => await BackpackSearcher(input);

        public async Task<object> TaskPartTwo(string input) => await GroupSearcher(input);

        public async Task<object> BackpackSearcher(string input)
        {
            var backpacks = input.Split("\r\n").ToList();
            var sumPriorities = 0;

            foreach (var backpack in backpacks)
            {
                var compartmentsSize = backpack.Length/2;

                var compartment1 = backpack[..compartmentsSize].ToCharArray();
                var compartment2 = backpack[compartmentsSize..].ToCharArray();

                foreach (var item in compartment1.GroupBy(x => x).Select(g => g.First()).ToList())
                {
                    if (compartment2.Contains(item))
                    {
                        sumPriorities += _priorities[item];
                    }
                }
            }
            return sumPriorities;
        }

        public async Task<object> GroupSearcher(string input)
        {
            var backpacks = input.Split("\r\n").ToList();
            var sumPriorities = 0;

            for (var i = 0; i < backpacks.Count; i = i + 3)
            {
                foreach (var item in backpacks[i].ToCharArray().GroupBy(x => x).Select(g => g.First()).ToList())
                {
                    if (backpacks[i+1].ToCharArray().Contains(item)&& backpacks[i + 2].ToCharArray().Contains(item))
                    {
                        sumPriorities += _priorities[item];
                    }
                }
            }
            return sumPriorities;
        }

        readonly Dictionary<char, int> _priorities = new Dictionary<char, int>();
        public Day03()
        {
            char a;
            int i;
            for (a = 'a', i = 1; a <= 'z'; a++, i++)
            {
                _priorities.Add(a, i);
            }
            for (a = 'A'; a <= 'Z'; a++, i++)
            {
                _priorities.Add(a, i);
            }
        }
    }
}