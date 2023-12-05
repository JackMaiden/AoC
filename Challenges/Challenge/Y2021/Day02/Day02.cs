namespace Challenges.Challenge.Y2021.Day02
{
    [ChallengeName("Day 02: Dive!")]
    public class Day02 : IChallenge
    {
        public async Task<object> TaskPartOne(string input) => await GetFinalArea(GetInput(input));

        public async Task<object> TaskPartTwo(string input) => await GetFinalAreaCourse(GetInput(input));

        private static async Task<object> GetFinalArea(IEnumerable<Input> commands)
        {

            var t = commands.Count();
            var point = new CoordsXY(0, 0);
            point = commands.Aggregate(point, (current, command) => command.Command switch
            {
                "up" => current with { Y = current.Y + -command.Amount },
                "down" => current with { Y = current.Y + command.Amount },
                "forward" => current with { X = current.X + command.Amount },
                _ => current
            });

            return point.X * point.Y;
        }

        private static async Task<object> GetFinalAreaCourse(IEnumerable<Input> commands)
        {
            var point = new CoordsXYA(0, 0, 0);
            point = commands.Aggregate(point, (current, command) => command.Command switch
            {
                "up" => current with { Aim = current.Aim + -command.Amount },
                "down" => current with { Aim = current.Aim + command.Amount },
                "forward" => current with
                {
                    X = current.X + command.Amount,
                    Y = current.Y + command.Amount * current.Aim
                },
                _ => current
            });

            return point.X * point.Y;
        }

        private IEnumerable<Input> GetInput(string input) =>
            input.Split("\n").Where(str => !string.IsNullOrWhiteSpace(str)).Select(str =>
            {
                var commands = str.Split(" ");

                return new Input(commands[0], int.Parse(commands[1]));
            });


        private record Input(string Command, int Amount);

        private record CoordsXY(int X, int Y);

        private record CoordsXYA(int X, int Y, int Aim);

    }
}