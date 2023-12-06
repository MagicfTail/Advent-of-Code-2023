namespace AdventOfCode;

using MathNet.Numerics;

public class Day_06 : BaseDay
{
    private readonly string _input;

    public Day_06()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        StringReader reader = new(_input);

        string timesString = reader.ReadLine() + " ";
        string distancesString = reader.ReadLine() + " ";

        List<int> times = [];
        List<int> distances = [];

        string current = "";
        for (int i = 0; i < timesString.Length; i++)
        {
            if (char.IsDigit(timesString[i]))
            {
                current += timesString[i];
            }
            else if (current.Length > 0)
            {
                times.Add(Int32.Parse(current));
                current = "";
            }
        }

        for (int i = 0; i < distancesString.Length; i++)
        {
            if (char.IsDigit(distancesString[i]))
            {
                current += distancesString[i];
            }
            else if (current.Length > 0)
            {
                distances.Add(Int32.Parse(current));
                current = "";
            }
        }

        int total = 1;

        for (int i = 0; i < times.Count; i++)
        {
            // Yes we could just try all possibilities. No it isn't as fun
            var roots = FindRoots.Quadratic(-distances[i], times[i], -1);

            int start = (int)Math.Floor(Math.Min(roots.Item1.Real, roots.Item2.Real)) + 1;
            int end = (int)Math.Ceiling(Math.Max(roots.Item1.Real, roots.Item2.Real)) - 1;

            total *= end - start + 1;
        }

        return new(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        StringReader reader = new(_input);

        string timesString = reader.ReadLine();
        string distancesString = reader.ReadLine();

        string current = "";
        for (int i = 0; i < timesString.Length; i++)
        {
            if (char.IsDigit(timesString[i]))
            {
                current += timesString[i];
            }
        }
        long time = Int64.Parse(current);

        current = "";
        for (int i = 0; i < distancesString.Length; i++)
        {
            if (char.IsDigit(distancesString[i]))
            {
                current += distancesString[i];
            }
        }

        long distance = Int64.Parse(current);

        // Yes we could still just try all possibilities. No it still isn't as fun
        var roots = FindRoots.Quadratic(-distance, time, -1);

        double start = Math.Floor(Math.Min(roots.Item1.Real, roots.Item2.Real)) + 1;
        double end = Math.Ceiling(Math.Max(roots.Item1.Real, roots.Item2.Real)) - 1;

        return new((end - start + 1).ToString());
    }
}
