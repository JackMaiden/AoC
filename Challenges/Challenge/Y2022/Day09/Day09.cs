namespace Challenges.Challenge.Y2022.Day09
{
    [ChallengeName("Day 9: Rope Bridge")]
    public class Day09: IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await RopePhysics(input, 1);

        public async Task<object?> TaskPartTwo(string input) => await RopePhysics(input, 9);


        public async Task<object> RopePhysics(string input, int tailLength)
        {
            var moves = input.Split("\r\n");

            //head and tail start in same point
            var head = new Point(1, 1);
            var tail = new Point[tailLength];

            for (var i = 0; i < tail.Length; i++)
            {
                tail[i] = new Point(1, 1);
            }

            var uniquePoints = new HashSet<Point> { tail.Last() };

            foreach (var move in moves)
            {
                var moveSplit = move.Split(" ");
                var direction = moveSplit[0];

                for (var i = 0; i < int.Parse(moveSplit[1]); i++)
                {
                    switch (direction)
                    {
                        case "U":
                            head.Y++;
                            break;
                        case "D":
                            head.Y--;
                            break;
                        case "R":
                            head.X++;
                            break;
                        case "L":
                            head.X--;
                            break;
                        default:
                            break;
                    }
                
                    for (var j = 0; j < tail.Length; j++)
                    {
                        var headPoint = j == 0 ? head : tail[j-1];

                        var xDiff = Math.Abs(headPoint.X - tail[j].X);
                        var yDiff = Math.Abs(headPoint.Y - tail[j].Y);

                        if(xDiff <= 1 && yDiff <=1) continue; //touching
                        if (xDiff == 2 && yDiff == 0)
                        {
                            tail[j].X += headPoint.X > tail[j].X? 1 : -1;
                        }
                        else if (yDiff == 2 && xDiff == 0)
                        {
                            tail[j].Y += headPoint.Y > tail[j].Y? 1 : -1;
                        }
                        else
                        {
                            tail[j].X += headPoint.X > tail[j].X ? 1 : -1;
                            tail[j].Y += headPoint.Y > tail[j].Y ? 1 : -1;
                        }
                    }
                    uniquePoints.Add(tail.Last());
                }

            }
            return uniquePoints.Count;
        }

        public struct Point: IEquatable<Point>
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public bool Equals(Point other)
            {
                return X == other.X && Y == other.Y;
            }

        }
    }
}