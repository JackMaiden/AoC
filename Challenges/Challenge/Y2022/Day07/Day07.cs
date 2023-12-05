namespace Challenges.Challenge.Y2022.Day07
{
    [ChallengeName("Day 7: No Space Left On Device")]
    public class Day07: IChallenge
    {
        public async Task<object> TaskPartOne(string input)
        {
            filesystem(input);
            return Directories.Select(x => x.Value.Size()).Where(x => x <= 100000).Sum();
        }

        public async Task<object> TaskPartTwo(string input)
        {
            filesystem(input);
            var totalDisk = 70000000;
            var spaceNeeded = 30000000;
            var usedDisk = Directories["/"].Size();

            var minToDelete = spaceNeeded - (totalDisk - usedDisk);

            return Directories.Select(x => x.Value.Size()).Where(x => x > minToDelete).Min();
        }
    
        void filesystem(string input)
        {
            Directories.Clear();

            var commands = input.Split("\r\n");

            Directories.Add("/", new Dir("/")); //add root

            var currentDir = new Stack<string>();

            foreach (var command in commands)
            {
                var splitCommand = command.Split(' ');

                var cwdName = currentDir.FirstOrDefault() ?? "";
                _ = Directories.TryGetValue(cwdName, out var cwd);

                switch (splitCommand[0])
                {
                    case "$":
                        if (splitCommand[1] == "cd" && splitCommand[2] != "..")
                        {
                            var path = cwd == null ? splitCommand[2] : $"{cwd?.path}/{splitCommand[2]}";
                            currentDir.Push(path);
                        }
                        if (splitCommand[1] == "cd" && splitCommand[2] == "..")
                            currentDir.TryPop(out _);
                        break;
                    case "dir":
                        cwd?.AddDir(splitCommand[1]);
                        break;
                    default:
                        if (int.TryParse(splitCommand[0], out int size))
                            cwd?.AddFile(size);
                        break;
                }
            }
        }


        static readonly Dictionary<string, Dir> Directories = new Dictionary<string, Dir>();

        private class Dir
        {
            public string path { get; set; }
            List<string> innerDirectories { get; set; }

            int fileSizes { get; set; }

            public Dir(string path)
            {
                innerDirectories = new List<string>();
                fileSizes = 0;
                this.path = path;
            }

            public void AddFile(int size)
            {
                fileSizes += size;
            }

            public void AddDir(string? dir)
            {
                var newCwd = $"{path}/{dir}" ?? dir;
                innerDirectories.Add(newCwd);
                Directories.Add(newCwd, new Dir(newCwd));
            }

            public int Size()
            {
                return fileSizes + innerDirectories.Sum(dir => Directories[dir].Size());
            }
        }
    }
}

