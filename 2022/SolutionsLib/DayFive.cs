namespace SolutionLib;
using System.IO;
using System.Linq;
using Utils;

public class DayFive
{
    public IDictionary<int, LinkedList<char>> stack = new Dictionary<int, LinkedList<char>>();

    public DayFive()
    {
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-five-test.txt";
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-five-puzzle.txt";
        string[] input = Utilities.ReadInput(path);

        // Initialize stack
        for(int i = 1; i <= 9; i++)
        {
            stack.Add(i, new LinkedList<char>());
        }

        // while loop till row equals 1
        int index = 0;
        string current = input[index];
        while (!current[1].Equals('1'))
        {
            // Add values to respective rows
            // Console.WriteLine("current[1] = " + current[1]);
            AddToStack(current);
            current = input[++index];
        }
        // Console.WriteLine("exiting loop...");

        // PrintDictionary();
        
        // add 1 to index to skip blank line
        index += 2;
        // Console.WriteLine("Current index: " + index);
        for (int i = index; i < input.Length; i ++) 
        {
            string[] cmd = input[i].Split(" ");

            int amount = Convert.ToInt32(cmd[1]);
            int from = Convert.ToInt32(cmd[3]);
            int to = Convert.ToInt32(cmd[5]);
            // move(amount, from, to);
            moveMultiple(amount, from, to);
        }

        // PrintDictionary();
        PrintLastItems();
        
    }

    public void PrintLastItems()
    {
        foreach(KeyValuePair<int, LinkedList<char>> s in stack) 
        {
            Console.Write(s.Value.Last());
        }
        Console.WriteLine();
    }

    public void move(int amount, int from, int to)
    {
        for (int i = 0; i < amount; i++)
        {
            char item = this.stack[from].Last();
            this.stack[from].RemoveLast();
            this.stack[to].AddLast(item);
        }
    }

    public void moveMultiple(int amount, int from, int to) 
    {
        List<char> temp = new List<char>();
        //Add to a list 
        for (int i = 0; i < amount; i++)
        {
            char item = this.stack[from].Last();
            this.stack[from].RemoveLast();
            temp.Add(item);
        }
        //Remove from list and add back to stack
        for (int i = temp.Count - 1; i >= 0; i--) 
        {
            this.stack[to].AddLast(temp[i]);
        }
    }

    public void AddToStack(string row)
    {
        int location = 1;
        // Check how long the row is
        if (row.Length <= 11)
        {
            // This is the test case and only need to add for 3 stacks
            for (int i = 1; i <= 3; i++)
            {
                if (!row[location].Equals(' '))
                {
                    stack[i].AddFirst(row[location]);
                }
                location += 4;
            }
        }
        else 
        {
            for (int i = 1; i <= 9; i++)
            {
                if (!row[location].Equals(' '))
                {
                    stack[i].AddFirst(row[location]);
                }
                location += 4;
            }
        }
    }

    public void PrintDictionary()
    {

        foreach(KeyValuePair<int, LinkedList<char>> s in stack) 
        {
            foreach(var c in s.Value) {
                Console.Write(c + " ");
            }

            Console.WriteLine();
        }
    }
}
