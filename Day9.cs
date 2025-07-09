namespace Advent24;

internal class Day9
{

    public void Go()
    {
        var v = new HashSet<int>();
        var input = File.ReadAllLines("input/day9.txt");
        Console.WriteLine($"result: {Checksum(input)}");
        Console.WriteLine($"result: {Checksum2(input)}");
    }

    long Checksum(string[] input)
    {
        var map = ProcessInput(input);
        //Show(map);

        var l = map.FindIndex(x => x.Num == -1);
        var r = map.Count - 1;

        while (l < r)
        {
            map[l] = map[r];
            map[r] = new FF(-1);
            while (l < r && map[l].Num != -1) l++;
            r--;
        }
        //Show(map);

        long result = 0;
        for (int i = 0; i < map.Count; i++) result += map[i].Num > 0 ? i * map[i].Num : 0;
        return result;
    }

    long Checksum2(string[] input)
    {
        var map = ProcessInput(input);
        Show(map);

        var maxId = map.MaxBy(x => x.Num)!;
        for (int i = maxId.Num; i >= 1; i--)
        {
            var start = map.FindIndex(x => x.Num == i);
            var end = map.FindLastIndex(x => x.Num == i);
            var size = end - start + 1;

            var numk = 0;
            for (int j = 0; j < start; j++)
            {
                if (map[j].Num == -1)
                {
                    numk++;
                    if (numk == size)
                    {
                        Fill(map, start, end, -1);
                        Fill(map, j-numk+1, j, i);
                        break;
                    }
                }
                else numk = 0;
            }
        }

        Show(map);

        long result = 0;
        for (int i = 0; i < map.Count; i++) result += map[i].Num > 0 ? i * map[i].Num : 0;
        return result;
    }
    
    void Fill(List<FF> map, int l, int r, int num)
    {
        for (int i = l; i <= r; i++) map[i] = new FF(num);
    }

    private static void Show(List<FF> map)
    {
        Console.WriteLine(string.Join("", map.Select(x => x.Num == -1 ? "." : x.Num.ToString())));
    }

    List<FF> ProcessInput(string[] input)
    {
        var result = new List<FF>();
        int id = 0;

        var line = input[0];
        for (int i = 0; i < line.Length; i++)
        {
            var num = int.Parse(line[i].ToString());
            var fNum = i % 2 == 0 ? id : -1;
            result.AddRange(Enumerable.Range(1, num).Select(x => new FF(fNum)));
            id += fNum == id ? 1 : 0;
        }
        return result;
    }

    record FF(int Num);
}
