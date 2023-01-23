namespace SolutionLib;
using System.IO;
using System.Linq;
using Utils;

public class DaySeven
{
    public Tree structure = new Tree { Root = new TreeNode("/", 0, null) };
    public Stack<TreeNode> currentDirectory = new Stack<TreeNode>();
    public IDictionary<string, int> directories = new Dictionary<string, int>();
    public List<TreeNode> listOfDirectories = new List<TreeNode>();

    public DaySeven()
    {
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-seven-test.txt";
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-seven-puzzle.txt";
        string[] input = Utilities.ReadInput(path);
        Console.WriteLine("Starting Calculations");

        int index = 0;
        while (index < input.Length - 1)
        {
            // Console.WriteLine("Current index: " + index);
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
                        Console.WriteLine($"{file[0]} {file[1]}");

                        // Check if the file is a directory
                        if (file[0].Equals("dir"))
                        {
                            data = 0;
                        }
                        else
                        {
                            data = Convert.ToInt32(file[0]);
                        }
                        TreeNode newChild = new TreeNode(filename, data, current);
                        if (newChild.Data == 0)
                        {
                            listOfDirectories.Add(newChild);
                        }
                        children.Add(newChild);
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

        this.listOfDirectories.Add(structure.Root);
        Console.WriteLine("Caculating directory totals...");
        CalcTotal();

        Console.WriteLine("Printing directory totals...");
        PrintDirectoryTotals();

        Console.WriteLine("Summing totals of directories less than 100k...");
        Console.WriteLine("Total: " + TotalOfDirectories());

        // sort the directores from smallest to largest
        int spaceNeeded = 30000000 - (70000000 - this.directories["/"]);
        Console.WriteLine($"Space required: {spaceNeeded}");
        int spaceToDelete = 0;
        foreach (KeyValuePair<string, int> s in  this.directories.OrderBy(key => key.Value)) {
            spaceToDelete = s.Value;
            if (spaceNeeded <= s.Value)
            {
                break;
            }
        }

        Console.WriteLine($"Space to delete: {spaceToDelete}");

    }

    public int TotalOfDirectories()
    {
        int total = 0;
        foreach(KeyValuePair<string, int> dir in directories)
        {
            if (dir.Value <= 100000)
            {
                total += dir.Value;
            }
        }
        return total;
    }

    public void CalcTotal()
    {
        int duplicates = 0;
        foreach(TreeNode dir in this.listOfDirectories)
        {
            Console.WriteLine($"Checking dir {dir}");
            int total = 0;
            foreach(TreeNode child in dir.Children)
            {
                if (child.Data == 0)
                {
                    total += CalcuateSubDirectory(child);
                }
                else
                {
                    total += child.Data;
                }
            }
            //Might need to check this again
            Console.WriteLine($"Adding {dir.Filename} with total: {total}");
            if (this.directories.ContainsKey(dir.Filename)) 
            {
                this.directories.Add(dir.Filename + duplicates++, total);
            }
            else 
            {
                this.directories.Add(dir.Filename, total);
            }
        }
    }

    public int CalcuateSubDirectory(TreeNode sub)
    {
        Console.WriteLine("Calculating sub directory: " + sub.Filename);
        int total = 0;
        foreach(TreeNode child in sub.Children)
        {
            if (child.Data == 0)
            {
               total += CalcuateSubDirectory(child);
            }
            else 
            {
                total += child.Data;
            }
        }
        return total;
    }


    public void PrintDirectoryTotals()
    {
        foreach(KeyValuePair<string, int> dir in directories)
        {
            Console.WriteLine($"{dir.Key}: {dir.Value}");
        }
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
                this.currentDirectory.Push(structure.Root);
                break;
            case "..":
                this.currentDirectory.Pop();
                break;
            default:
                // Get the current directory off stack
                TreeNode cur = this.currentDirectory.Peek();
                foreach(TreeNode child in cur.Children)
                {
                    if (child.Filename.Equals(nav)) {
                        this.currentDirectory.Push(child);
                    }
                }
                break;
        }
    }

    public TreeNode GetCurrentDirectory()
    {
        // Console.WriteLine("Getting current directory...");
        // if (currentDirectory.Count == 0)
        // {
        //     return this.structure.Root;
        // }
        // else
        // {
        //     TreeNode current = this.structure.Root;
        //     string[] pathToNode = currentDirectory.ToArray();
        //     Console.WriteLine("Printing Path to directory");
        //     foreach(string p in pathToNode)
        //     {
        //         Console.Write(p + " ->");
        //     }
        //     int currentPathStep = pathToNode.Length - 1;
        //     while (!current.Filename.Equals(currentDirectory.Peek()))
        //     {
        //         // Console.WriteLine("Infite loop");
        //         foreach (TreeNode child in current.Children)
        //         {
        //             if (child.Filename.Equals(pathToNode[currentPathStep]))
        //             {
        //                 current = child;
        //                 currentPathStep--;
        //                 break;
        //             }
        //         }
        //     }
        //     return current;
        // }
        return currentDirectory.Peek();

    }
}
