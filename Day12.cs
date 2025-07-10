using System.Xml;

namespace Advent24;

internal class Day12
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day12.txt");
        Console.WriteLine($"result: {Areas(input)}");
        Console.WriteLine($"result: {Areas2(input)}");
    }

    private long Areas(string[] input)
    {
        var result = 0L;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (vis.Contains((i, j))) continue;
                result += Bfs(input, i, j);
            }
        }
        return result;
    }

    private long Areas2(string[] input)
    {
        var result = 0L;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (vis2.Contains((i, j))) continue;
                result += Bfs2(input, i, j);
            }
        }
        return result;
    }

    HashSet<(int x, int y)> vis = new();
    private long Bfs(string[] map, int i, int j)
    {
        var start = At(map, i, j);
        var area = 1L;
        var per = 0L;

        Queue<(int x, int y)> q = new Queue<(int x, int y)>();
        q.Enqueue((i, j));
        vis.Add((i, j));

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            foreach (var dir in new[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
            {
                var next = (x + dir.Item1, y + dir.Item2);
                var nextAt = At(map, next.Item1, next.Item2);

                if (nextAt != start) per += 1;

                if (vis.Contains(next)) continue;

                if (nextAt == start)
                {
                    area += 1;
                    vis.Add(next);
                    q.Enqueue(next);
                }
            }
        }
        //Console.WriteLine($"{start}: {area} * {per}");
        return area * per;
    }

    HashSet<(int x, int y)> vis2 = new();
    private long Bfs2(string[] map, int i, int j)
    {
        var start = At(map, i, j);
        var area = 1L;
        var per = 0L;

        Dictionary<(int dx, int dy), HashSet<(int x, int y)>> edges = new();
        Queue<(int x, int y)> q = new Queue<(int x, int y)>();
        q.Enqueue((i, j));
        vis2.Add((i, j));

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            foreach (var dir in new[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
            {
                var next = (x + dir.Item1, y + dir.Item2);
                var nextAt = At(map, next.Item1, next.Item2);

                if (nextAt != start)
                {
                    if(!edges.ContainsKey(dir)) 
                        edges[dir] = new HashSet<(int x, int y)> { (x, y) };
                    edges[dir].Add((x, y));
                }

                if (vis2.Contains(next)) continue;

                if (nextAt == start)
                {
                    area += 1;
                    vis2.Add(next);
                    q.Enqueue(next);
                }
            }
        }

        foreach (var key in edges.Keys)
        {
            HashSet<(int x, int y)> vis = new();
            var v = edges[key];

            foreach (var e in v)
            {
                if (vis.Contains(e)) continue;
                vis.Add(e);
                BfsEdges(v, e.x, e.y, vis);
                per += 1;
            }
        }
        return area * per;
    }
    private void BfsEdges(HashSet<(int x, int y)> map, int i, int j, HashSet<(int x, int y)> vis)
    {
        Queue<(int x, int y)> q = new Queue<(int x, int y)>();
        q.Enqueue((i, j));
        vis.Add((i, j));

        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            foreach (var dir in new[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
            {
                var next = (x + dir.Item1, y + dir.Item2);

                if (vis.Contains(next)) continue;
                if (map.Contains(next))
                {
                    vis.Add(next);
                    q.Enqueue(next);
                }
            }
        }
    }

    char At(string[] map, int i, int j)
        => i < 0 || i >= map.Length || j < 0 || j >= map[0].Length ? '.' : map[i][j];
}
