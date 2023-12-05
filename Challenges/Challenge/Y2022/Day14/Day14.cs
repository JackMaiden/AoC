using System.Numerics;

namespace Challenges.Challenge.Y2022.Day14
{
    [ChallengeName("Day 14: Regolith Reservoir")]
    public class Day14 : IChallenge
    {
        public async Task<object> TaskPartOne(string input)
        {
            return new Cave(input, false).SpawnSand();
        }

        public async Task<object> TaskPartTwo(string input)
        {
            return new Cave(input, true).SpawnSand();
        }

        class Cave
        {
            bool hasFloor;

            Dictionary<Complex, char> map;
            int AbyssLevel;

            public Cave(string input, bool hasFloor)
            {
                this.hasFloor = hasFloor;
                this.map = new Dictionary<Complex, char>();

                foreach (var line in input.GetLines())
                {
                    var rockFaces = line.Split(" -> ").Select(x => new Complex(int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1]))).ToList();
                
                    for (var i = 0; i < rockFaces.Count - 1; i++)
                    {
                        AddRocks(rockFaces[i], rockFaces[i + 1]);
                    }
                }

                this.AbyssLevel = (int)this.map.Keys.Select(pos => pos.Imaginary).Max()+1;
            }

            private void AddRocks(Complex from, Complex to)
            {
                var dir = new Complex(
                    Math.Sign(to.Real - from.Real),
                    Math.Sign(to.Imaginary - from.Imaginary)
                );

                for (var pos = from; pos != to + dir; pos += dir)
                {
                    map[pos] = '#';
                }
            }

            public int SpawnSand()
            {
                Complex sandSpawnPoint = new Complex(500, 0);

                while (true)
                {
                    var location = SandSimulator(sandSpawnPoint);

                    if (!hasFloor && location.Imaginary >= AbyssLevel)
                    {
                        break;
                    }

                    if (map.ContainsKey(location))
                    {
                        break;
                    }

                    map[location] = 'o';
                }

                return map.Values.Count(x => x == 'o');
            }

            Complex SandSimulator(Complex sand)
            {
                var down = new Complex(0, 1);
                var diagonalLeft = new Complex(-1, 1);
                var diagonalRight = new Complex(1, 1);

                while (sand.Imaginary < AbyssLevel)
                {
                    if (!map.ContainsKey(sand + down))
                    {
                        sand += down;
                    }
                    else if (!map.ContainsKey(sand + diagonalLeft))
                    {
                        sand += diagonalLeft;
                    }
                    else if (!map.ContainsKey(sand + diagonalRight))
                    {
                        sand += diagonalRight;
                    }
                    else
                    {
                        break;
                    }
                }
                return sand;
            }
        }
    }
}
