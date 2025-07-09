namespace Advent24;

internal class Day10
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day10.txt");
        Console.WriteLine($"result: {NumTrails(input)}");
        Console.WriteLine($"result: {NumTrails2(input)}");
    }
    long NumTrails2(string[] input)
    {
        long result = 0;

        var map = ProcessInput(input);
        for (int i = 0; i < input.Length; i++)
            for (int j = 0; j < input[0].Length; j++)
                if (map[i, j] == 0) result += Dfs(map, i, j);
        return result;
    }

    private long Dfs(int[,] map, int i, int j)
    {
        long result = 0;
        var dirs = new List<(int x, int y)> { (1, 0), (-1, 0), (0, 1), (0, -1) };
        var atCur = At(map, i, j);

        if (atCur == 9) return 1; // reached the end
        foreach (var dir in dirs)
        {
            (int x, int y) next = (i + dir.x, j + dir.y);
            var atNext = At(map, next.x, next.y);
            if (atNext - atCur == 1)
            {
                result += Dfs(map, next.x, next.y);
            }
        }
        return result;
    }


    long NumTrails(string[] input)
    {
        long result = 0;

        var map = ProcessInput(input);
        for (int i = 0; i < input.Length; i++)
            for (int j = 0; j < input[0].Length; j++)
                if (map[i, j] == 0) result += Bfs(map, i, j);
        return result;
    }

    private long Bfs(int[,] map, int i, int j)
    {
        var result = 0;
        HashSet<(int x, int y)> vis = new();
        Queue<(int x, int y)> q = new Queue<(int x, int y)>();
        q.Enqueue((i, j));
        vis.Add((i, j));

        var dirs = new List<(int x, int y)> { (1, 0), (-1, 0), (0, 1), (0, -1) };
        while (q.Any())
        {
            (i, j) = q.Dequeue();

            var atCur = At(map, i, j);
            if (atCur == 9) result += 1;
            foreach (var dir in dirs)
            {
                (int x, int y) next = (i + dir.x, j + dir.y);
                var atNext = At(map, next.x, next.y);
                if (atNext != -1 && !vis.Contains(next) && atNext - atCur == 1)
                {
                    q.Enqueue(next);
                    vis.Add(next);
                }
            }
        }
        return result;
    }

    void Show(int[,] map, HashSet<(int x, int y)> vis)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (vis.Contains((i, j)))
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                Console.Write(map[i,j] == -1 ? "." : map[i,j]);
            }
            Console.WriteLine();
        }
    }
    int At(int[,] map, int i, int j)
        => i < 0 || i >= map.GetLength(0) || j < 0 || j >= map.GetLength(1) ? -1 : map[i, j];

    int[,] ProcessInput(string[] input)
    {
        var result = new int[input.Length, input[0].Length];
        for (int i = 0; i < input.Length; i++)
            for (int j = 0; j < input[0].Length; j++)
                result[i, j] = input[i][j] == '.' ? -1 : int.Parse(input[i][j].ToString());
        return result;
    }
}
