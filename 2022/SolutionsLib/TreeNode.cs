namespace SolutionLib;

public class TreeNode
{
    public int Data { get; set; }
    public string Filename { get; set; }
    public TreeNode? Parent { get; set; }
    public List<TreeNode> Children { get; set; }

    public TreeNode(string filename, int data, TreeNode? parent)
    {
        this.Data = data;
        this.Filename = filename;
        this.Children = new List<TreeNode>();
        this.Parent = parent;
    }

    public void PrintChildren()
    {
        if (this.Children.Count == 0)
        {
            return;
        }
        Console.WriteLine("Printing children of " + this.Filename);
        foreach (TreeNode t in Children)
        {
            Console.WriteLine("-- " + t.Filename);
        }
    }
}
