using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace Challenges.Challenge.Y2015.Day04;

[ChallengeName("Day 4: The Ideal Stocking Stuffer")]
public class Day04 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => await FirstHashAsync(input, "00000");

    public async Task<object?> TaskPartTwo(string input) => await FirstHashAsync(input, "000000");

    private async Task<int> FirstHashAsync(string input, string prefix)
    {
        return await Task.Run(() =>
        {
            int result = int.MaxValue;
            object lockObject = new object();
            
            Parallel.ForEach(
                Numbers(), 
                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                MD5.Create, 
                (i, state, md5) => {
                    var hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(input + i));
                    var hash = string.Join("", hashBytes.Select(b => b.ToString("x2")));

                    if (hash.StartsWith(prefix)) {
                        lock (lockObject)
                        {
                            if (i < result)
                            {
                                result = i;
                                state.Stop();
                            }
                        }
                        
                    }
                    return md5;
                }, 
                md5 => md5.Dispose()
            );
            return result;
        });
    }

    IEnumerable<int> Numbers() {
        for (int i=0; ;i++) {
            yield return i;
        }
        // ReSharper disable once IteratorNeverReturns
    }
}