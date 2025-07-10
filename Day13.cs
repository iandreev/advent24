namespace Advent24;

internal class Day13
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day13.txt");
        Console.WriteLine($"result: {Cheapest(input)}"); //
        Console.WriteLine($"result: {Cheapest2(input)}"); // 
    }

    private long Cheapest(string[] input)
    {
        var list = ProcessInput(input);

        var result = 0;
        foreach (var item in list)
        {
            var min = int.MaxValue;
            for (int i = 0; i< 1000; i++)
                for (int j = 0; j < 1000; j++)
                {
                    var (ex, ey) = (item.x * i + item.xx * j, item.y * i + item.yy * j);
                    if ((ex,ey) == (item.px, item.py))
                        min = Math.Min(min, i * 3 + j);
                }
            result += min == int.MaxValue ? 0:min;
        }
        return result;
    }

    private long Cheapest2(string[] input)
    {
        var list = ProcessInput(input);

        long correction = 10_000_000_000_000;

        long result = 0;
        foreach (var item in list)
        {
            var a = ((double)(item.px + correction) * item.yy - (item.py + correction) * item.xx)
                / (item.x * item.yy - item.y * item.xx);
            var b = (double)(item.px + correction - item.x * a) / item.xx;

            if (a - (long)a == 0 && b - (long)b == 0)
                result += (long)a*3 + (long)b;
        }
        return result;
    }

    private List<(long x, long y, long xx, long yy, long px, long py)> ProcessInput(string[] input)
    {
        List<(long x, long y, long xx, long yy, long px, long py)> res = new();
        int i = 0;
        while (i < input.Length)
        {
            var a = input[i].Replace("Button A: ", "")
                .Replace("X+", "")
                .Replace("Y+", "")
                .Replace(",", "").Split(' ').Select(long.Parse).ToArray();
            var (x, y) = (a[0], a[1]);

            i++;
            var b = input[i].Replace("Button B: ", "")
                .Replace("X+", "")
                .Replace("Y+", "")
                .Replace(",", "").Split(' ').Select(long.Parse).ToArray();
            var (xx, yy) = (b[0], b[1]);
            i++;
            var c = input[i].Replace("Prize: ", "")
                .Replace("X", "")
                .Replace("Y", "")
                .Replace("=", "")
                .Replace(",", "").Split(' ').Select(long.Parse).ToArray();
            var (pz, py) = (c[0], c[1]);
            i+=2;
            
            res.Add((x, y, xx, yy, pz, py));
        }

        return res;
    }
}
