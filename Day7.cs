namespace Advent24;

internal class Day7
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day7.txt");
        Console.WriteLine($"total: {FindTotalOperations(input)}");
        Console.WriteLine($"total: {FindTotalOperations2(input)}");
    }

    private long FindTotalOperations(string[] input)
    {
        var data = ProcessInput(input);
        long total = 0;
        foreach (var (result, nums) in data)
        {
            total += CalLine(nums, 1, nums[0], result) > 0 ? result : 0;
        }
        return total;
    }

    private long FindTotalOperations2(string[] input)
    {
        var data = ProcessInput(input);
        long total = 0;
        foreach (var (result, nums) in data)
        {
            total += CalLine2(nums, 1, nums[0], result) > 0 ? result : 0;
        }
        return total;
    }

    private int CalLine2(List<int> nums, int pos, long cur, long target)
    {
        if (cur > target)
        {
            return 0;
        }
        if (pos >= nums.Count)
        {
            return cur == target ? 1 : 0;
        }
        return CalLine2(nums, pos + 1, cur + nums[pos], target) +
            CalLine2(nums, pos + 1, cur * nums[pos], target) +
            CalLine2(nums, pos + 1, long.Parse(cur.ToString() + nums[pos].ToString()), target);
    }

    private int CalLine(List<int> nums, int pos, long cur, long target)
    {
        if (cur > target)
        {
            return 0;
        }
        if (pos >= nums.Count)
        {
            return cur == target ? 1 : 0;
        }
        return CalLine(nums, pos + 1, cur + nums[pos], target) +
            CalLine(nums, pos + 1, cur * nums[pos], target);
    }
    private List<(long result, List<int> nums)> ProcessInput(string[] input)
         => input.Select(line =>
            {
                var s = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                var result = long.Parse(s[0]);
                var nums = s[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => int.Parse(n)).ToList();
                return (result, nums);
            }).ToList();
}
