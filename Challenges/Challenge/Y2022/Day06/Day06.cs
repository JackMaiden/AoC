namespace Challenges.Challenge.Y2022.Day06
{
    [ChallengeName("Day 6: Tuning Trouble")]
    public class Day06: IChallenge
    {
        public async Task<object> TaskPartOne(string input) => await StartOfXMarker(input,4);

        public async Task<object> TaskPartTwo(string input) => await StartOfXMarker(input, 14);


        //start of packet marker is a 4 char unique string
        //start of Message marker is a 14 char unique string

        private async Task<object> StartOfXMarker(string input, int distinct)
        {
            for (var i = 0; i < input.Length - distinct; i++)
            {
                if (input.Substring(i, distinct).ToCharArray().GroupBy(x => x).Any(g => g.Count() > 1)) continue;

                return i+ distinct;
            }

            return null;
        }
    }
}