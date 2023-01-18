namespace SolutionLib;
using System.IO;
using System.Linq;
using Utils;


public class DayFour
{
    public int count = 0;
    
    public DayFour()
    {
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-four-test.txt";
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-four-puzzle.txt";
        string[] input = Utilities.ReadInput(path);

        foreach (string i in input)
        {
            // Spilt by , to get first two splits
            string[] split = i.Split(',');
            int[] elfOne = ConvertToIntArray(split[0]);
            int[] elfTwo = ConvertToIntArray(split[1]);

            // Calculate which elf has largest range
            int larger = LargestRange(elfOne, elfTwo);

            // Compare largest to smallest
            if (larger == 0) {
                // CheckForCompleteOverlap(elfOne, elfTwo);
                CheckForAnyOverlap(elfOne, elfTwo);
            } else {
                // CheckForCompleteOverlap(elfTwo, elfOne);
                CheckForAnyOverlap(elfTwo, elfOne);
            }
        }
        Console.WriteLine(this.count);

    }

    public void CheckForCompleteOverlap(int[] largerRange, int[] smallerRange) 
    {
        // Check if lower bound
        bool lowerBound = largerRange[0] - smallerRange[0] <= 0;
        
        // Check upper bound
        bool upperBound = largerRange[1] - smallerRange[1] >= 0;

        if (lowerBound && upperBound) 
        {
            this.count++;
        }

    }

    public void CheckForAnyOverlap(int[] largerRange, int[] smallerRange) {
        bool lowerBound = smallerRange[0] >= largerRange[0] && smallerRange[0] <= largerRange[1];
        bool upperBound = smallerRange[1] <= largerRange[1] && smallerRange[1] >= largerRange[0];

        if (lowerBound || upperBound)
        {
            this.count++;
        }

    }

    public int[] ConvertToIntArray(string value) 
    {
        int[] convertedValues = new int[2];
        string[] splitByDash = value.Split('-');
        convertedValues[0] = Convert.ToInt32(splitByDash[0]);
        convertedValues[1] = Convert.ToInt32(splitByDash[1]);
        return convertedValues;

    }

    public int LargestRange(int[] elfOne, int[] elfTwo) 
    {
        int elfRangeOne = elfOne[1] - elfOne[0];
        int elfRangeTwo = elfTwo[1] - elfTwo[0];

        if (elfRangeOne >= elfRangeTwo)
        {
            return 0;
        }
        return 1;
    }

}
