namespace Challenges.Challenge.Y2022.Day11
{
    [ChallengeName("Day 11: Monkey in the Middle")]
    public class Day11: IChallenge
    {
        public async Task<object?> TaskPartOne(string input)
        {
            var monkeyDict = ParseMonkey(input);
            return await MonkeyInTheMiddle(monkeyDict, 20, x => x / 3);
        }

        public async Task<object?> TaskPartTwo(string input)
        {
            var monkeyDict = ParseMonkey(input);

            var mod = monkeyDict.Aggregate(1, (mod, monkey) => mod * monkey.Test); //value cannot be bigger than all tests multiplied together

            return await MonkeyInTheMiddle(monkeyDict, 10000, x => x % mod);
        }


        private List<Monkey> ParseMonkey(string input)
        {
            var monkeys = new List<Monkey>();

            var monkeysSplit = input.Split("\r\n\r\n");

            foreach (var monkey in monkeysSplit)
            {
                var monkeySplit = monkey.Split("\r\n").ToList();
                monkeySplit.Remove(monkeySplit[0]);
                monkeys.Add(new Monkey(monkeySplit));
            }

            return monkeys;
        }


        public async Task<object> MonkeyInTheMiddle(List<Monkey> monkeys, long rounds, Func<long,long> worryReduction)
        {
            for (var i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.CurrentItems.Any())
                    {
                        monkey.InspectedItems++;
                    
                        var item = monkey.CurrentItems.Dequeue();
                        item = monkey.DoOperation(item);
                        item = worryReduction(item);

                        var newMonkey = monkey.DoTest(item);

                        monkeys[newMonkey].CurrentItems.Enqueue(item);
                    }
                }
            }

            return monkeys.Select(x => x.InspectedItems).OrderByDescending(x => x).Take(2)
                .Aggregate(1L, (result, inspectedItems) => result * inspectedItems);
        }

    

        public class Monkey
        {
            public long InspectedItems { get; set; }
            public Queue<long> CurrentItems { get; set; }
            public string Operation { get; set; }
            public int Test { get; set; }
            public int TrueMonkey { get; set; }
            public int FalseMonkey { get; set; }

            /*
             * Pass in as this
             * Starting items: 79, 98
               Operation: new = old * 19
               Test: divisible by 23
               If true: throw to monkey 2
               If false: throw to monkey 3
             */
            public Monkey(List<string> line)
            {
                foreach (var partSplit in line.Select(part => part.Trim().Split(":").Select(x => x.Trim()).ToArray()))
                {
                    switch (partSplit[0])
                    {
                        case "Starting items":
                            InspectedItems = 0;
                            CurrentItems = new Queue<long>(partSplit[1].Split(", ").Select(long.Parse));
                            break;
                        case "Operation":
                            Operation = partSplit[1].Split("=").ToArray()[1];
                            break;
                        case "Test":
                            Test = int.Parse(partSplit[1].Split(" ").Last());
                            break;
                        case "If true":
                            TrueMonkey = int.Parse(partSplit[1].Split(" ").LastOrDefault() ?? "0");
                            break;
                        case "If false":
                            FalseMonkey = int.Parse(partSplit[1].Split(" ").LastOrDefault() ?? "0");
                            break;
                        default:
                            break;
                    }
                }
            }


            public long DoOperation(long item) //TODO: Could change Operation to be a Func
            {
                var mathSplit = Operation.Replace("old", item.ToString()).Split(" ", StringSplitOptions.RemoveEmptyEntries);

                return mathSplit[1] switch
                {
                    "+" => long.Parse(mathSplit[0]) + long.Parse(mathSplit[2]),
                    "*" => long.Parse(mathSplit[0]) * long.Parse(mathSplit[2]),
                    "-" => long.Parse(mathSplit[0]) - long.Parse(mathSplit[2]),
                    "/" => long.Parse(mathSplit[0]) / long.Parse(mathSplit[2]),
                    _ => 0
                };
            }

            public int DoTest(long item)
            {
                return item % Test == 0 ? TrueMonkey: FalseMonkey;
            }
        }


    }
}