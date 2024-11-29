﻿using Challenges;
using Newtonsoft.Json;
using System.Diagnostics;

var execPath = AppDomain.CurrentDomain.BaseDirectory;

var path = Path.Combine(execPath, @"..\..\..\");

var stopWatch = new Stopwatch();
stopWatch.Start();

var _runChallenges = new RunChallenges(path);

var result = await _runChallenges.RunAoCTask(new DateTime(2021, 12, 01), new DateTime(2030, 12, 25));

var json = JsonConvert.SerializeObject(result,
    new JsonSerializerSettings { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });

Console.WriteLine(json);

stopWatch.Stop();

var duration = stopWatch.Elapsed;

Console.WriteLine($"Total Duration: {duration.TotalMilliseconds:N0}ms");