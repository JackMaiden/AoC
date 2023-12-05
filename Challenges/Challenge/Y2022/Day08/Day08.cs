namespace Challenges.Challenge.Y2022.Day08
{
    [ChallengeName("Day 8: Treetop Tree House")]
    public class Day08 : IChallenge
    {
        public async Task<object> TaskPartOne(string input) => await TreeGrid(input);


        public async Task<object> TaskPartTwo(string input) => await TreeHousePositions(input);


        public async Task<object> TreeGrid(string input)
        {
            var treeGrid = input.Split("\r\n").ToList();

            //first sum of outer trees
            var visibleTrees = (treeGrid.Count * 2) + (treeGrid.First().Length - 2) + (treeGrid.Last().Length - 2);

            for (var i = 1; i < treeGrid.Count-1; i++) //loop through inner Trees X
            {
                for (var j = 1; j < treeGrid.Count - 1; j++) //loop through inner Trees Y
                {

                    var treeHeight = int.Parse(treeGrid[i][j].ToString());

                    var xVisible =
                        !treeGrid[i].Where((x, index) => index < j && int.Parse(x.ToString()) >= treeHeight).Any() ||
                        !treeGrid[i].Where((x, index) => index > j && int.Parse(x.ToString()) >= treeHeight).Any();
                
                    var yVisible =
                        !treeGrid.Where((y, index) => index < i && int.Parse(y[j].ToString()) >= treeHeight).Any() ||
                        !treeGrid.Where((y, index) => index > i && int.Parse(y[j].ToString()) >= treeHeight).Any();

                    if (xVisible || yVisible) visibleTrees++;

                }
            }

            return visibleTrees;
        }

        public async Task<object> TreeHousePositions(string input)
        {
            var treeGrid = input.Split("\r\n").ToList();

            var visibleTreesPerPos = new List<int>();

            for (var i = 0; i < treeGrid.Count - 1; i++) //loop through inner Trees X
            {
                for (var j = 0; j < treeGrid.Count - 1; j++) //loop through inner Trees Y
                {

                    var treeHeight = int.Parse(treeGrid[i][j].ToString());

                    var top = 0;
                    for (var x = i-1; x >= 0; x--)
                    {
                        top++;
                        if(int.Parse(treeGrid[x][j].ToString()) >= treeHeight) break;
                    }

                    var bottom = 0;
                    for (var x = i+1; x < treeGrid.Count; x++)
                    {
                        bottom++;
                        if (int.Parse(treeGrid[x][j].ToString()) >= treeHeight) break;
                    }

                    var left = 0;
                    for (var x = j-1; x >= 0; x--)
                    {
                        left++;
                        if (int.Parse(treeGrid[i][x].ToString()) >= treeHeight) break;
                    }

                    var right = 0;
                    for (var x = j+1; x < treeGrid[i].Length; x++)
                    {
                        right++;
                        if (int.Parse(treeGrid[i][x].ToString()) >= treeHeight) break;
                    }

                    visibleTreesPerPos.Add(top * right * left * bottom);

                }
            }

            return visibleTreesPerPos.Max();
        }


    }
}