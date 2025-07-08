using System.Text.RegularExpressions;

namespace Advent24;

internal class Day3
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day3.txt");
        Console.WriteLine($"mul: {FindMul(input)}");
        Console.WriteLine($"mul: {FindMul2(input)}");
    }

    public int FindMul(string[] input)
    {
        return new Regex(@"mul\(\d{1,3},\d{1,3}\)").Matches(input[0]).Select(x => x.Value.Replace("mul(", "").Replace(")", ""))
              .Select(x => x.Split(','))
              .Select(x => int.Parse(x[0]) * int.Parse(x[1]))
              .Sum();
    }

    public int FindMul2(string[] input)
    {
        var matches = new Regex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)").Matches(input[0])
            .Select(x => x.Value).ToList();

        var result = 0;
        var mul = 1;
        foreach (var m in matches)
        {
            if (m == "do()") mul = 1;
            else
            if (m == "don't()") mul = 0;
            else
            {
                var res = m.Replace("mul(", "").Replace(")", "")
                    .Split(',').Select(x => int.Parse(x)).ToList();
                result += res[0] * res[1] * mul;
            }
        }

        return result;
    }
}
