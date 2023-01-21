namespace SolutionLib;
using System.IO;
using System.Linq;
using Utils;

public class DaySeven
{
    public Tree structure = new Tree { Root = new TreeNode("/", 0, null) };
    public Stack<string> currentDirectory = new Stack<string>();
    public IDictionary<string, int> directories = new Dictionary<string, int>();

    public DaySeven()
    {
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-seven-test.txt";
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-seven-puzzle.txt";
        string[] input = Utilities.ReadInput(path);

        // TODO: 
        // 1. Create a Tree datastructure
        //    -- Need to know the parent and list of child nodes
        // 2. Create methods that read the input commands and create the appropriate treenodes
        int index = 0;
        while (index < input.Length - 1)
        {
            Console.WriteLine("Current index: " + index);
            string[] split = input[index].Split(" ");

            // Get command
            string command = split[1];
            Console.WriteLine($"Command: {command}");

            switch (command)
            {
                case "cd":
                    string nav = split[2];
                    ChangeDirectory(nav);
                    index++;
                    break;
                case "ls":
                    TreeNode current = GetCurrentDirectory();
                    List<TreeNode> children = new List<TreeNode>();
                    string[] file = input[++index].Split(" ");

                    while (!file[0].Equals("$"))
                    {
                        string filename = file[1];
                        int data;

                        // Check if the file is a directory
                        if (file[0].Equals("dir"))
                        {
                            data = 0;
                        }
                        else
                        {
                            data = Convert.ToInt32(file[0]);
                        }
                        children.Add(new TreeNode(filename, data, current));
                        if (index < input.Length - 1)
                        {
                            file = input[++index].Split(" ");
                        }
                        else
                        {
                            break;
                        }
                    }

                    current.Children = children;
                    break;
            }
        }

        // Calculate the sum of each directory


        Console.WriteLine("Printing Tree...");
        PrintTree(structure.Root);
    }

    public int CalculateDirectoryTotal()
    {
        Stack<string> dir = new Stack<string>();
        TreeNode current = structure.Root;
        
        

        return 0;
    }

    public void PrintTree(TreeNode current)
    {
        if (current.Children == null)
        {
            return;
        }
        foreach (TreeNode child in current.Children)
        {
            child.PrintChildren();
            PrintTree(child);
        }
    }

    public void ChangeDirectory(string nav)
    {
        // Console.WriteLine("Changing directory...");
        switch (nav)
        {
            case "/":
                this.currentDirectory.Clear();
                break;
            case "..":
                this.currentDirectory.Pop();
                break;
            default:
                this.currentDirectory.Push(nav);
                break;
        }
    }

    public TreeNode GetCurrentDirectory()
    {
        Console.WriteLine("Getting current directory...");
        if (currentDirectory.Count == 0)
        {
            return this.structure.Root;
        }
        else
        {
            TreeNode current = this.structure.Root;
            string[] pathToNode = currentDirectory.ToArray();
            int currentPathStep = pathToNode.Length - 1;
            while (!current.Filename.Equals(currentDirectory.Peek()))
            {
                foreach (TreeNode child in current.Children)
                {
                    if (child.Filename.Equals(pathToNode[currentPathStep]))
                    {
                        current = child;
                        currentPathStep--;
                        break;
                    }
                }
            }
            return current;
        }

    }
}
