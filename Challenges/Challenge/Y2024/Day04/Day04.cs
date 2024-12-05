namespace Challenges.Challenge.Y2024.Day04;

[ChallengeName("Day 4: Ceres Search")]
public class Day04 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => WordSearch(input.GetLines().Select(l => l.ToCharArray()).ToArray());

    public async Task<object?> TaskPartTwo(string input) => XSearch(input.GetLines().Select(l => l.ToCharArray()).ToArray());

    private int WordSearch(char[][] grid)
    {
        int counter = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (j + 3 < grid[i].Length)
                {
                    counter = grid[i][j] == 'X' && grid[i][j+1] == 'M'  && grid[i][j+2] =='A'  && grid[i][j+3] == 'S' ? counter + 1 : counter;
                }
                
                if (i + 3 < grid.Length)
                {
                    counter = grid[i][j] == 'X' && grid[i+1][j] == 'M'  && grid[i+2][j] =='A'  && grid[i+3][j] == 'S' ? counter + 1 : counter;
                }
                
                if (i + 3 < grid.Length && j + 3 < grid[i].Length)
                {
                    counter = grid[i][j] == 'X' && grid[i+1][j+1] == 'M'  && grid[i+2][j+2] =='A'  && grid[i+3][j+3] == 'S' ? counter + 1 : counter;
                }
                
                if (j - 3 >= 0)
                {
                    counter = grid[i][j] == 'X' && grid[i][j-1] == 'M'  && grid[i][j-2] =='A'  && grid[i][j-3] == 'S' ? counter + 1 : counter;
                }
                
                if (i - 3 >= 0)
                {
                    counter = grid[i][j] == 'X' && grid[i-1][j] == 'M'  && grid[i-2][j] =='A'  && grid[i-3][j] == 'S' ? counter + 1 : counter;
                }
                
                if (i - 3 >= 0 && j - 3 >= 0)
                {
                    counter = grid[i][j] == 'X' && grid[i-1][j-1] == 'M'  && grid[i-2][j-2] =='A'  && grid[i-3][j-3] == 'S' ? counter + 1 : counter;
                }
                
                if (i + 3 < grid.Length && j - 3 >=0)
                {
                    counter = grid[i][j] == 'X' && grid[i+1][j-1] == 'M'  && grid[i+2][j-2] =='A'  && grid[i+3][j-3] == 'S' ? counter + 1 : counter;
                }
                
                if (i - 3 >= 0 && j + 3 < grid[i].Length)
                {
                    counter = grid[i][j] == 'X' && grid[i-1][j+1] == 'M'  && grid[i-2][j+2] =='A'  && grid[i-3][j+3] == 'S' ? counter + 1 : counter;
                }
            }
        }
        return counter;
    }
    
    private int XSearch(char[][] grid)
    {
        int counter = 0;
        for (int i = 1; i < grid.Length-1; i++)
        {
            for (int j = 1; j < grid[i].Length-1; j++)
            {
                if(grid[i][j] != 'A') continue;

                if (
                    (
                        (grid[i - 1][j - 1] == 'M' && grid[i + 1][j + 1] == 'S') 
                        || (grid[i - 1][j - 1] == 'S' && grid[i + 1][j + 1] == 'M')
                    ) 
                    &&
                    (
                        (grid[i + 1][j - 1] == 'M' && grid[i - 1][j + 1] == 'S') 
                        || (grid[i + 1][j - 1] == 'S' && grid[i - 1][j + 1] == 'M')
                    )
                )
                {
                    counter++;
                }
            }
        }
        return counter;
    }
    
    
}