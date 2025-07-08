namespace Advent24;

internal class Day2
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day2.txt");
        Console.WriteLine($"safe: {FindSafe(input)}");
        Console.WriteLine($"safe: {FindSafeWithSkip(input)}");
    }

    public int FindSafe(string[] input) => ProcessInput(input).Count(IsSafe);

    public int FindSafeWithSkip(string[] input)
        => ProcessInput(input).Where(x =>
            {
                if (IsSafe(x)) return true;

                for (int i = 0; i < x.Count; i++)
                {
                    if (IsSafe(x.Where((_, j) => j != i).ToList()))
                        return true;
                }
                return false;
            }).Count();


    private bool IsSafe(List<int> line)
    {
        var sign = Math.Sign(line[^1] - line[0]);
        var prev = line[0];
        for (int i = 1; i < line.Count; i++)
        {
            if (Math.Sign(line[i] - prev) != sign)
                return false;

            var dist = Math.Abs(line[i] - prev);
            if (dist < 1 || dist > 3)
                return false;

            prev = line[i];
        }

        return true;
    }

    private static List<List<int>> ProcessInput(string[] input)
        => input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList()).ToList();
}
