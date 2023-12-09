using System.Reflection;
using Newtonsoft.Json;
using static System.IO.File;

namespace Challenges
{
    public class RunChallenges
    {
        private readonly string _environmentPath;

        public RunChallenges(string environmentPath)
        {
            _environmentPath = environmentPath;
        }

        public async Task<IEnumerable<Result>> RunAoCTask(DateTime? startDateTime, DateTime? endDateTime = null)
        {
            startDateTime ??= DateTime.Today;
            if (startDateTime.Value.Month != 12 || startDateTime.Value.Day > 25)
                return new Result[] { "You can only play this between 1-25th December" };
            if (startDateTime.Value.Year < 2015 || startDateTime.Value > DateTime.Today)
                return new Result[] { "This project does not support this challenge" };

            var implementedChallengeTypes = Assembly.GetExecutingAssembly()!.GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && typeof(IChallenge).IsAssignableFrom(t))
                .OrderBy(t => t.FullName)
                .ToArray();
            var implementedChallenges = GetChallenges(implementedChallengeTypes);

            var results = new List<Result>();

            foreach (var challenge in implementedChallenges.Where(c => ChallengeExtensions.IsChallenge(c.GetType(), startDateTime.Value, endDateTime)))
            {
                var result = await Process(challenge);

                results.Add(result);
            }

            if (results.Count == 0) results.Add(new Result("Challenge Not Implemented Yet"));

            return results;
        }

        private async Task<Result> Process(IChallenge? challenge)
        {
            if (challenge == null) return "Challenge Not Implemented Yet";

            var challengePath = Path.Combine(_environmentPath, "..\\Challenges", "Challenge", challenge.WorkingDir());

            var exampleInput = "";
            await using (var stream = typeof(RunChallenges).Assembly.GetManifestResourceStream($"Challenges.Challenge.{challenge.WorkingDir().Replace("\\", ".")}.example.input"))
                if (stream is not null)
                    using (var reader = new StreamReader(stream))
                        exampleInput = await reader.ReadToEndAsync();

            if (!string.IsNullOrEmpty(exampleInput))
            {
                var exampleResult = await challenge.CompleteChallenge(exampleInput);
                await WriteAllLinesAsync(Path.Combine(challengePath, "example.output"), new[] { JsonConvert.SerializeObject(exampleResult, Formatting.Indented) ?? "" });
            }
            else
            {
                exampleInput = "";
                await using (var stream = typeof(RunChallenges).Assembly.GetManifestResourceStream($"Challenges.Challenge.{challenge.WorkingDir().Replace("\\", ".")}.example.input.1"))
                    if (stream is not null)
                        using (var reader = new StreamReader(stream))
                            exampleInput = await reader.ReadToEndAsync();

                if (!string.IsNullOrEmpty(exampleInput))
                {
                    var exampleResult = await challenge.CompleteChallenge(exampleInput, true);

                    exampleInput = "";
                    await using (var stream = typeof(RunChallenges).Assembly.GetManifestResourceStream($"Challenges.Challenge.{challenge.WorkingDir().Replace("\\", ".")}.example.input.2"))
                        if (stream is not null)
                            using (var reader = new StreamReader(stream))
                                exampleInput = await reader.ReadToEndAsync();

                    if (!string.IsNullOrEmpty(exampleInput))
                    {
                        exampleResult = await challenge.CompleteChallenge(exampleInput, false, exampleResult.PartOne);
                    }

                    await WriteAllLinesAsync(Path.Combine(challengePath, "example.output"),
                        new[] { JsonConvert.SerializeObject(exampleResult, Formatting.Indented) ?? "" });
                }
            }

            var input = "";
            await using (var stream = typeof(RunChallenges).Assembly.GetManifestResourceStream($"Challenges.Challenge.{challenge.WorkingDir().Replace("\\", ".")}.data.input"))
                if(stream is not null)
                    using (var reader = new StreamReader(stream))
                        input = await reader.ReadToEndAsync();

            Result result;
            if (!string.IsNullOrEmpty(input))
                result = await challenge.CompleteChallenge(input);
            else
                result = new Result(challenge.GetName(), new TimeSpan(0, 0, 0), null, null,
                    $"{challenge.Year()}/{challenge.Day():00}");
            await WriteAllLinesAsync(Path.Combine(challengePath, "result.output"), new[] { JsonConvert.SerializeObject(result, Formatting.Indented) ?? "" });

            return result;
        }

        private static IEnumerable<IChallenge?> GetChallenges(IEnumerable<Type> challenges)
        {
            return challenges.Select(c => Activator.CreateInstance(c) as IChallenge).ToArray();
        }
    }
}