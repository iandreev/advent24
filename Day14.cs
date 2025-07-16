using System.Collections.Generic;

namespace Advent24;

internal class Day14
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day14.txt");
        //Console.WriteLine($"result: {Robots(input, 103, 101)}");
        Console.WriteLine($"result: {Robots2(input, 103, 101)}"); //7861
    }

    private long Robots(string[] input, int n, int m)
    {
        var list = ProcessInput(input);

        Show(n, m, list);
        var result = 0;

        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            var (r, c) = (item.Row, item.Col);
            (r, c) = (r + item.VR * 100, c + item.VC * 100);
            if (r < 0)
                r = n + r % n;
            r = r % n;

            if (c < 0)
                c = m + c % m;
            c = c % m;

            list[i] = list[i] with { Row = r, Col = c };
        }

        var mr = n / 2;
        var mc = m / 2;

        int[] q = new int[4];
        foreach (var r in list)
        {
            if (r.Row < mr && r.Col < mc) q[0] += 1;
            if (r.Row > mr && r.Col > mc) q[2] += 1;
            if (r.Row > mr && r.Col < mc) q[1] += 1;
            if (r.Row < mr && r.Col > mc) q[3] += 1;
        }

        Console.WriteLine();
        Show(n, m, list);
        return q[0] * q[1] * q[2] * q[3];
    }

    private long Robots2(string[] input, int n, int m)
    {
        var list = ProcessInput(input);

        //Show(n, m, list);
        var result = 0;

        //for (int s = 0; s < 10000; s++)
        int s = 0;
        while (true)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var (r, c) = (item.Row, item.Col);
                (r, c) = (r + item.VR, c + item.VC);
                if (r < 0)
                    r = n + r % n;
                r = r % n;

                if (c < 0)
                    c = m + c % m;
                c = c % m;

                list[i] = list[i] with { Row = r, Col = c };
            }


            s++;
            if (s % 100 == 0)
            {
                Console.WriteLine(s);
            }
            if (IsTree(list))
            {
                Console.WriteLine($"Tree found at step {s}");
                break;
            }
        }
        Show(n, m, list);
        Console.ReadKey();

        return s;
    }

    private bool IsTree(List<Robo> robots)
    {
        var a = robots.GroupBy(x => x.Col)
            .Select(x => new { Key = x.Key, Count = x.Count() })
            .Where(x => x.Count > 20)
            .Any();

        var b = robots.GroupBy(x => x.Row)
            .Select(x => new { Key = x.Key, Count = x.Count() })
            .Where(x => x.Count > 20)
            .Any();

        return a && b;
    }
    private void Show(int n, int m, List<Robo> robots)
    {
        var rmap = robots.Select(x => (x.Row, x.Col))
            .GroupBy(p => p).Select(x => new { Key = x.Key, Count = x.Count() })
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

    private List<Robo> ProcessInput(string[] input)
    {
        List<Robo> res = new();
        for (int i = 0; i < input.Length; i++)
        {
            // p=10,3 v=-1,2
            var parts = input[i].Replace("p=", "")
                .Replace("v=", "")
                .Replace(",", " ")
                .Split(' ');
            var col = long.Parse(parts[0]);
            var row = long.Parse(parts[1]);
            var vc = long.Parse(parts[2]);
            var vr = long.Parse(parts[3]);
            res.Add(new Robo(row, col, vr, vc));
        }
        return res;
    }

    int At(int i, int j, int n, int m)
        => i < 0 || i >= n || j < 0 || j >= m ? -1 : 0;

    record Robo(long Row, long Col, long VR, long VC);
}
