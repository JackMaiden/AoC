
using Challenges;
using Newtonsoft.Json;

var execPath = AppDomain.CurrentDomain.BaseDirectory;

var path =Path.Combine(execPath, @"..\..\..\");

var _runChallenges = new RunChallenges(path);

var result = await _runChallenges.RunAoCTask(DateTime.Today);

var json = JsonConvert.SerializeObject(result.FirstOrDefault(),
    new JsonSerializerSettings { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });

Console.WriteLine(json);