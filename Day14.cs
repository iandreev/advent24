namespace Advent24;

internal class Day14
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day14.txt");
        Console.WriteLine($"result: {Robots(input)}"); //
    }

    private long Robots(string[] input)
    {
        int n = 7;
        int m = 11;
        var list = ProcessInput(input);

        Show(n, m, list);
        var result = 0;
        foreach (var item in list)
        {
            for (int i = 0; i < 100; i++)
            {
                var (x, y) = (item.x + item.vx * i, item.y + item.vy * i);
            }
        }
        return result;
    }

    private void Show(int n, int m, List<(long x, long y, long vx, long vy)> robots)
    {
        var rmap = robots.Select(x => (x.x, x.y))
            .GroupBy(p => p).Select(x => new { Key = x.Key, Count = x.Count()})
            .ToList();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                var robot = rmap.FirstOrDefault(x => x.Key == (i, j));
                if (robot != null)
                {
                    Console.Write(robot.Count);
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
    }

    private List<(long x, long y, long vx, long vy)> ProcessInput(string[] input)
    {
        List<(long x, long y, long vx, long vy)> res = new();
        for (int i = 0; i < input.Length; i++)
        {
            // p=10,3 v=-1,2
            var parts = input[i].Replace("p=", "")
                .Replace("v=", "")
                .Replace(",", " ")
                .Split(' ');
            var x = long.Parse(parts[0]);
            var y = long.Parse(parts[1]);
            var vx = long.Parse(parts[2]);
            var vy = long.Parse(parts[3]);
            res.Add((x, y, vx, vy));
        }
        return res;
    }

    int At(int i, int j, int n, int m)
        => i < 0 || i >= n || j < 0 || j >= m ? -1 : 0;
}
