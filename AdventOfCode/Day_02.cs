namespace AdventOfCode;

public partial class Day_02 : BaseDay
{
    private readonly string _input;

    public Day_02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int redMax = 12;
        int greenMax = 13;
        int blueMax = 14;
        Regex red = Red();
        Regex green = Green();
        Regex blue = Blue();
        Regex game = Game();

        StringReader reader = new(_input);

        int total = 0;

        String line = reader.ReadLine();

        while (line != null)
        {
            bool possible = true;
            foreach (Match rMatch in red.Matches(line))
            {
                if (Int32.Parse(rMatch.Groups[1].Value) > redMax)
                {
                    possible = false;
                    break;
                }
            }

            if (!possible)
            {
                line = reader.ReadLine();
                continue;
            }

            foreach (Match gMatch in green.Matches(line))
            {
                if (Int32.Parse(gMatch.Groups[1].Value) > greenMax)
                {
                    possible = false;
                    break;
                }
            }

            if (!possible)
            {
                line = reader.ReadLine();
                continue;
            }

            foreach (Match bMatch in blue.Matches(line))
            {
                if (Int32.Parse(bMatch.Groups[1].Value) > blueMax)
                {
                    possible = false;
                    break;
                }
            }

            if (!possible)
            {
                line = reader.ReadLine();
                continue;
            }

            total += Int32.Parse(game.Match(line).Groups[1].Value);

            line = reader.ReadLine();
        }

        return new(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Regex red = Red();
        Regex green = Green();
        Regex blue = Blue();

        StringReader reader = new(_input);

        int total = 0;

        String line = reader.ReadLine();

        while (line != null)
        {
            int redMin = 0;
            foreach (Match rMatch in red.Matches(line))
            {
                if (Int32.Parse(rMatch.Groups[1].Value) > redMin)
                {
                    redMin = Int32.Parse(rMatch.Groups[1].Value);
                }
            }

            int greenMin = 0;
            foreach (Match gMatch in green.Matches(line))
            {
                if (Int32.Parse(gMatch.Groups[1].Value) > greenMin)
                {
                    greenMin = Int32.Parse(gMatch.Groups[1].Value);
                }
            }

            int blueMin = 0;
            foreach (Match bMatch in blue.Matches(line))
            {
                if (Int32.Parse(bMatch.Groups[1].Value) > blueMin)
                {
                    blueMin = Int32.Parse(bMatch.Groups[1].Value);
                }
            }

            total += redMin * greenMin * blueMin;

            line = reader.ReadLine();
        }

        return new(total.ToString());
    }

    [GeneratedRegex(@"(\d*) red")]
    private static partial Regex Red();
    [GeneratedRegex(@"(\d*) green")]
    private static partial Regex Green();
    [GeneratedRegex(@"(\d*) blue")]
    private static partial Regex Blue();
    [GeneratedRegex(@"Game (\d*)")]
    private static partial Regex Game();
}
