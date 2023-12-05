namespace Challenges.Challenge.Y2023.Day03
{
    //Invoked Implicitly
    [ChallengeName("Day 3: Gear Ratios")]
    public class Day03: IChallenge
    {
        public async Task<object> TaskPartOne(string input) =>  PartNumbers(ParseInput(input)).Select(x=>x.Part).Sum();

        public async Task<object> TaskPartTwo(string input) => GearRatios(ParseInput(input)).Sum();


        public IEnumerable<PartNumber> PartNumbers(char[][] schematic)
        {
            var yLength = schematic.Length;
            var xLength = schematic[0].Length;

            List<PartNumber> partNumber = new();

            StringBuilder number = new();
            bool isPart = false;
            List<Coord> coords = new();

            for (var y = 0; y < yLength; y++)
            {
                if (!schematic[y].Any(char.IsDigit)) continue;

                for (var x = 0; x < xLength; x++)
                {
                    if (x == 0)
                    {

                        if (isPart)
                        {
                            var part = new PartNumber(coords, int.Parse(number.ToString()));
                            partNumber.Add(part);
                        }

                        number.Clear();
                        isPart = false;
                        coords = new();
                    }

                    char check = schematic[y][x];

                    if (char.IsDigit(check))
                    {
                        if (!isPart) isPart = IsPartNumber(schematic, x, y);

                        number.Append(check);
                        coords.Add(new Coord(x, y));
                    }
                    else
                    {
                        if (isPart)
                        {
                            var part = new PartNumber(coords, int.Parse(number.ToString()));
                            partNumber.Add(part);
                        }

                        number.Clear();
                        isPart = false;
                        coords = new();
                    }
                }
            }
            return partNumber;
        }


        public IEnumerable<int> GearRatios(char[][] schematic)
        {
            var partNumber = PartNumbers(schematic);

            var yLength = schematic.Length;
            var xLength = schematic[0].Length;

            List<int> gearRatio = new();

            for (var y = 0; y < yLength; y++)
            {
                if (!schematic[y].Contains('*')) continue;

                for (var x = 0; x < xLength; x++)
                {
                    char check = schematic[y][x];

                    if (check == '*')
                    {
                        bool canCheckNorth = y > 0;
                        bool canCheckSouth = y < yLength - 1;
                        bool canCheckEast = x < xLength - 1;
                        bool canCheckWest = x > 0;

                        List<Coord> coordList = new();

                        if (canCheckNorth) coordList.Add(new Coord(x + N.X, y + N.Y));
                        if (canCheckSouth) coordList.Add(new Coord(x + S.X, y + S.Y));
                        if (canCheckEast) coordList.Add(new Coord(x + E.X, y + E.Y));
                        if (canCheckWest) coordList.Add(new Coord(x + W.X, y + W.Y));
                        if (canCheckNorth && canCheckEast) coordList.Add(new Coord(x + NE.X, y + NE.Y));
                        if (canCheckNorth && canCheckWest) coordList.Add(new Coord(x + NW.X, y + NW.Y));
                        if (canCheckSouth && canCheckEast) coordList.Add(new Coord(x + SE.X, y + SE.Y));
                        if (canCheckSouth && canCheckWest) coordList.Add(new Coord(x + SW.X, y + SW.Y));


                        var partsNextToo = partNumber.Where(x => x.Coords.Intersect(coordList).Any());

                        if (partsNextToo.Count() == 2)
                        {
                            gearRatio.Add(partsNextToo.Select(s => s.Part).Aggregate(1, (a,b) => a * b));
                        }
                    }
                }
            }

            return gearRatio;
        }

        public bool IsPartNumber(char[][] schematic, int x, int y)
        {
            var yLength = schematic.Length;
            var xLength = schematic[0].Length;

            bool canCheckNorth = y > 0;
            bool canCheckSouth = y < yLength - 1;
            bool canCheckEast = x < xLength - 1;
            bool canCheckWest = x > 0;

            if (canCheckNorth && isSymbol(schematic[y + N.Y][x + N.X])) return true;
            if (canCheckSouth && isSymbol(schematic[y + S.Y][x + S.X])) return true;
            if (canCheckEast && isSymbol(schematic[y + E.Y][x + E.X])) return true;
            if (canCheckWest && isSymbol(schematic[y + W.Y][x + W.X])) return true;
            if (canCheckNorth && canCheckEast && isSymbol(schematic[y + NE.Y][x + NE.X])) return true;
            if (canCheckNorth && canCheckWest && isSymbol(schematic[y + NW.Y][x + NW.X])) return true;
            if (canCheckSouth && canCheckEast && isSymbol(schematic[y + SE.Y][x + SE.X])) return true;
            if (canCheckSouth && canCheckWest && isSymbol(schematic[y + SW.Y][x + SW.X])) return true;

            return false;
        }

        public bool isSymbol(char check)
        {
            return !char.IsDigit(check) && check != '.';
        }

        public char[][] ParseInput(string input)
        {
            return input.GetLines().Select(x => x.ToCharArray()).ToArray();
        }

        public record Coord
        {
            public int X;
            public int Y;

            public Coord(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        private Coord N = new(0, -1);
        private Coord NE = new(1, -1);
        private Coord NW = new(-1, -1);

        private Coord S = new(0, 1);
        private Coord SE = new(1, 1);
        private Coord SW = new(-1, 1);

        private Coord E = new(1, 0);
        private Coord W = new(-1, 0);


        public class PartNumber
        {
            public PartNumber(IEnumerable<Coord> coords, int part)
            {
                Coords = coords;
                Part = part;
            }

            public int Part { get; set; }
            public IEnumerable<Coord> Coords { get; set; }



        }
    }
}