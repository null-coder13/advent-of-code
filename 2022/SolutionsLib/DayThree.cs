namespace SolutionLib;
using System.IO;
using System.Linq;
using Utils;

public class DayThree
{

    public int lowercase = 96;
    public int uppercase = 38;
    public int sum = 0;

    public HashSet<char> characters = new HashSet<char>();
    HashSet<char> bagOne = new HashSet<char>();
    HashSet<char> bagTwo = new HashSet<char>();
    HashSet<char> bagThree = new HashSet<char>();

    public DayThree() 
    {
        // Read in the input
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/test.txt";
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-three-puzzle.txt";
        string[] input = Utilities.ReadInput(path);
        
        for (int i = 0; i <= input.Length - 3; i+= 3)
        {
            // Console.WriteLine("Word " + i + ": " + input[i]);
            // Console.WriteLine("Word " + (i + 1) + ": " + input[i + 1]);
            // Console.WriteLine("Word " + (i + 2) + ": " + input[i + 2]);
            PartTwo(input[i], input[i + 1], input[i + 2]);
        }

        Console.WriteLine("Total sum = " + this.sum);

    }

    public void PartTwo(string one, string two, string three) 
    {
        // Find all unique chars in each string
        FindUniqueChars(one, 1);
        FindUniqueChars(two, 2);
        FindUniqueChars(three, 3);

        // Console.WriteLine("Complete finding unique chars...");

        // Console.WriteLine("Finding similar char...");
        foreach(char c in this.bagOne)
        {
            // Console.WriteLine("Checking char: " + c + " " + this.bagTwo.Contains(c) + " " + this.bagThree.Contains(c));
            if (this.bagTwo.Contains(c) && this.bagThree.Contains(c))
            {
                this.sum += CalculateValue(c);
            }
        }
        // Clear bags
        bagOne = new HashSet<char>();
        bagTwo = new HashSet<char>();
        bagThree = new HashSet<char>();
    }

    public void FindUniqueChars(string word, int bag) 
    {
        foreach (char c in word)
        {
           switch (bag) 
           {
               case 1:
                   bagOne.Add(c);
                   break;
               case 2:
                   bagTwo.Add(c);
                   break;
               case 3:
                   bagThree.Add(c);
                   break;
           }
        }
    }

    public void PartOne(string i) 
    {
        // Console.WriteLine(i);
        // Find length of string 
        int length = i.Length / 2;
        int index = 0;

        // Loop through string adding unique char to characters hashset for first bag
        foreach(char c in i) 
        {
            if (index < length)
            {
                // Console.WriteLine("Adding char: " + c + "  || At index: " + index + " of length: " + (length));
                characters.Add(c);
                index++;
            }
            else if (characters.Contains(c))
            {
                // Find the value of the character
                // Console.WriteLine("Found duplicate char: " + c);
                sum += CalculateValue(c);
                // Console.WriteLine(value.ToString());
                characters = new HashSet<char>();
                break;
            }
        }
    }

    public int CalculateValue(char c)
    {
        // Console.WriteLine("Calculating value...");
        int value;
        if (Char.IsUpper(c))
        {
            value = (int) c - uppercase; 
        }
        else 
        {
            value = (int) c - lowercase; 
        }
        return value;
    }
    
}
