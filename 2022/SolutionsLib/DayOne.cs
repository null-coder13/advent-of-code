namespace SolutionLib;
using System.IO;
using System.Linq;

public class DayOne
{
    public DayOne() 
    {
        // Variables to track current highest
        // int highestCalories = Int32.MinValue;
        int current = 0;
        List<int> elfs = new List<int>();

        // Read in the file
        Console.WriteLine("Reading in file...");
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-one-puzzle.txt";
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-one-test.txt";

        if (File.Exists(path)) 
        {
            // Read all lines of file
            string[] values = File.ReadAllLines(path).ToArray();
            for (int i = 0; i < values.Length; i++) 
            {
                // Check if we have reached the end of an elf or end of the array
                if (values[i] == "" || i == values.Length - 1)
                {
                    if (i == values.Length - 1) 
                    {
                        current += Convert.ToInt32(values[i]);
                    }
                    elfs.Add(current);
                    current = 0;
                }
                else
                {
                    // Add up current calory
                    current += Convert.ToInt32(values[i]);
                }
            }

        }
        else 
        {
            Console.WriteLine("Error: File not found");
        }

        var descendingOrder = elfs.OrderByDescending(i => i);

        int total = 0;
        int index = 0;

        foreach (var elf in descendingOrder)
        {
            if (index == 3)
                break;
            Console.WriteLine("Adding to total: " + elf);
            total += Convert.ToInt32(elf);
            Console.WriteLine("Current total = " + total);
            index++;
        }

        Console.WriteLine("Total: " + total);

    }
}
