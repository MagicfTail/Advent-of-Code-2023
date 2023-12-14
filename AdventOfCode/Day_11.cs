namespace AdventOfCode;

public class Day_11 : BaseDay
{
    private readonly string _input;

    public Day_11()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    struct Star(int row, int col)
    {
        public int Row = row;
        public int Col = col;
    }

    public override ValueTask<string> Solve_1()
    {
        string[] lines = _input.Split('\n');

        int rows = lines.Length;
        int cols = lines[0].Length;

        List<Star> stars = [];

        bool[] filledRows = new bool[rows];

        bool[] filledCols = new bool[cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (lines[row][col] == '#')
                {
                    stars.Add(new Star(row, col));
                    filledRows[row] = true;
                    filledCols[col] = true;
                }
            }
        }

        int total = 0;
        int count = 0;

        for (int i = 0; i < stars.Count; i++)
        {
            Star currentStar = stars[i];

            for (int j = i + 1; j < stars.Count; j++)
            {
                Star otherStar = stars[j];

                int rowStart = Math.Min(currentStar.Row, otherStar.Row);
                int rowEnd = Math.Max(currentStar.Row, otherStar.Row);

                for (int x = rowStart + 1; x <= rowEnd; x++)
                {
                    total += filledRows[x] ? 1 : 2;
                }

                int colStart = Math.Min(currentStar.Col, otherStar.Col);
                int colEnd = Math.Max(currentStar.Col, otherStar.Col);

                for (int x = colStart + 1; x <= colEnd; x++)
                {
                    total += filledCols[x] ? 1 : 2;
                }
                count += 1;
            }
        }

        return new(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        string[] lines = _input.Split('\n');

        int rows = lines.Length;
        int cols = lines[0].Length;

        List<Star> stars = [];

        bool[] filledRows = new bool[rows];

        bool[] filledCols = new bool[cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (lines[row][col] == '#')
                {
                    stars.Add(new Star(row, col));
                    filledRows[row] = true;
                    filledCols[col] = true;
                }
            }
        }

        long total = 0;
        int count = 0;

        for (int i = 0; i < stars.Count; i++)
        {
            Star currentStar = stars[i];

            for (int j = i + 1; j < stars.Count; j++)
            {
                Star otherStar = stars[j];

                int rowStart = Math.Min(currentStar.Row, otherStar.Row);
                int rowEnd = Math.Max(currentStar.Row, otherStar.Row);

                for (int x = rowStart + 1; x <= rowEnd; x++)
                {
                    total += filledRows[x] ? 1 : 1000000;
                }

                int colStart = Math.Min(currentStar.Col, otherStar.Col);
                int colEnd = Math.Max(currentStar.Col, otherStar.Col);

                for (int x = colStart + 1; x <= colEnd; x++)
                {
                    total += filledCols[x] ? 1 : 1000000;
                }
                count += 1;
            }
        }

        return new(total.ToString());
    }
}
