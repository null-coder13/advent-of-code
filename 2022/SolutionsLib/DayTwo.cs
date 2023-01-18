namespace SolutionLib;
using System.IO;
using System.Linq;

public class DayTwo {


    // Opponent
    // A - Rock 
    // B - Paper
    // C - Scissors
    //
    // You
    // X - Rock
    // Y - Paper
    // Z - Scissors
    //
    // Scores:
    // 1 - Rock
    // 2 - Paper
    // 3 - Scissors
    // 0 - Loss
    // 3 - Draw
    // 6 - Win
    //
    // Part 2:
    // X - loose
    // Y - draw
    // Z - win

    // Dictonary for wins
    public IDictionary<string, string> winMap = new Dictionary<string, string>();
    public IDictionary<string, string> tieMap = new Dictionary<string, string>();
    public IDictionary<string, string> looseMap = new Dictionary<string, string>();
    public int total = 0;


    public DayTwo() 
    {
        // Set up win map where you win
        winMap.Add("A", "B"); // rock
        winMap.Add("B", "C"); // paper
        winMap.Add("C", "A"); // scissors

        // Set up loose map where the opponent wins
        looseMap.Add("A", "C");
        looseMap.Add("B", "A");
        looseMap.Add("C", "B");

        // Set up tie map where no one wins
        tieMap.Add("A", "A");
        tieMap.Add("B", "Y");
        tieMap.Add("C", "Z");

        Console.WriteLine("Reading in file...");
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-two-puzzle.txt";

        if (File.Exists(path)) 
        {
            // Read all lines of file
            string[] values = File.ReadAllLines(path).ToArray();
            for (int i = 0; i < values.Length; i++) 
            {
                string[] round = values[i].Split(' ');
                
                // Determine which to use
                switch (round[1])
                {
                    case "X":
                        // loose
                        this.total += CalculatePoints(looseMap[round[0]]);
                        break;
                    case "Y":
                        // draw
                        this.total += 3 + CalculatePoints(round[0]);
                        break;
                    case "Z":
                        // win
                        this.total += 6 + CalculatePoints(winMap[round[0]]);
                        break;
                    default:
                        Console.WriteLine("Unknown condition");
                        break;
                }

            }
            Console.WriteLine(this.total);

        }
        else 
        {
            Console.WriteLine("Error: File not found");
        }
    }

    public int CalculatePoints(string obj) 
    {
        switch (obj) 
        {
            case "A":
                return 1;
            case "B":
                return 2;
            case "C":
                return 3;
            default:
                return 0;
        }
    }


}
