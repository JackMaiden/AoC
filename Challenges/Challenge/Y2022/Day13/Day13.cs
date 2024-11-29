using System.Text.Json.Nodes;

namespace Challenges.Challenge.Y2022.Day13
{
    [ChallengeName("Day 13: Distress Signal")]
    public class Day13 : IChallenge
    {
        public async Task<object?> TaskPartOne(string input) => GetPackets(input)
            .Chunk(2)
            .Select((pair, index) => Compare(pair[0], pair[1]) < 0 ? index + 1 : 0)
            .Sum();

        public async Task<object?> TaskPartTwo(string input)
        {
            var divider = GetPackets("[[2]]\r\n[[6]]").ToList();
            var packets = GetPackets(input).Concat(divider).ToList();
            packets.Sort(Compare);
            return (packets.IndexOf(divider[0]) + 1) * (packets.IndexOf(divider[1]) + 1);
        }

        IEnumerable<JsonNode> GetPackets(string input) =>
            from line in input.Split("\r\n")
            where !string.IsNullOrEmpty(line)
            select JsonNode.Parse(line);

        int Compare(JsonNode nodeA, JsonNode nodeB)
        {
            if (nodeA is JsonValue && nodeB is JsonValue)
            {
                return (int)nodeA - (int)nodeB;
            }
            else
            {
                var arrayA = nodeA as JsonArray ?? new JsonArray((int)nodeA);
                var arrayB = nodeB as JsonArray ?? new JsonArray((int)nodeB);
                return Enumerable.Zip(arrayA, arrayB)
                    .Select(p => Compare(p.First, p.Second))
                    .FirstOrDefault(c => c != 0, arrayA.Count - arrayB.Count);
            }
        }
    }
}