namespace Challenges.Challenge.Y2021.Day04
{
    [ChallengeName("Day 04: Giant Squid")]
    public class Day04: IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await Bingo(input, true);

        public async Task<object?> TaskPartTwo(string input) => await Bingo(input, false);

        public async Task<object> Bingo(string input, bool best)
        {
            //lets split into sub blocks
            var bingoInput = input.Split("\r\n\r\n").ToList();

            //first row is input so lets separate
            var bingoOrder = bingoInput[0];
            bingoInput.Remove(bingoOrder);
            var bingoOrderList = bingoOrder.Split(",").Select(int.Parse).ToList();

            var bingoBoards = bingoInput.Select(s => new BingoBoard(
                s.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(a =>
                    a.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(a => new Cell(int.Parse(a)))
                        .ToArray()).ToArray())).ToList();

            BingoBoard lastWon = null;

            foreach (var bingoCall in bingoOrderList)
            {
                foreach (var bingoBoard in bingoBoards.ToArray())
                {
                    if (!bingoBoard.BingoCall(bingoCall)) continue;
                    if(best) return bingoBoard.BingoScore;
                    bingoBoards.Remove(bingoBoard);
                    lastWon = bingoBoard;
                }
            }

            return lastWon.BingoScore;
        }

        public struct Cell
        {
            public int Value { get; set; }
            public bool Called { get; set; }

            public Cell(int value, bool called = false)
            {
                Value = value;
                Called = called;
            }
        }

        ;

        public class BingoBoard
        {
            public int BingoScore { get; set; }

            public Cell[][] Board { get; set; }

            public BingoBoard(Cell[][] board)
            {
                Board = board;
            }

            public bool BingoCall(int number)
            {
                foreach (var row in Board)
                {
                    for (var j = 0; j < row.Length; j++)
                    {
                        if (row[j].Value == number) row[j].Called = true;
                    }
                }

                if (!HasWon(Board)) return false;

                BingoScore = number * Board.Select(a => a.Select(b => !b.Called ? b.Value : 0).Sum()).Sum();
                return true;

            }

            private static bool HasWon(Cell[][] bingoBoard)
            {
                for (var i = 0; i < bingoBoard.Length; i++)
                {
                    var lineCorrect = 0;
                    var columnCorrect = 0;
                    for (var j = 0; j < bingoBoard[i].Length; j++)
                    {
                        if (bingoBoard[i][j].Called)
                        {
                            lineCorrect++;
                        }
                        if (bingoBoard[j][i].Called)
                        {
                            columnCorrect++;
                        }
                    }

                    if (lineCorrect == bingoBoard.Length || columnCorrect == bingoBoard.Length)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}