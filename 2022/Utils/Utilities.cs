namespace Utils;

using System.IO;
using System.Linq;

public class Utilities

{
    public static string[] ReadInput(string path) 
    {
        if (File.Exists(path)) 
        {
            // Read all lines of file
            string[] values = File.ReadAllLines(path).ToArray();
            return values;
        }
        else 
        {
            return null;
        }

    }

}
