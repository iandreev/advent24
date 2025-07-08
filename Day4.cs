namespace Advent24;

internal class Day4
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day4.txt");
        Console.WriteLine($"xmas: {FindXMASSimple(input)}");
        Console.WriteLine($"xmas: {FindXMASStar(input)}");
    }

    private int FindXMASStar(string[] input)
    {
        var result = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'A')
                {
                    var (w1, w2) = Diag(input, i, j);
                    if (w1 is "MAS" or "SAM" && w2 is "MAS" or "SAM")
                        result += 1;
                }
            }
        }
        return result;

    }

    private (string a, string b) Diag(string[] input, int x, int y)
    {
        var word1 = "" + At(input, x - 1, y - 1) + At(input, x, y) + At(input, x + 1, y + 1);
        var word2 = "" + At(input, x + 1, y - 1) + At(input, x, y) + At(input, x - 1, y + 1);

        return (word1, word2);
    }

    private int FindXMASSimple(string[] input)
    {
        var result = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'X') result += FindWord(input, i, j);
            }
        }
        return result;
    }

    private int FindWord(string[] input, int x, int y)
    {
        int[] dx = [0, 0, 1, 1, -1, -1, 1, -1];
        int[] dy = [1, -1, 1, -1, 1, -1, 0, 0];

        var words = 0;
        for (int i = 0; i < dx.Length; i++)
        {
            var word = string.Empty;
            for (int j = 0; j < 4; j++)
            {
                word += At(input, x + dx[i] * j, y + dy[i] * j);
            }
            if (word == "XMAS")
            {
                words += 1;
            }
        }
        return words;
    }

    private char? At(string[] input, int x, int y)
        => (x < 0 || x >= input.Length || y < 0 || y >= input[0].Length) ? null : input[x][y];
}
