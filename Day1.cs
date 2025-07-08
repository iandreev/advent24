namespace Advent24;

internal class Day1
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day1.txt");
        Console.WriteLine($"dist: {FindDist(input)}");
        Console.WriteLine($"similarities: {FindSimilarity(input)}");
    }

    public int FindDist(string[] input)
    {
        var (a, b) = ProcessInput(input);

        a = a.OrderBy(n => n).ToList();
        b = b.OrderBy(n => n).ToList();

        var dist = 0;
        for (int i = 0; i < a.Count; i++)
        {
            dist += Math.Abs(a[i] - b[i]);
        }

        return dist;
    }

    public int FindSimilarity(string[] input)
    {
        var (a, b) = ProcessInput(input);
        var freq = new Dictionary<int, int>();
        for (int i = 0; i < b.Count; i++)
        {
            if (!freq.ContainsKey(b[i]))
            {
                freq[b[i]] = 0;
            }
            freq[b[i]]++;
        }

        var result = 0;
        for (int i = 0; i < a.Count; i++)
        {
            result += a[i] * freq.GetValueOrDefault(a[i], 0);
        }

        return result;
    }

    private static (List<int> a, List<int> b) ProcessInput(string[] input)
    {
        var a = new List<int>(input.Length);
        var b = new List<int>(input.Length);
        foreach (var line in input)
        {
            var nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList();
            a.Add(nums[0]);
            b.Add(nums[^1]);
        }

        return (a, b);
    }
}
