namespace Advent24;

internal class Day11
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day11.txt");
        Console.WriteLine($"result: {Nums(input)}"); // 220722
        Console.WriteLine($"result: {Nums75(input)}");
    }

    private long Nums(string[] input)
    {
        var numbers = ProcessInput(input);
        Enumerable.Range(0, 25).ToList().ForEach(_ => { numbers = Blink(numbers); });
        return numbers.Count();
    }

    private long Nums75(string[] input)
    {
        var numbers = ProcessInput(input).OrderBy(x => x).ToList();
        var d = numbers.ToDictionary(k => k, v => (long)1);

        for (int i = 0; i < 75; i++)
        {
            d = Blink2(d);
        }

        return d.Sum(x => x.Value);
    }

    private Dictionary<long, long> Blink2(Dictionary<long, long> numbers)
    {
        var res = new Dictionary<long, long>();

        foreach (var i in numbers.Keys)
        {
            if (i == 0)
            {
                if (!res.ContainsKey(1)) res[1] = 0;
                res[1] += numbers[i];
            }
            else if (i.ToString().Length % 2 == 0)
            {
                var c = i.ToString();
                var a = long.Parse(c.Substring(0, c.Length / 2));
                var b = long.Parse(c.Substring(c.Length / 2));
                if (!res.ContainsKey(a)) res[a] = 0; res[a] += numbers[i];
                if (!res.ContainsKey(b)) res[b] = 0; res[b] += numbers[i];
            }
            else
            {
                var v = i * 2024;
                if (!res.ContainsKey(v)) res[v] = 0; res[v] += numbers[i];
            }
        }

        return res;
    }

    private List<long> Blink(List<long> numbers)
    {
        var res = new List<long>();
        for (var i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] == 0)
            {
                res.Add(1);
            }
            else if (numbers[i].ToString().Length % 2 == 0)
            {
                var c = numbers[i].ToString();
                var a = c.Substring(0, c.Length / 2);
                var b = c.Substring(c.Length / 2);
                res.Add(long.Parse(a));            
                res.Add(long.Parse(b));
            }
            else
            {
                var v = numbers[i] * 2024;
                res.Add(v);
            }
        }
        return res;
    }

    List<long> ProcessInput(string[] input)
        => input[0].Split(' ').Select(long.Parse).ToList();
}
