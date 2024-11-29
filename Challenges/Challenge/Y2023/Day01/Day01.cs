namespace Challenges.Challenge.Y2023.Day01
{
    //Invoked Implicitly
    [ChallengeName("Day 1: Trebuchet?!")]
    public class Day01 : IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await onlyNumbers(input);
        public async Task<object?> TaskPartTwo(string input) => await wordsAndDigits(input);

        public async Task<int> onlyNumbers(string input)
        {
            var lines = input.GetLines();

            char[] testChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            return (from line in lines let minIndex = line.IndexOfAny(testChars) let maxIndex = line.LastIndexOfAny(testChars) select String.Concat(line[minIndex], line[maxIndex]) into i select int.Parse(i)).Sum();
        }


        public async Task<int> wordsAndDigits(string input)
        {
            var lines = input.GetLines();
            string[] digits = new string[]
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "zero",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine"
            };

            var sum = 0;
            foreach (var line in lines)
            {
            
                var minIndex = digits.Select(s => line.IndexOf(s)).Where(s => s > -1).Min();
                var maxIndex = digits.Select(s => line.LastIndexOf(s)).Max();

                char first;
                char last;

                if (char.IsDigit(line[minIndex]))
                {
                    first = line[minIndex];
                }
                else
                {
                    var s2 = line.Substring(minIndex);

                    var digit = digits.FirstOrDefault(s => s2.StartsWith(s));

                    first = digit switch
                    {
                        "one" => '1',
                        "two" => '2',
                        "three" => '3',
                        "four" => '4',
                        "five" => '5',
                        "six" => '6',
                        "seven" => '7',
                        "eight" => '8',
                        "nine" => '9',
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                if (char.IsDigit(line[maxIndex]))
                {
                    last = line[maxIndex];
                }
                else
                {
                    var s2 = line.Substring(maxIndex);

                    var digit = digits.FirstOrDefault(s => s2.StartsWith(s));

                    last = digit switch
                    {
                        "one" => '1',
                        "two" => '2',
                        "three" => '3',
                        "four" => '4',
                        "five" => '5',
                        "six" => '6',
                        "seven" => '7',
                        "eight" => '8',
                        "nine" => '9',
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }

                var i = String.Concat(first, last);

                sum += int.Parse(i);
            }

            return sum;
        }
    }
}
