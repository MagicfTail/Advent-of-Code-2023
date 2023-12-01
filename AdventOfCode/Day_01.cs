namespace AdventOfCode;

public partial class Day_01 : BaseDay
{
    private readonly string _input;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        StringReader reader = new(_input);
        string line = reader.ReadLine();

        int output = 0;

        while (line != null)
        {
            string cv = "";

            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    cv += line[i];
                    break;
                }
            }

            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    cv += line[i];
                    break;
                }
            }

            output += Int32.Parse(cv);

            line = reader.ReadLine();
        }

        return new(output.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Regex lazyRegex = MyLazyRegex();
        Regex regex = MyRegex();

        StringReader reader = new(_input);
        string line = reader.ReadLine();

        int output = 0;

        while (line != null)
        {
            string cv = "";

            Match[] items = [lazyRegex.Match(line), regex.Match(line)];

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Groups[1].Value.Length == 1)
                {
                    cv += items[i].Groups[1].Value;
                    continue;
                }

                switch (items[i].Groups[1].Value)
                {
                    case "one":
                        cv += "1";
                        break;
                    case "two":
                        cv += "2";
                        break;
                    case "three":
                        cv += "3";
                        break;
                    case "four":
                        cv += "4";
                        break;
                    case "five":
                        cv += "5";
                        break;
                    case "six":
                        cv += "6";
                        break;
                    case "seven":
                        cv += "7";
                        break;
                    case "eight":
                        cv += "8";
                        break;
                    case "nine":
                        cv += "9";
                        break;
                    default:
                        break;
                }
            }

            output += Int32.Parse(cv);

            line = reader.ReadLine();
        }

        return new(output.ToString());
    }

    [GeneratedRegex(@".*?(\d|one|two|three|four|five|six|seven|eight|nine)")]
    private static partial Regex MyLazyRegex();

    [GeneratedRegex(@".*(\d|one|two|three|four|five|six|seven|eight|nine)")]
    private static partial Regex MyRegex();
}
