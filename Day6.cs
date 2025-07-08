namespace Advent24;

internal class Day6
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day6.txt");
        Console.WriteLine($"route: {FindRoute(input)}");
        Console.WriteLine($"route: {FindLoops(input)}");
    }

    private int FindLoops(string[] input)
    {
        var (startx, starty) = (0, 0);
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '^')
                {
                    (startx, starty) = (i, j);
                    break;
                }
            }
        }

        var dirs = new Dictionary<(int, int), (int, int)>
        {
            { (0, -1), (-1, 0) },
            { (1, 0), (0, -1) },
            { (0, 1), (1, 0) },
            { (-1, 0), (0, 1) }
        };

        var result = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] != '.')
                    continue;
                result += IsLoop(input, startx, starty, dirs, i, j);
            }
        }
        

        return result;
    }

    private int IsLoop(string[] input,
        int startx,
        int starty,
        Dictionary<(int, int), (int, int)> dirs,
        int addx,
        int addy)
    {
        var (x, y) = (startx, starty);
        (int x, int y) d = (-1, 0);
        HashSet<(int, int, (int, int))> visited = new();
        while (At(input, x, y) != null)
        {
            if (visited.Contains((x, y, d))) // loop
            {
                return 1;
            }

            visited.Add((x, y, d));

            while (At(input, x + d.x, y + d.y) == '#' || (x + d.x == addx && y + d.y == addy))
            {
                d = dirs[d];
                visited.Add((x, y, d));
            }
            (x, y) = (x + d.x, y + d.y);
        }

        return 0;
    }

    private int FindRoute(string[] input)
    {
        HashSet<(int, int)> visited = new();

        var (x, y) = (0, 0);
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '^')
                {
                    (x, y) = (i, j);
                    break;
                }
            }
        }

        var dirs = new Dictionary<(int, int), (int, int)>
        {
            { (0, -1), (-1, 0) },
            { (1, 0), (0, -1) },
            { (0, 1), (1, 0) },
            { (-1, 0), (0, 1) }
        };

        (int x, int y) d = (-1, 0);
        while (At(input, x, y) != null)
        {
            if (!visited.Contains((x, y)))
                visited.Add((x, y));

            if (At(input, x + d.x, y + d.y) == '#')
                d = dirs[d];
            (x, y) = (x + d.x, y + d.y);
        }

        //ShowMap(input, visited);
        return visited.Count;
    }

    private static void ShowMap(string[] input, HashSet<(int, int)> visited, (int x,int y)? red = null)
    {
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (red != null && red.Value.x == i && red.Value.y == j)
                    Console.ForegroundColor = ConsoleColor.Red;
                if (visited.Contains((i, j)))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(input[i][j]);
            }
            Console.WriteLine();
        }
    }

    private char? At(string[] input, int x, int y)
        => (x < 0 || x >= input.Length || y < 0 || y >= input[0].Length) ? null : input[x][y];
}
