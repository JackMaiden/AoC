using System.Text.RegularExpressions;

namespace Challenges.Challenge.Y2022.Day05
{
    [ChallengeName("Day 5: Supply Stacks")]
    public class Day05 : IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => await ContainerStacks(input, false);

        public async Task<object?> TaskPartTwo(string input) => await ContainerStacks(input, true);

        public async Task<object> ContainerStacks(string input, bool stackMove)
        {

            var challengeSplit = input.Split("\r\n\r\n");

            var containers = challengeSplit[0].Split("\r\n").Reverse().ToList();

            var containerIds = containers[0];
            containers.Remove(containerIds);

            var stacks = containerIds.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(container => new Stack<char>()).ToList();

            foreach (var container in containers)
            {
                for (var i = 0; i < stacks.Count; i++)
                {
                    var stackNumber = containerIds.IndexOf((i + 1).ToString(), StringComparison.Ordinal);

                    if (!char.IsWhiteSpace(container[stackNumber]))
                    {
                        stacks[i].Push(container[stackNumber]);
                    }
                }
            } //Loop and add the containers to stacks

            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)", RegexOptions.IgnoreCase);

            foreach (var command in challengeSplit[1].Split("\r\n"))
            {
                var x = regex.Match(command);

                var count = int.Parse(x.Groups[1].Value);
                var from = int.Parse(x.Groups[2].Value) - 1;
                var to = int.Parse(x.Groups[3].Value) - 1;

                var moveQueue = new List<char>();

                for (var i = 0; i < count; i++)
                {
                    if (stacks[from].Count < 0) continue;
                    var container = stacks[from].Pop();
                    moveQueue.Add(container);
                }

                if (stackMove) moveQueue.Reverse();

                foreach (var container in moveQueue)
                {
                    stacks[to].Push(container);
                }
            }

            return stacks.Where(stack => stack.Count > 0).Aggregate("", (current, stack) => current + stack.Pop());

        }

    }
}