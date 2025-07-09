namespace Advent24;

internal class Day8
{

    public void Go()
    {
        var v = new HashSet<int>(); 
        var input = File.ReadAllLines("input/day8.txt");
        Console.WriteLine($"total: {FindAntinodes(input)}");
        Console.WriteLine($"total: {FindAntinodes2(input)}");
    }

    int FindAntinodes(string[] input)
    {
        var map = ProcessInput(input);
        HashSet<(int,int)> set = new();

        for (int i = 0; i < input.Length; i++)
            for (int j = 0; j < input[i].Length; j++)
            {
                var c = input[i][j];
                if (c == '.') continue;

                var all = map[c];
                foreach (var a in all.Where(x => x != (i,j)))
                {
                    var dy = j - a.y;
                    var dx = i - a.x;

                    (int x, int y) p1 = (a.x - dx, a.y - dy);
                    (int x, int y) p2 = (i + dx, j + dy);

                    if (!Out(input, p1.x, p1.y)) set.Add(p1);
                    if (!Out(input, p2.x, p2.y)) set.Add(p2);
                }
            }
        Show(input, set);
        return set.Count;
    }

    int FindAntinodes2(string[] input)
    {
        var map = ProcessInput(input);
        HashSet<(int, int)> set = new();

        for (int i = 0; i < input.Length; i++)
            for (int j = 0; j < input[i].Length; j++)
            {
                var c = input[i][j];
                if (c == '.') continue;

                var all = map[c];
                foreach (var a in all.Where(x => x != (i, j)))
                {
                    var dy = j - a.y;
                    var dx = i - a.x;

                    for (int k = -100; k <= 100; k++)
                    {
                        (int x, int y) p = (a.x + dx * k, a.y + dy * k);
                        if (!Out(input, p.x, p.y)) set.Add(p);
                    }
                }
            }
        Show(input, set);
        return set.Count;
    }

    void Show(string[] input, HashSet<(int, int)> set)
    {
        for (int i = 0; i< input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (set.Contains((i, j)))
                    Console.Write("#");
                else
                    Console.Write(input[i][j]);
            }
            Console.WriteLine();
        }
    }

    bool Out(string[] input, int x, int y)
        => x < 0 || y < 0 || x >= input.Length || y >= input[0].Length;

    Dictionary<char, List<(int x, int y)>> ProcessInput(string[] input)
    {
        var map = new Dictionary<char, List<(int x, int y)>>();
        for (int i = 0; i < input.Length; i++)
            for (int j = 0; j < input[i].Length; j++)
            {
                var c = input[i][j];
                if (c == '.') continue;
                if (!map.ContainsKey(c)) map.Add(c, new List<(int x, int y)>());
                map[c].Add((i, j));
            }

        return map;
    }
}
