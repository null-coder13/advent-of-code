namespace SolutionLib;
using System.IO;
using System.Linq;
using Utils;

public class DaySix
{
    public HashSet<char> charSet = new HashSet<char>();
    public int index = 0;

    public DaySix()
    {
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-six-test.txt";
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-six-puzzle.txt";
        string[] input = Utilities.ReadInput(path);

        char[] message = input[0].ToCharArray();


        // Part 1
        // int length = 0;
        // for (int i = 3; i < message.Length; i++)
        // {
        //     if (isUnique(message, i - 3, i))
        //     {
        //         Console.WriteLine($"Unique Found: {message[i - 3]} {message[i - 2]} {message[i - 1]} {message[i]}");
        //         length = i + 1;
        //         Console.WriteLine($"Length is: {length}");
        //         break;
        //     }
        // }

        // Console.WriteLine(length);

        // Part 2
        for (int i = 0; i < message.Length; i++)
        {
            if (FindMarker(message, i))
            {
                break;
            }
        }

        Console.WriteLine(this.index);
    }

    public bool isUnique(char[] arr, int start, int end)
    {
        // need 14 unique char in a row
        HashSet<char> uniqueSet = new HashSet<char>();
        for (int i = start; i <= end; i++)
        {
            this.charSet.Add(arr[i]);
            if (!uniqueSet.Add(arr[i]))
            {
                return false;
            }

        }
        return true;
    }

    public bool FindMarker(char[] arr, int start)
    {
        HashSet<char> uniqueSet = new HashSet<char>();
        for (int i = start; i < arr.Length; i++)
        {
            if (uniqueSet.Count == 14)
            {
                this.index = i;
                return true;
            }
            if (!uniqueSet.Add(arr[i]))
            {
                return false;
            }
        }
        return false;
    }
}
