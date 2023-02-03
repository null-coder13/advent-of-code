namespace SolutionLib;
using System.Linq;
using Utils;

public class DayNine
{
    public HashSet<(int, int)> visisted = new HashSet<(int, int)>();
    public (int, int) head =(0,0);
    public (int, int) tail =(0,0);
    public (int, int)[] snake = Enumerable.Repeat<(int, int)>((0,0), 10).ToArray();


    public DayNine()
    {
        string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-nine-test.txt";
        // string path = @"/home/tom/Documents/advent-of-code/2022/SolutionsLib/Inputs/day-nine-puzzle.txt";
        string[] input = Utilities.ReadInput(path);
        
        AddToVisited();

        foreach(string item in input)
        {
            string[] command = item.Split(" ");
            string direction = command[0];
            int distance = Convert.ToInt32(command[1]);

            // Part 1
            // // Move Head
            // MoveHead(direction, distance);
            //
            // // Move tail
            // MoveTail(direction);
            
            // Part 2
            while (distance != 0)
            {
                MoveHead(direction, 1);
                // Move rest of snake
                for (int i = 0; i < snake.Length - 1; i++)
                {
                    if (!MoveTail(direction, i, i + 1))
                    {
                        // Rest of tail does not need to move
                        break;
                    }
                    // Finished moving add tails current location
                    AddToVisited();
                }
                distance--;
            }
        }

        System.Console.WriteLine(visisted.Count);
    }

    public void MoveHead(string direction, int distance) 
    {
        switch (direction)
        {
            case "U":
                head.Item2 += distance;
                break;
            case "D":
                head.Item2 -= distance;
                break;
            case "R":
                head.Item1 += distance;
                break;
            case "L":
                head.Item1 -= distance;
                break;
        }

    }

    public void MoveTail(string direction)
    {
        // Check if head is already next to tail
        if (IsAdjacent()) return;

        // Check if on same x or y plane
        int x = head.Item1 - tail.Item1;
        int y = head.Item2 - tail.Item2;

        // if x == 0 or y == 0 they are on same plane
        if (x != 0 && y != 0) 
        {
             if (x > 0) 
             {
                 this.tail.Item1 += 1;
             }
             else 
             {
                 this.tail.Item1 -= 1;
             }

             if (y > 0)
             {
                 this.tail.Item2 += 1;
             }
             else 
             {
                 this.tail.Item2 -= 1;
             }
        }
        else 
        {
            while (!IsAdjacent())
            {
                FindHead(direction);
            }
        }
    }

    public bool MoveTail(string direction, int headIndex, int tailIndex)
    {
        // Check if head is already next to tail
        if (IsAdjacent(headIndex, tailIndex)) return false;

        // Check if on same x or y plane
        int x = snake[headIndex].Item1 - snake[tailIndex].Item1;
        int y = snake[headIndex].Item2 - snake[tailIndex].Item2;

        // if x == 0 or y == 0 they are on same plane
        if (x != 0 && y != 0) 
        {
             if (x > 0) 
             {
                 snake[tailIndex].Item1 += 1;
             }
             else 
             {
                 snake[tailIndex].Item1 -= 1;
             }

             if (y > 0)
             {
                 snake[tailIndex].Item2 += 1;
             }
             else 
             {
                 snake[tailIndex].Item2 -= 1;
             }
        }
        else 
        {
            while (!IsAdjacent(headIndex, tailIndex))
            {
                FindHead(DetermineDirection(headIndex, tailIndex), headIndex, tailIndex);
            }
        }
        return true;
    }

    public void FindHead(string direction)
    {
        switch(direction)
        {
            case "U":
                for (int i = tail.Item2 + 1; i < head.Item2; i++)
                {
                    tail.Item2 = i;
                    AddToVisited();
                }
                break;
            case "D":
                for (int i = tail.Item2 - 1; i > head.Item2; i--)
                {
                    tail.Item2 = i;
                    AddToVisited();
                }
                break;
            case "R":
                for (int i = tail.Item1 + 1; i < head.Item1; i++)
                {
                    tail.Item1 = i;
                    AddToVisited();
                }
                break;
            case "L":
                for (int i = tail.Item1 + 1; i > head.Item1; i--)
                {
                    tail.Item1 = i;
                    AddToVisited();
                }
                break;
        }
    }


    public void FindHead(string direction, int headIndex, int tailIndex)
    {
        switch(direction)
        {
            case "U":
                for (int i = snake[tailIndex].Item2 + 1; i < snake[headIndex].Item2; i++)
                {
                    snake[tailIndex].Item2 = i;
                    AddToVisited();
                }
                break;
            case "D":
                for (int i = snake[tailIndex].Item2 - 1; i > snake[headIndex].Item2; i--)
                {
                    snake[tailIndex].Item2 = i;
                    AddToVisited();
                }
                break;
            case "R":
                for (int i = snake[tailIndex].Item1 + 1; i < snake[headIndex].Item1; i++)
                {
                    snake[tailIndex].Item1 = i;
                    AddToVisited();
                }
                break;
            case "L":
                for (int i = snake[tailIndex].Item1 + 1; i > snake[headIndex].Item1; i--)
                {
                    tail.Item1 = i;
                    AddToVisited();
                }
                break;
        }
    }

    public bool IsAdjacent()
    {
        int x = head.Item1 - tail.Item1;
        int y = head.Item2 - tail.Item2;
        if (x <= 1 && x >= -1 && y <= 1 && y >= -1) 
        {
            return true;
        }
        return false;

    }


    public bool IsAdjacent(int headIndex, int tailIndex)
    {
        int x = snake[headIndex].Item1 - snake[tailIndex].Item1;
        int y = snake[headIndex].Item2 - snake[tailIndex].Item2;
        if (x <= 1 && x >= -1 && y <= 1 && y >= -1) 
        {
            return true;
        }
        return false;

    }


    public string DetermineDirection(int headIndex, int tailIndex) 
    {
        (int, int) head = snake[headIndex];
        (int, int) tail = snake[tailIndex];
        int x = head.Item1 - tail.Item1;
        int y = head.Item2 - tail.Item2;

        if (x == 0)
        {
            if (y > 0)
            {
                return "U";
            }
            else 
            {
                return "D";
            }
        }
        else 
        {

            if (x > 0)
            {
                return "R";
            }
            else 
            {
                return "L";
            }
        }

    }

    public void AddToVisited()
    {
        if (visisted.Add(tail))
        {
            System.Console.WriteLine("Added");
        }
        else
        {
            System.Console.WriteLine("Spot already visited");
        }
    }
}
