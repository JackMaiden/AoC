namespace Challenges.Challenge.Y2022.Day01
{
    //Invoked Implicitly
    [ChallengeName("Day 01: Calorie Counting")]
    public class Day01 : IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await ElfCalories(input);
        public async Task<object?> TaskPartTwo(string input) => await ElfCaloriesTop3(input);

        [Obsolete]
        public async Task<object> CalculateCalories(IEnumerable<int?> challenge)
        {
            int elf = 1;
            int elfVal = 0;
            int highest = 0;
            int highestelf = 0;
            foreach (var challengeItem in challenge)
            {
                if (challengeItem != null)
                {
                    elfVal += challengeItem.Value;
                
                    continue;
                }

                if (elfVal > highest)
                {
                    highest = elfVal;
                    highestelf = elf;
                }

                elfVal = 0;
                elf++;

            }

            return highest;
        }
        [Obsolete]
        public async Task<object> CalculateCaloriesTopThree(IEnumerable<int?> challenge)
        {

            var elves = new List<int>();
            int elfVal = 0;
            foreach (var challengeItem in challenge)
            {
                if (challengeItem != null)
                {
                    elfVal += challengeItem.Value;

                    continue;
                }

                elves.Add(elfVal);
                elfVal = 0;
            }
            elves.Add(elfVal);


            return elves.OrderByDescending(i => i).Take(3).Sum(x => x);
        }


        public async Task<object> ElfCalories(string input)
        {
            return input.Split("\r\n\r\n").Select(e => e.Split("\r\n").Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).Sum()).Max();
        }

        public async Task<object> ElfCaloriesTop3(string input)
        {
            return input.Split("\r\n\r\n").Select(e => e.Split("\r\n").Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).Sum()).OrderByDescending(i => i).Take(3).Sum();
        }
    }
}
