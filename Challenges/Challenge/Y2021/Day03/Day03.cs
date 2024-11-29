namespace Challenges.Challenge.Y2021.Day03
{
    [ChallengeName("Day 03: Binary Diagnostic")]
    public class Day03: IChallenge
    {
        public async Task<object?> TaskPartOne(string diagnostics) => GetPowerConsumption(GetBinaryEnumerable(diagnostics));
        public async Task<object?> TaskPartTwo(string diagnostics) => GetOxygenLevels(GetBinaryEnumerable(diagnostics));

        private object GetPowerConsumption(IEnumerable<string> diagnostics)
        {
            return GammaRate(diagnostics) * EpsilonRate(diagnostics);
        }

        private object GetOxygenLevels(string[] diagnostics)
        {
            return OxygenGenerator(diagnostics) * CarbonScrubber(diagnostics);
        }

        private int ConvertToPowerInt(IEnumerable<string> diagnosticLines, Func<IEnumerable<string>, int, char> calculateCommonBitFunc)
        {
#pragma warning disable CS8602
            var lastBit = diagnosticLines.FirstOrDefault().Length;
#pragma warning restore CS8602

            var commonBits = "";
            for (var bitToCheck = 0; bitToCheck < lastBit; bitToCheck++)
            {
                commonBits += calculateCommonBitFunc(diagnosticLines, bitToCheck);
            }

            return Convert.ToInt32(commonBits, 2);
        }

        private int ConvertToO2Int(string[] diagnosticLines, Func<string[], int, char> calculateCommonBitFunc)
        {
            var lastBit = diagnosticLines[0].Length;
            for (var bitToCheck = 0; bitToCheck < lastBit && diagnosticLines.Length > 1; bitToCheck++)
            {
                var bit = calculateCommonBitFunc(diagnosticLines, bitToCheck);

                diagnosticLines = diagnosticLines.Where(diag => diag[bitToCheck] == bit).ToArray();
            }

            return Convert.ToInt32(diagnosticLines[0], 2);
        }

        private int GammaRate(IEnumerable<string> diagnosticLines) => ConvertToPowerInt(diagnosticLines, MostCommonBit);
        private int EpsilonRate(IEnumerable<string> diagnosticLines) => ConvertToPowerInt(diagnosticLines, LeastCommonBit);

        private int OxygenGenerator(string[] diagnosticLines) => ConvertToO2Int(diagnosticLines, MostCommonBit);
        private int CarbonScrubber(string[] diagnosticLines) => ConvertToO2Int(diagnosticLines, LeastCommonBit);

        private char MostCommonBit(IEnumerable<string> diagnosticLines, int bitToCheck) => 2 * diagnosticLines.Count(diag => diag[bitToCheck] == '1') >= diagnosticLines.Count() ?  '1': '0';
        private char LeastCommonBit(IEnumerable<string> diagnosticLines, int bitToCheck) => MostCommonBit(diagnosticLines, bitToCheck) == '1' ? '0' : '1';

        private string[] GetBinaryEnumerable(string input) =>
            input.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ).Where(s=> !string.IsNullOrWhiteSpace(s)).ToArray();

    }
}