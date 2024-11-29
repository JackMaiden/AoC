namespace Challenges.Challenge.Y2022.Day02
{
    [ChallengeName("Day 02: Rock Paper Scissors")]
    public class Day02: IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await RockPaperScissors(input, true);

        public async Task<object?> TaskPartTwo(string input) => await RockPaperScissors(input, false);

        public async Task<object> RockPaperScissors(string input, bool plan)
        {
            var rps = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();

            return rps.Select(x => Score(x, plan)).Sum();
        }

        public int Score(string[] hand, bool plan)
        {
#pragma warning disable CS8509
            var opponent = hand[0] switch
#pragma warning restore CS8509
            {
                "A" => Game.Rock,
                "B" => Game.Paper,
                "C" => Game.Scissors
            };

#pragma warning disable CS8509
            var myHand = hand[1] switch
#pragma warning restore CS8509
            {
#pragma warning disable CS8846
                "X" => plan switch
#pragma warning restore CS8846
                {
                    true => Game.Rock,
                    false when Equals(opponent, Game.Rock) => Game.Scissors,
                    false when Equals(opponent, Game.Paper) => Game.Rock,
                    false when Equals(opponent, Game.Scissors) => Game.Paper
                },
                "Y" => plan switch
                {
                    true => Game.Paper,
                    false => opponent
                },
#pragma warning disable CS8846
                "Z" => plan switch
#pragma warning restore CS8846
                {
                    true => Game.Scissors,
                    false when Equals(opponent, Game.Rock) => Game.Paper,
                    false when Equals(opponent, Game.Paper) => Game.Scissors,
                    false when Equals(opponent, Game.Scissors) => Game.Rock
                }
            };
            return myHand switch
            {
                var x when Equals(myHand, opponent) => Game.Draw.GameResult(myHand),
                Game.Rock when Equals(opponent, Game.Scissors) => Game.Win.GameResult(myHand),
                Game.Paper when Equals(opponent, Game.Rock) => Game.Win.GameResult(myHand),
                Game.Scissors when Equals(opponent, Game.Paper) => Game.Win.GameResult(myHand),
                _ => Game.Loss.GameResult(myHand)
            };
        }

        public enum Game
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3,
            Win = 6,
            Loss = 0,
            Draw = 3
        }

        
    }

    public static class GameExtensions
    {
        public static int GameResult(this Day02.Game a, Day02.Game b)
        {
            return (int)a + (int)b;
        }
    }
}