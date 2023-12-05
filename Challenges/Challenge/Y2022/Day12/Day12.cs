namespace Challenges.Challenge.Y2022.Day12
{
    [ChallengeName("Day 12: Hill Climbing Algorithm")]
    public class Day12: IChallenge
    {
        public async Task<object> TaskPartOne(string input)
        {
            return HillClimbingAlgorithm(input).Single(x=> x.Value.elv == Start).Value.dist;
        }

        public async Task<object> TaskPartTwo(string input)
        {
            return HillClimbingAlgorithm(input).Select(x=> x.Value).Where(x => GetElevation(x.elv) == LowestElevation).Min(x => x.dist);
        }

        private Dictionary<Coord, PointOfInterest> HillClimbingAlgorithm(string input)
        {

            var map = ParseMap(input);
            var goalPoint = map.Select(x => x).First(x => x.Value == Goal).Key;
            var queue = new Queue<Coord>();
            queue.Enqueue(goalPoint);

            var pointsOfInterest = new Dictionary<Coord, PointOfInterest>();
            pointsOfInterest.Add(goalPoint, new PointOfInterest(map[goalPoint],0));
            while (queue.Any())
            {
                var point  = queue.Dequeue();

                var neighbours = NeighbourCoords(point);

                foreach (var neighbour in neighbours)
                {
                    //check we havent visited before or queued to visit or point doesnt exist
                    if(pointsOfInterest.ContainsKey(neighbour) || queue.Contains(neighbour) || !map.ContainsKey(neighbour)) continue;

                    if (GetElevation(map[neighbour]) + 1 < (int)GetElevation(map[point])) continue;

                    queue.Enqueue(neighbour);
                    pointsOfInterest.Add(neighbour, new PointOfInterest(map[neighbour], pointsOfInterest[point].dist+1));
                }
            }


            return pointsOfInterest;
        }

        record struct Coord(int x, int y);

        record struct PointOfInterest(char elv, int dist);

        private const char Start = 'S';
        private const char Goal = 'E';
        private const char LowestElevation = 'a';
        private const char HighestElevation = 'z';

        private Dictionary<Coord, char> ParseMap(string input)
        {
            var lines = input.Split("\r\n");
            var dictionary = new Dictionary<Coord, char>();
            for (var y = 0 ; y < lines.Length; y++)
            for (var x = 0; x < lines[0].Length; x++)
            {
                dictionary.Add(new Coord(x, y), lines[y][x]);
            }

            return dictionary;
        }

        private Coord[] NeighbourCoords(Coord coord)
        {
            return new Coord[]
            {
                new Coord(coord.x + 1, coord.y),
                new Coord(coord.x - 1, coord.y),
                new Coord(coord.x, coord.y + 1),
                new Coord(coord.x, coord.y - 1)
            };
        }

        private char GetElevation(char c)
        {
            return c switch
            {
                Start => LowestElevation,
                Goal => HighestElevation,
                _ => c
            };
        }

    }
}