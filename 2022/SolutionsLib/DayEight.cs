namespace SolutionLib;
using System.IO;
using System.Linq;
using Utils;

public class DayEight
{
    public List<List<int>> grid;

    public DayEight()
    {
        grid = new List<List<int>>();
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-eight-test.txt";
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-eight-puzzle.txt";
        string[] input = Utilities.ReadInput(path);

        // Convert input into a 2d array
        foreach (string item in input)
        {
            grid.Add(item.Select(n => Convert.ToInt32(Char.GetNumericValue(n))).ToList());
        }

        int invisableTrees = 0;
        int highestScore = 0;

        // Row
        for (int r = 1; r < this.grid.Count - 1; r++)
        {
            // Column
            for (int c = 1; c < this.grid[r].Count - 1; c++)
            {
                // Check if surrounding trees are smaller
                int tree = this.grid[r][c];
                // Part 1
                // if (isVisable(tree, r, c))
                // {
                //     invisableTrees++;
                // }

                // Part 2
                int score = CheckScore(tree, r, c);
                if (score > highestScore)
                {
                    highestScore = score;
                }
            }
        }

        // Part 1
        // int totalNumberOfTrees = grid.Count * grid[0].Count;
        // int totalVisableTrees = totalNumberOfTrees - invisableTrees;
        // System.Console.WriteLine($"Total Trees: {totalNumberOfTrees}, Total visable: {totalVisableTrees}");


        //Part 2
        System.Console.WriteLine(highestScore);
    }

    public bool isVisable(int tree, int row, int col)
    {
        int passedChecks = 0;

        // North
        for (int i = row - 1; i >= 0; i--)
        {
            if (this.grid[i][col] >= tree)
            {
                passedChecks++;
                break;
            }
        }

        // South
        for (int i = row + 1; i < this.grid.Count; i++)
        {
            if (this.grid[i][col] >= tree)
            {
                passedChecks++;
                break;
            }
        }

        // East
        for (int i = col + 1; i < this.grid[row].Count; i++)
        {
            if (this.grid[row][i] >= tree)
            {
                passedChecks++;
                break;
            }
        }

        // West
        for (int i = col - 1; i >= 0; i--)
        {
            if (this.grid[row][i] >= tree)
            {
                passedChecks++;
                break;
            }
        }

        return passedChecks == 4;
    }

    public int CheckScore(int tree, int row, int col)
    {
        int north = 0;
        // North
        for (int i = row - 1; i >= 0; i--)
        {
            north++;
            if (this.grid[i][col] >= tree)
            {
                break;
            }
        }

        int south = 0;
        // South
        for (int i = row + 1; i < this.grid.Count; i++)
        {
            south++;
            if (this.grid[i][col] >= tree)
            {
                break;
            }
        }

        int east = 0;
        // East
        for (int i = col + 1; i < this.grid[row].Count; i++)
        {
            east++;
            if (this.grid[row][i] >= tree)
            {
                break;
            }
        }

        int west = 0;
        // West
        for (int i = col - 1; i >= 0; i--)
        {
            west++;
            if (this.grid[row][i] >= tree)
            {
                break;
            }
        }

        return north * south * east * west;
    }
}
