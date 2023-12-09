
using Challenges;
using Newtonsoft.Json;
using System.Security.Principal;

var execPath = AppDomain.CurrentDomain.BaseDirectory;

var path =Path.Combine(execPath, @"..\..\..\");

var _runChallenges = new RunChallenges(path);

var result = await _runChallenges.RunAoCTask(DateTime.Today);

string json = JsonConvert.SerializeObject(result.FirstOrDefault(), Formatting.Indented);

Console.WriteLine(json);