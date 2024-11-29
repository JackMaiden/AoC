namespace Challenges.Challenge.Y2023.Day02
{
    //Invoked Implicitly
    [ChallengeName("Day 2: Cube Conundrum")]
    public class Day02: IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await SumValidGames(input.GetLines());


        public async Task<object?> TaskPartTwo(string input) => await SumPowerGames(input.GetLines());


        private const int red = 12;
        private const int green = 13;
        private const int blue = 14;

        private async Task<int> SumValidGames(IEnumerable<string?> games)
        {
            var parsedGames = Game.ParseGames(games);

            return (from parsedGame in parsedGames let valid = parsedGame.CubesInPlay.All(CheckPlayValid) where valid select parsedGame.Id).Sum();
        }

        private bool CheckPlayValid(Dictionary<string, int> play)
        {
            if (play.ContainsKey("red") && play["red"] > red) return false;
            if (play.ContainsKey("green") && play["green"] > green) return false;
            return !play.ContainsKey("blue") || play["blue"] <= blue;
        }


        private async Task<int> SumPowerGames(IEnumerable<string?> games)
        {
            return Game.ParseGames(games).Sum(GamePower);
        }

        private int GamePower(Game game)
        {
            var red = 0;
            var green = 0;
            var blue = 0;

            foreach (var play in game.CubesInPlay)
            {
                foreach (var cube in play)
                {
                    switch (cube.Key)
                    {
                        case "red":
                            red = red < cube.Value ? cube.Value : red;
                            break;
                        case "green":
                            green = green < cube.Value ? cube.Value : green;
                            break;
                        case "blue":
                            blue = blue < cube.Value ? cube.Value : blue;
                            break;
                    }
                }
            }

            return red * green * blue;
        }

        private class Game
        {
            public int Id { get; set; }
            public readonly IEnumerable<Dictionary<string, int>> CubesInPlay;

            private Game(int iD, IEnumerable<Dictionary<string, int>> cubes)
            {
                Id = iD;
                this.CubesInPlay = cubes;
            }

            public static IEnumerable<Game> ParseGames (IEnumerable<string> games)
            {
                List<Game> parsedGamesList = new();
                foreach (var game in games)
                {
                    var x = game.Split(":");
                    var id = int.Parse(x[0].Split(" ")[1]);

                    var gamePlays = x[1].Split(";");

                    List<Dictionary<string, int>> gamesList = new();

                    foreach (var gamePlay in gamePlays)
                    {
                        var cube = gamePlay.Split(",");
                    
                        Dictionary<string, int> cubes = cube.Select(cubePlay => cubePlay.Trim().Split(" ")).ToDictionary(a => a[1], a => int.Parse(a[0]));

                        gamesList.Add(cubes);
                    }

                    Game parsedGame = new(id, gamesList);
                    parsedGamesList.Add(parsedGame);
                }
                return parsedGamesList;
            }


        }

    }
}