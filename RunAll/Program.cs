using Challenges;
using Newtonsoft.Json;
using System.Diagnostics;

var execPath = AppDomain.CurrentDomain.BaseDirectory;

var path = Path.Combine(execPath, @"..\..\..\");

var start = Stopwatch.GetTimestamp();

var runChallenges = new RunChallenges(path);

var result = await runChallenges.RunAoCTask(new DateTime(2015, 12, 01), new DateTime(2030, 12, 25));

var json = JsonConvert.SerializeObject(result,
    new JsonSerializerSettings { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
Console.WriteLine(json);

var duration = Stopwatch.GetElapsedTime(start);

Console.WriteLine($"Total Duration: {duration.TotalMilliseconds:N0}ms");