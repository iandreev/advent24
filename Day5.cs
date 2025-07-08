namespace Advent24;

internal class Day5
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day5.txt");
        Console.WriteLine($"order: {FindOrder(input)}");
        Console.WriteLine($"order: {FindUnOrdered(input)}");
    }

    private int FindUnOrdered(string[] input)
    {
        var (rules, lines) = ProcessInput(input);

        var result = 0;
        var d = new Dictionary<int, HashSet<int>>();

        foreach (var rule in rules)
        {
            if (!d.ContainsKey(rule.x))
            {
                d[rule.x] = new HashSet<int>();
            }
            d[rule.x].Add(rule.y);
        }

        foreach (var line in lines)
        {
            var (valid, x, y) = IsValid(d, line);
            if (!valid)
            {
                while (!valid)
                {
                    // swap x and y
                    var temp = line[x];
                    line[x] = line[y];
                    line[y] = temp;

                    (valid, x, y) = IsValid(d, line);
                }

                result += line[line.Count / 2];
            }
        }
        return result;
    }

    private static (bool, int, int) IsValid(Dictionary<int, HashSet<int>> d, List<int> line)
    {
        for (int i = 0; i < line.Count; i++)
        {
            var v = d.GetValueOrDefault(line[i], new HashSet<int>());
            for (int j = 0; j < i; j++)
            {
                if (v.Contains(line[j]))
                {
                    return (false, j, i);
                }
            }
        }

        return (true, 0 ,0);
    }

    private int FindOrder(string[] input)
    {
        var (rules, lines) = ProcessInput(input);

        var result = 0;
        var d = new Dictionary<int, HashSet<int>>();

        foreach (var rule in rules)
        {
            if (!d.ContainsKey(rule.x))
            {
                d[rule.x] = new HashSet<int>();
            }
            d[rule.x].Add(rule.y);
        }

        foreach (var line in lines)
        {
            var valid = true;
            var set = new HashSet<int>();
            for (int i = 0; i < line.Count; i++)
            {
                var v = d.GetValueOrDefault(line[i], new HashSet<int>());
                if (set.Overlaps(v))
                {
                    valid = false;
                    break;
                }

                set.Add(line[i]);
            }

            if (valid)
            {
                result += line[line.Count / 2];
            }
        }
        return result;
    }

    private (List<(int x, int y)> rules, List<List<int>> lines) ProcessInput(string[] input)
    {
        var lines = new List<List<int>>();
        var rules = new List<(int x, int y)>();
        int i = 0;
        while (input[i] != "")
        {
            var a = input[i].Split('|').Select(x => int.Parse(x));
            rules.Add((a.First(), a.Last()));
            i++;
        }

        i++;
        while (i < input.Length)
        {
            lines.Add(input[i].Split(',').Select(x => int.Parse(x)).ToList());
            i++;
        }
        return (rules, lines);
    }
}
